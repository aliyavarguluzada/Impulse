using Impulse.Data;
using Impulse.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace EcommerceApp.MVC.Middlewares
{
    public static class RequestLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseMyLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogginMiddleware>();
        }
    }


    public class RequestLogginMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLogginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            context.Request.EnableBuffering();


            var dbContext = context.RequestServices.GetService<ApplicationDbContext>();
            var logger = context.RequestServices.GetService<ILogger<RequestLogginMiddleware>>();
            logger.LogInformation("--------Request started--------");
            logger.LogInformation("Request date is : " + DateTime.Now.ToString());


            string ip = context.Connection.RemoteIpAddress.ToString();
            logger.LogInformation("Request ip is : " + ip);

            string address = context.Request.GetDisplayUrl();
            logger.LogInformation("Request to : " + address);



            Log log = new Log();

            log.IP = ip;
            log.Address = address;
            log.Logged = DateTime.Now;
            log.LogTypeId = 20;



            if (context.Request.Method == "POST" || context.Request.Method == "PUT")
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await context.Request.Body.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string body = await reader.ReadToEndAsync();
                        log.Content = body;
                        logger.LogInformation("Request BODY is : " + body);
                    }
                }

                context.Request.Body.Position = 0;
            }
            else if (context.Request.Method == "GET")
            {
                log.Content = "";
            }



            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();



            await _next(context);


            logger.LogInformation("---------REQUEST ENDED--------");


        }
    }


}
