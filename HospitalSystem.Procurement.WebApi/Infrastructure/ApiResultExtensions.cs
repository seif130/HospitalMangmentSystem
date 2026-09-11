
namespace HospitalSystem.WebApi.Infrastructure;

public static class ApiResultExtensions
{
    public static IResult ToHttpResult(this Result result)
        => result.IsSuccess ? Results.NoContent() : ToProblem(result.Error);

    public static IResult ToHttpResult<T>(this Result<T> result)
        => result.IsSuccess ? Results.Ok(result.Value) : ToProblem(result.Error);

    public static IResult ToCreatedResult<T>(this Result<T> result, Func<T, string> locationFactory)
        => result.IsSuccess
            ? Results.Created(locationFactory(result.Value), result.Value)
            : ToProblem(result.Error);

    private static IResult ToProblem(Error error) => error.Type switch
    {
        ErrorType.NotFound => Results.Problem(title: error.Code, detail: error.Message, statusCode: 404),
        ErrorType.Validation => Results.Problem(title: error.Code, detail: error.Message, statusCode: 400),
        ErrorType.Conflict => Results.Problem(title: error.Code, detail: error.Message, statusCode: 409),
        ErrorType.Unauthorized => Results.Problem(title: error.Code, detail: error.Message, statusCode: 401),
        _ => Results.Problem(title: error.Code, detail: error.Message, statusCode: 500)
    };
}
