using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShopService.Book.Infraestructure.Persistance.Interface;
using ShopService.Book.Infraestructure.Persistance.Utils;

namespace ShopService.Book.Infraestructure.Persistance.Repository
{
    public abstract class RepositoryBaseAsync<T> : IRepositoryBaseAsync<T> where T : class
    {
        private readonly DbContext _dbContext;
        public RepositoryBaseAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            //_dbSet = dbSet;
        }
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> where){
            return await _dbContext.Set<T>().Where(where).CountAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> FindByAsync(QueryParameters<T> queryParameters)
        {
            Expression<Func<T, bool>> whereTrue = x => true;
            Expression<Func<T, bool>> where = (queryParameters.Where == null) ? whereTrue : queryParameters.Where;
            List<T> recordsList = new();

            if (queryParameters.OrderByDescending != null){
                recordsList = await _dbContext.Set<T>().Where(where).ToListAsync();
                return recordsList.OrderByDescending(queryParameters.OrderByDescending);
            }else{
                if (queryParameters.OrderBy != null){
                    recordsList = await _dbContext.Set<T>().Where(where).ToListAsync();
                    return recordsList.OrderBy(queryParameters.OrderByDescending);
                }else{
                    Func<T, object> orderByDefault = x => x.Equals("Id");
                    queryParameters.OrderBy = (queryParameters.OrderBy == null) ? orderByDefault : queryParameters.OrderBy;
                    recordsList = await _dbContext.Set<T>().Where(where).ToListAsync();
                    return recordsList.OrderBy(queryParameters.OrderBy);
                }

            }
        }
        public async Task<PagedResult<T>> FindByAsync(QueryParameters<T> queryParameters, PageParameters pageParameters){
            Expression<Func<T, bool>> whereTrue = x => true;
            Expression<Func<T, bool>> predicate = (queryParameters.Where == null) ? whereTrue : queryParameters.Where;
            IEnumerable<T> recordsList;

            if (queryParameters.OrderByDescending != null){
                recordsList = await _dbContext.Set<T>().Where(predicate).ToListAsync();
                recordsList = recordsList.OrderByDescending(queryParameters.OrderByDescending);
            }else{
                if (queryParameters.OrderBy != null){
                    recordsList = await _dbContext.Set<T>().Where(predicate).ToListAsync();
                    recordsList = recordsList.OrderBy(queryParameters.OrderByDescending);
                }else{
                    Func<T, object> orderByDefault = x => x.Equals("Id");
                    queryParameters.OrderBy = (queryParameters.OrderBy == null) ? orderByDefault : queryParameters.OrderBy;
                    recordsList = await _dbContext.Set<T>().Where(predicate).ToListAsync();
                    recordsList = recordsList.OrderBy(queryParameters.OrderBy);
                }
            }

            PagedResult<T> resultList = new(
                        pageParameters.PageSize, 
                        pageParameters.CurrentPage, 
                        (int)Math.Ceiling((decimal)recordsList.Count()/10), 
                        recordsList.Count(), 
                        recordsList.Skip(pageParameters.PageSize * (pageParameters.CurrentPage-1)).Take(pageParameters.PageSize) 
                    );  
            return resultList;                   
        } 
        public async Task<bool> HasChangesAsync(T entity)
        {
            return await
                Task.FromResult(
                    _dbContext.ChangeTracker
                    .Entries<T>()  
                    .Any(a => 
                        a.State == EntityState.Added || a.State == EntityState.Deleted || a.State == EntityState.Modified
                    )
                );  
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }    
        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            // Move data to updated. update data.
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<T> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            // Not remove. Move data to historic_deleted
            var deletedRecord = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(deletedRecord);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return deletedRecord;
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}