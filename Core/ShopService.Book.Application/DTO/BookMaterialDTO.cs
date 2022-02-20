using System;
namespace ShopService.Book.Application.DTO
{
    public class BookMaterialDTO
    {
        public string name { get; set; }
        public DateTime? publishDate { get; set; }
        public Guid? AuthorId { get; set; }
    }
}