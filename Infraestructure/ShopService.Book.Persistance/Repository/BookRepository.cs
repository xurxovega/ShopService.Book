using ShopService.Book.Infraestructure.Persistance.Interface;

namespace ShopService.Book.Infraestructure.Persistance.Repository
{
    public class BookRepository<T> : RepositoryBaseAsync<T>, IRepositoryBaseAsync<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        public BookRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}