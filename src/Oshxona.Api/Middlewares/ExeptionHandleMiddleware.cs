using Azure;
using Oshxona.Api.Models;
using Oshxona.Service.Exeptions;

namespace Oshxona.Api.Middlewares
{
    public class ExeptionHandleMiddleware
    {
        private readonly RequestDelegate next;

        public ExeptionHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (OshxonaExeption exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new Responce
                {
                    Code = exception.Code,
                    Error = exception.Message,
                    Data = exception.Data
                });
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new Responce
                {
                    Code = 500,
                    Error = exception.Message
                });
            }
        }
    }
}
