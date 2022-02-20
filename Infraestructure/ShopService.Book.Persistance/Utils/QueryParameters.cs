using System;
using System.Linq.Expressions;

namespace ShopService.Book.Infraestructure.Persistance.Utils
{
    //public class QueryParameters<Entity, TKey> where Entity : class /* Entity of bussiness to return */ where TKey : class   /* Key from entity to filter. */
    public class QueryParameters<T> where T : class
    {
        public QueryParameters()
        {
            Where = null;
            OrderBy = null;// always Ascending
            OrderByDescending = null;
        }
        public Expression<Func<T, bool>> Where {get; set;}
        public Func<T, object> OrderBy {get; set;}
        public Func<T, object> OrderByDescending {get; set;}   
    }
}