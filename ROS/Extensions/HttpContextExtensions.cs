using System.Net;

namespace ROS.TemporaryBd;

public static class HttpContextExtensions
{
    public static Result<T> WithResult<T>(this HttpContext context, HttpStatusCode status, T? result) where T : class
    {
        context.Response.StatusCode = (int)status;
        return new Result<T>(result);
    }

    public static Result<T> WithError<T>(this HttpContext context, HttpStatusCode status, string error) where T : class
    {
        context.Response.StatusCode = (int)status;
        return new Result<T>(error);
    }
}