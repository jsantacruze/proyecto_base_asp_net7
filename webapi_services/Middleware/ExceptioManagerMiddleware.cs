using business_layer.ExceptionsManager;
using Newtonsoft.Json;
using System.Net;

namespace webapi_services.Middleware
{
    public class ExceptioManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptioManagerMiddleware> _logger;

        public ExceptioManagerMiddleware(RequestDelegate next, ILogger<ExceptioManagerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await AsyncExceptionManager(context, ex, _logger);
            }
        }

        private async Task AsyncExceptionManager(HttpContext context, Exception ex, ILogger<ExceptioManagerMiddleware> logger)
        {
            object errores = null;
            switch (ex)
            {
                case CustomException me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me.Errors;
                    context.Response.StatusCode = (int)me.StatusCode;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}
