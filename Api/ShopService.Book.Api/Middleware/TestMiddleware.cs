using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace ShopService.Book.Api.Middlewares
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            System.Console.WriteLine("BookService: TestMiddleware ");
            try
            {
                System.Console.WriteLine("BookService: Request ");
                //System.Console.WriteLine($"Request {context.Request.Method}");
                //System.Console.WriteLine($"Request {context.Request?.Path.Value}");
                await _next(context);
                System.Console.WriteLine("BookService: Response");

            }
            catch(System.Exception err){
                System.Console.WriteLine("BookService: Error catch", err.Message);
                await context.Response.WriteAsync(err.Message);
            }
        }
    }
}
