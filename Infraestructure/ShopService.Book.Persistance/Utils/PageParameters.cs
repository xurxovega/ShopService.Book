namespace ShopService.Book.Infraestructure.Persistance.Utils
{
    public class PageParameters
    {
        public int CurrentPage {get; set;}
        public int PageSize {get; set;}
        public PageParameters()
        {
            CurrentPage = 1;
            PageSize = 10;
        }
        public PageParameters(int currentPage, int pageSize)
        {
            CurrentPage = (currentPage <= 0 || currentPage.Equals(null) ) ? 1 : currentPage;
            PageSize = (pageSize <= 0 || pageSize.Equals(null) ) ? 1 : pageSize;;
        }
    }
}