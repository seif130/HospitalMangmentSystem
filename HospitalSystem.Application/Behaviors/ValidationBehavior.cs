using FluentValidation;
using MediatR;
using HospitalSystem.Application.Common;

namespace HospitalSystem.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = results.SelectMany(x => x.Errors).Where(x => x is not null)
            .GroupBy(x => x.PropertyName)
            .Select(g => $"{g.Key}: {string.Join("; ", g.Select(x => x.ErrorMessage))}")
            .ToList();

        if (failures.Count == 0)
            return await next(cancellationToken);

        var error = Error.Validation("Validation.Error", string.Join(" | ", failures));
        if (typeof(TResponse) == typeof(Result))
            return (TResponse)(object)Result.Failure(error);

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = typeof(TResponse).GetGenericArguments()[0];
            var method = typeof(Result).GetMethods()
                .Single(m => m.Name == nameof(Result.Failure) && m.IsGenericMethodDefinition && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(Error))
                .MakeGenericMethod(valueType);
            return (TResponse)method.Invoke(null, [error])!;
        }

        throw new ValidationException(results.SelectMany(x => x.Errors));
    }
}
