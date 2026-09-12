using HospitalSystem.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace HospitalSystem.WebApi.Common;
public static class EndpointHelper
{
    public static async Task<IResult> SendAsync<TRequest>(TRequest request, ISender sender, CancellationToken ct)
        where TRequest : notnull, IRequest<Result>
        => (await sender.Send(request, ct)).ToHttpResult();
    public static async Task<IResult> SendAsync<TRequest,TResponse>(TRequest request, ISender sender, CancellationToken ct, Func<TResponse,IResult>? onSuccess=null)
        where TRequest : notnull, IRequest<Result<TResponse>>
    {
        var response=await sender.Send(request,ct);
        return response.IsFailure || onSuccess is null ? response.ToHttpResult() : onSuccess(response.Value);
    }
}
