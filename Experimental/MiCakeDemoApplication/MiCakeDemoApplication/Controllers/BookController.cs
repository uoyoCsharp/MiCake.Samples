using MiCake.AspNetCore;
using MiCake.Core;
using MiCake.Core.DependencyInjection;
using MiCake.DDD.Domain;
using MiCakeDemoApplication.Domain.BookBoundary.Aggregates;
using MiCakeDemoApplication.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookController(IRepository<Book, Guid> repository)
        {
            _bookRepository = repository;
        }

        [HttpGet]
        public async Task<Book> GetBook(Guid bookId)
        {
            var s = ServiceLocator.Instance.GetSerivce<IOptions<MiCakeAspNetOptions>>();
            return await _bookRepository.FindAsync(bookId);
        }

        [HttpPost]
        public async Task AddBook([FromBody]AddBookDto bookDto)
            => await _bookRepository.AddAsync(new Book(bookDto.BookName, bookDto.AuthorFirstName, bookDto.AuthroLastName));

        [HttpPost]
        public async Task<bool> ChangeAuthor([FromBody]ChangeBookAuthorDto bookDto)
        {
            var _bookInfo = await _bookRepository.FindAsync(bookDto.BookID)
                                ?? throw new SoftlyMiCakeException("未找到对应书籍信息");

            _bookInfo.ChangeAuthor(bookDto.AuthorFirstName, bookDto.AuthorLastName);
            await _bookRepository.UpdateAsync(_bookInfo);

            return true;
        }
    }
}
