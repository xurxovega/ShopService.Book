using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ShopService.Book.Application.Wrapper;
using ShopService.Book.Domain.Entities;
using ShopService.Book.Infraestructure.Persistance.Interface;

namespace ShopService.Book.Application.Command
{
    public class RemoveBookCommand: IRequest<Response<string>>
    {
        public int BookId { get; set; }
    }

    public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand, Response<string>>
    {
        private readonly IRepositoryBaseAsync<BookMaterial> _repoBookMaterial;
        private readonly IMapper _mapper;
        public RemoveBookCommandHandler(IRepositoryBaseAsync<BookMaterial> repoBookMaterial, IMapper mapper)
        {
            _repoBookMaterial = repoBookMaterial;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var data = await _repoBookMaterial.RemoveAsync(request.BookId);
            if (data == null || data.id == 0){
                throw new KeyNotFoundException($"Book {request.BookId} does not remove");
            }else
            {
                return new Response<string>(request.BookId.ToString(), $"Book {request.BookId} has removed");
            }
        }
    }
}

