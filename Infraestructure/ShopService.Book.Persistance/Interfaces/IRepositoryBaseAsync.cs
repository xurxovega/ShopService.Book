using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ShopService.Book.Infraestructure.Persistance.Utils;

namespace ShopService.Book.Infraestructure.Persistance.Interface
{
    public interface IRepositoryBaseAsync<T>: IDisposable where T : class
    {   
        //IRepositoryBase<Entity, TKey> GetRepository();
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindByAsync(QueryParameters<T> queryParameters);
        Task<PagedResult<T>> FindByAsync(QueryParameters<T> queryParameters, PageParameters pageParameters);               
        Task<bool> HasChangesAsync(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> RemoveAsync(int id, CancellationToken cancellationToken = default); 
    }
}