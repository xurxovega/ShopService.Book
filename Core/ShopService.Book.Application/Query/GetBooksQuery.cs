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

namespace ShopService.Book.Application.Query
{
    public class GetBooksQuery: IRequest<Response<List<BookMaterialDTO>>>{ }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Response<List<BookMaterialDTO>>> 
    {
        private readonly IRepositoryBaseAsync<BookMaterial> _repoBookMaterial;
        private readonly IMapper _mapper;
        public GetBooksQueryHandler(IRepositoryBaseAsync<BookMaterial> repoBookMaterial, IMapper mapper)
        {
            _repoBookMaterial = repoBookMaterial;
            _mapper = mapper;
        }
        public async Task<Response<List<BookMaterialDTO>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var data = await _repoBookMaterial.GetAllAsync();
            var records = _mapper.Map<List<BookMaterialDTO>>(data);
            return new Response<List<BookMaterialDTO>>(records);
        }
    }
}

