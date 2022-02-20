using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopService.Book.Application.Command;
using ShopService.Book.Application.DTO;
using ShopService.Book.Application.Query;
using ShopService.Book.Domain.Entities;

namespace ShopService.Author.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _meadiator;
        public BookController(IMediator meadiator)
        {
            _meadiator = meadiator;
            // Inyección de Mediator para cualquier Controlador que herede de BaseApiController.
            //protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }

        [HttpPost]
        public async Task<IActionResult> AddOne(CreateBookCommand command)
        {
            return Ok( await _meadiator.Send(command));
        }
        
        [HttpGet]
        [Route("getBooks")]
        public async Task<IActionResult> getBooks()
        {
            return Ok( await _meadiator.Send(new GetBooksQuery()));
        }

        [HttpGet]
        [Route("getBook/{id}")]
        public async Task<IActionResult> getBook(string id)
        {
            return Ok(await _meadiator.Send(new GetBookQuery{BookGuid = id} ));
        }

        [HttpDelete]
        [Route("removeBook/{id}")]
        public async Task<IActionResult> removeBook(int id)
        {
            return Ok( await _meadiator.Send(new RemoveBookCommand{ BookId = id} ));
        }
    }
}