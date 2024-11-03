using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        public BookController(IBookRepository Repository)
        {
            _repository = Repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> getAllBook()
        {
            var authors = await _repository.getAllBook();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> findBookById(int id)
        {
            var author = await _repository.findBookById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Author>>> findBook([FromQuery] string item)
        {
            var authors = await _repository.findBook(item);
            return Ok(authors);
        }

        [HttpPost]
        public async Task<ActionResult> createBook(Book item)
        {
            await _repository.createBook(item);
            return CreatedAtAction(nameof(findBookById), new { id = item.AuthorId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateBook(int id, Book item)
        {
            if (id != item.AuthorId)
            {
                return BadRequest();
            }
            await _repository.updateBook(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBook(int id)
        {
            await _repository.deleteBook(id);
            return Ok();
        }
    }
}
