using AutoMapper;
using ShopService.Book.Application.Command;
using ShopService.Book.Domain.Entities;

namespace ShopService.Book.Application.DTO
{
    public class MappingCommands: Profile
    {
        public MappingCommands()
        {
            CreateMap<CreateBookCommand, BookMaterial>();
            CreateMap<BookMaterial, BookMaterialDTO>();
        }
    }
}   