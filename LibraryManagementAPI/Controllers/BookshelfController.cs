using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.interfaces;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookshelfController : Controller
    {
        private readonly IBookshelfRepository _bookshelfRepository;
        public BookshelfController(IBookshelfRepository bookshelfRepository)
        {
            _bookshelfRepository = bookshelfRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookshelf>>> getAllBookshelves()
        {
            var bookshelves = await _bookshelfRepository.getAllBookshelves();
            return Ok(bookshelves);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bookshelf>> getBookshelfById(int id)
        {
            var bookshelvess = await _bookshelfRepository.GetBookshelfById(id);
            if (bookshelvess == null)
            {
                return NotFound();
            }
            return Ok(bookshelvess);
        }

        [HttpGet("getShelvesByNumber/{ShelfNumber}")]
        public async Task<ActionResult<Bookshelf>> getBookshelvesByShelfNumber(int ShelfNumber)
        {
            var bookshelvess = await _bookshelfRepository.getBookshelvesByShelfNumber(ShelfNumber);
            if(bookshelvess == null) { return NotFound(); }
            return Ok(bookshelvess);
        }

        [HttpPost]
        public async Task<IActionResult> createBookshelves(Bookshelf bookshelve)
        {
            await _bookshelfRepository.createBookshelves(bookshelve);
            return CreatedAtAction(nameof(getBookshelfById), new { id = bookshelve.ShelfId }, bookshelve);
        }

        // POST: BookshelfController/Create
        [HttpPut("{id}")]
        public async Task<IActionResult> updateBookshelf(int id, Bookshelf bookshelf)
        {
            if(id != bookshelf.ShelfId)
            {
                return BadRequest();
            }
            var (success, message, shelf) = await _bookshelfRepository.updateBookshelves(bookshelf);
            return Ok( new {message});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBookshelfByShelfNumber(int id)
        {
            var (success, message) = await _bookshelfRepository.deleteBookshelves(id);
            return Ok(new { message });
        }
    }
}
