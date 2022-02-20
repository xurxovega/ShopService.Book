using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using ShopService.Book.Application.DTO;
using ShopService.Book.Application.Wrapper;
using ShopService.Book.Domain.Entities;
using ShopService.Book.Infraestructure.Persistance.Interface;
using ShopService.Book.Infraestructure.Persistance.Utils;

namespace ShopService.Book.Application.Query
{
    public class GetBookQuery: IRequest<Response<BookMaterialDTO>>
    {
        public string BookGuid;
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Response<BookMaterialDTO>> 
    {
        private readonly IRepositoryBaseAsync<BookMaterial> _repoBookMaterial;
        private readonly IMapper _mapper;
        public GetBookQueryHandler(IRepositoryBaseAsync<BookMaterial> repoBookMaterial, IMapper mapper)
        {
            _repoBookMaterial = repoBookMaterial;
            _mapper = mapper;
        }
        public async Task<Response<BookMaterialDTO>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var record = new BookMaterial();
            var whereBookId = new QueryParameters<BookMaterial>()
            {
                Where = w => w.guid == new System.Guid(request.BookGuid)
            };
            var data = await _repoBookMaterial.FindByAsync(whereBookId); //.Cast<BookMaterial>().FirstOrDefault()
            record = data.Cast<BookMaterial>().FirstOrDefault();
            
            if (record != null)
            {
                return new Response<BookMaterialDTO>(_mapper.Map<BookMaterialDTO>(record));
            }
            else
            {
                return new Response<BookMaterialDTO>($"Book { request.BookGuid} does not find");
            }
        }
    }
}

