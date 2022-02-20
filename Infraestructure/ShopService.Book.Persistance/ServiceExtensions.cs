using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShopService.Book.Infraestructure.Persistance.Interface;
using ShopService.Book.Infraestructure.Persistance.Repository;

namespace ShopService.Book.Infraestructure.Persistance
{
    public static class ServiceExtensions
    {
        public static void AddLayerInfraestructurePersistance(this IServiceCollection services, IConfiguration config)
        {
                services.AddDbContext<AppDbContext>(
                    options => options.UseSqlServer(config.GetConnectionString("BookConnection")
                    //,b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.F)
                    ));

                //services.AddTransient(typeof(IRepositoryBaseAsync<Domain.Entities.AuthorBook>), typeof(RepositoryBaseAsync<Domain.Entities.AuthorBook>)) ;
                services.AddTransient(typeof(IRepositoryBaseAsync<>), typeof(BookRepository<>)) ; // para que funcione tiene hay que crear el AuthoRepository
        }
    }
}
