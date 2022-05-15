using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            this.next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke (HttpContext context)
        {
            var watch=Stopwatch.StartNew();//isteğin response olana kadar geçirdiği süreyi bulmak için süre başlattık
            try
            {
                string message="[Request] HTTP "+ context.Request.Method +"-"+ context.Request.Path;
                _loggerService.Write(message);

                await next(context);
                watch.Stop();

                message="[Response] HTTP "+ context.Request.Method +"-"+ context.Request.Path+" responsed "+context.Response.       StatusCode+" in "+watch.Elapsed.Milliseconds+" ms ";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop(); //request anında veya diğer middleware'ların çalışması aşamasında hata oluşursa süre duramayacağından burayada ekledik
                await HandleException(context,ex,watch);//Hata anında yönettiğimiz metot
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            
            string message="[Error] HTTP "+ context.Request.Method +"-"+ context.Response.StatusCode+" Error Message: "+ex.Message+" in "+watch.Elapsed.TotalMilliseconds+" ms ";
            _loggerService.Write(message);
            

            var result=JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}