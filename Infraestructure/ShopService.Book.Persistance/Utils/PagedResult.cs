using System.Collections.Generic;

namespace ShopService.Book.Infraestructure.Persistance.Utils
{
    public class PagedResult<T>
    {
        public PagedResult(int pageSize, int currentPage, int totalPages, int totalItems, IEnumerable<T> result)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalItems = totalItems;
            Result = result;
        }
        private int _pageSize = 10; //also max size 
        private int _currentPage = 1;        
        public int CurrentPage { 
            get { return _currentPage; }
            set { _currentPage = (value == 0 || value.Equals(null) ) ? _currentPage : value; }
        }
        public int PageSize { 
            get { return _pageSize; }
            set { _pageSize = (value == 0 || value.Equals(null) ) ? _pageSize : value; }
        }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}