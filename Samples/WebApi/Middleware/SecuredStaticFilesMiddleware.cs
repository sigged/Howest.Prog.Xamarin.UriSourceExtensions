using System.Globalization;
using System.Net;

namespace SampleWebApi.Middleware
{


    public class SecuredStaticFilesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path;

        public SecuredStaticFilesMiddleware(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith(_path) &&
                !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync($"Access to {context.Request.Path} has been denied.");
                context.Response.Body.Dispose();
                return;
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class SecuredStaticFilesMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecuredStaticFiles(
            this IApplicationBuilder builder, string path = "/")
        {
            return builder
                .UseMiddleware<SecuredStaticFilesMiddleware>(path)
                .UseStaticFiles();
        }
    }

}
