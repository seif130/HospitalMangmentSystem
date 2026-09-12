using System.Text.Json;
using HospitalSystem.Application.Abstractions.Events;
using HospitalSystem.Application.Abstractions.Time;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Procurement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HospitalSystem.Procurement.Infrastructure.Outbox;

internal sealed class ProcurementOutboxProcessor(
    IServiceScopeFactory scopeFactory,
    IDateTimeProvider dateTimeProvider,
    ILogger<ProcurementOutboxProcessor> logger) : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(PollInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessBatchAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Unhandled error while processing Procurement outbox.");
            }

            try
            {
                await timer.WaitForNextTickAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ProcurementDbContext>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();

        var messages = await db.OutboxMessages
            .Where(message => message.ProcessedOnUtc == null)
            .OrderBy(message => message.OccurredOnUtc)
            .Take(20)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                var eventType = Type.GetType(message.Type, throwOnError: true)!;
                var domainEvent = JsonSerializer.Deserialize(message.Payload, eventType, SerializerOptions) as IDomainEvent
                    ?? throw new InvalidOperationException($"Could not deserialize outbox message {message.Id} as {eventType.FullName}.");

                await dispatcher.DispatchAsync([domainEvent], cancellationToken);
                message.MarkProcessed(dateTimeProvider.UtcNow);
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                message.MarkFailed(exception.Message);
                logger.LogError(exception, "Failed to process outbox message {MessageId} for Procurement.", message.Id);
            }
        }

        if (messages.Count > 0)
            await db.SaveChangesAsync(cancellationToken);
    }
}
