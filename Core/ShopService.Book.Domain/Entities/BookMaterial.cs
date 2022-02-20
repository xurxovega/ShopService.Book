using System;
namespace ShopService.Book.Domain.Entities
{
    public class BookMaterial
    {
        public Guid guid { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? publishDate { get; set; }
        public Guid? AuthorId {get; set;}
    }
}