using Serilog;
using System.Net;
using System.Text.Json;

namespace BackCar.Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Ejecutar el siguiente middleware
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error inesperado."); // Registrar el error con Serilog
                await HandleExceptionAsync(context, ex); // Manejar la respuesta
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new { message = "Ha ocurrido un error interno en el servidor." };
            var responseText = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(responseText);
        }
    }
}
