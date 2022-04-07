using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Endpoint.Site.Helpers.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SetVisitorIdMiddleware
    {
        private readonly RequestDelegate _next;

        public SetVisitorIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string visitorId = httpContext.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                visitorId = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("VisitorId", visitorId, new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromDays(30)
                });
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SetVisitorIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseSetVisitorIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetVisitorIdMiddleware>();
        }
    }
}
