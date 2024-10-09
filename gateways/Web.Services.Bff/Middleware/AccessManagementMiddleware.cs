using Web.Services.Bff.Services;

namespace Web.Services.Bff.Middleware;

public class AccessManagementMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    
    public async Task InvokeAsync(HttpContext context, AccessManagementService ams)
    {
        var statusCode = await ams.ShouldBeGranted(context);
        if (statusCode != StatusCodes.Status200OK)
        {
            context.Response.StatusCode = statusCode;
            return;
        }
        await _next(context);
    }
}