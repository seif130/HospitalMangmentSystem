using HospitalSystem.Application.DependencyInjection;
using HospitalSystem.Infrastructure.DependencyInjection;
using HospitalSystem.Procurement.WebApi.DependencyInjection;
using HospitalSystem.Procurement.WebApi.Endpoints;
using HospitalSystem.WebApi.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddHospitalSystemApplication();

builder.Services.AddHospitalSystemCoreInfrastructure();

builder.Services.AddHospitalSystemAuthentication(
    builder.Configuration);

builder.Services.AddHospitalSystemApiSecurity();

builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddProcurementModule(
    builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseRateLimiter();

if (builder.Configuration.GetValue<bool>("Authentication:Enabled"))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health").AllowAnonymous();

app.MapProcurementEndpoints();

app.Run();