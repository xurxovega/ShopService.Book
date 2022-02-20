using Microsoft.AspNetCore.Builder;
using ShopService.Book.Api.Middlewares;

namespace ShopService.Book.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void UseHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TestMiddleware>();
        }
    }
}