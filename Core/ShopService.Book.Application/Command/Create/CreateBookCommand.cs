using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ShopService.Book.Application.DTO;
using ShopService.Book.Application.Wrapper;
using ShopService.Book.Domain.Entities;
using ShopService.Book.Infraestructure.Persistance.Interface;

namespace ShopService.Book.Application.Command
{
    public class CreateBookCommand: IRequest<Response<BookMaterialDTO>>
    {
        public string name {get; set;}
        public DateTime? publishDate {get; set;}
        public Guid? AuthorId {get; set;}
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<BookMaterialDTO>>
    {
        private readonly IRepositoryBaseAsync<BookMaterial> _repoBookMaterial;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IRepositoryBaseAsync<BookMaterial> repoBookMaterial, IMapper mapper)
        {
            _repoBookMaterial = repoBookMaterial;
            _mapper = mapper;
        }
        public async Task<Response<BookMaterialDTO>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try{
                var data = _mapper.Map<BookMaterial>(request);
                data.guid =  Guid.NewGuid();
                var newRecord = await _repoBookMaterial.AddAsync(data);
                return new Response<BookMaterialDTO>(_mapper.Map<BookMaterialDTO>(newRecord));
            }
            catch(Exception e){
                throw new KeyNotFoundException($"Error on creating Book. More info: {e.InnerException.Message} .");
            }
        }    
    }
}

