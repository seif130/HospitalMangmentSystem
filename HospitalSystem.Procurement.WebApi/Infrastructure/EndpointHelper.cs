using MediatR;
using FluentValidation;
using HospitalSystem.Application.Shared.Common;

namespace HospitalSystem.WebApi.Infrastructure;

public static class EndpointHelper
{
    public static async Task<IResult> SendAsync<TRequest>(
        TRequest request, ISender sender, CancellationToken ct)
        where TRequest : notnull, IRequest<Result>
    {
        var response = await sender.Send(request, ct);
        return response.ToHttpResult();
    }

    public static async Task<IResult> SendAsync<TRequest, TResponse>(
        TRequest request, ISender sender, CancellationToken ct,
        Func<TResponse, IResult>? onSuccess = null)
        where TRequest : notnull, IRequest<Result<TResponse>>
    {
        var response = await sender.Send(request, ct);
        return onSuccess is null ? response.ToHttpResult() :
            response.IsSuccess ? onSuccess(response.Value) : response.ToHttpResult();
    }
}
