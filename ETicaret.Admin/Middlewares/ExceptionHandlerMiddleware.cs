using ETicaret.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Admin.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                var model = new ErrorViewModel
                {
                    ErrorCode = ex.Message,
                    ErrorMessage = ex.Message
                };
                httpContext.Response.Redirect("/Home/Error?errorCode=" + model.ErrorCode + "&errorMessage=" + model.ErrorMessage);
            }

        }
    }
}
