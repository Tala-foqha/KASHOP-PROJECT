namespace KASHOP.PL.MiddleWere
{
    public static class customMiddlewereExtinsions
    {
        public static IApplicationBuilder UseCustomMiddlewere(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddlewere>();
        }
    }
    public class CustomMiddlewere
    {
        private readonly RequestDelegate _next;
        public CustomMiddlewere(RequestDelegate next) { 
        _next=next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("processing Request");
            await _next(context);
            Console.WriteLine("processing Response");

        }
    }
}
