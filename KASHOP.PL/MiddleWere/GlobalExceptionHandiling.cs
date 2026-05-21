using KASHOP.DAL.Dto.Response;

namespace KASHOP.PL.MiddleWere
{
    public class GlobalExceptionHandiling
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandiling(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokAsync(HttpContext context)
        {
            try
            {
                //يحمل الريكوست
                await _next(context);

            }
            catch (Exception e)
            {
                var errorDetails = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Server Error",
                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(errorDetails);
            }
        }
    }
}
