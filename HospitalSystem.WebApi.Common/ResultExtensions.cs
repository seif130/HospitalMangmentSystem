using HospitalSystem.Application.Common;
using Microsoft.AspNetCore.Http;
namespace HospitalSystem.WebApi.Common;
public static class ResultExtensions
{
    public static IResult ToHttpResult(this Result result)=>result.IsSuccess?Results.NoContent():ToProblem(result.Error);
    public static IResult ToHttpResult<T>(this Result<T> result,Func<T,string>? locationFactory=null)
    {
        if(result.IsFailure)return ToProblem(result.Error);
        return locationFactory is null?Results.Ok(result.Value):Results.Created(locationFactory(result.Value),result.Value);
    }
    private static IResult ToProblem(Error error)
    {
        var status=error.Type switch
        {
            ErrorType.NotFound=>StatusCodes.Status404NotFound,
            ErrorType.Validation=>StatusCodes.Status400BadRequest,
            ErrorType.Conflict=>StatusCodes.Status409Conflict,
            ErrorType.Unauthorized=>StatusCodes.Status401Unauthorized,
            _=>StatusCodes.Status500InternalServerError
        };
        return Results.Problem(statusCode:status,title:error.Code,detail:error.Message);
    }
}
