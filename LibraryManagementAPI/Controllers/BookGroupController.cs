using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.interfaces;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookGroupController : Controller
    {
        private readonly IBookGroupRepository _bookGroupRepository;
        public BookGroupController(IBookGroupRepository bookGroupRepository)
        {
            _bookGroupRepository = bookGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookGroup>>> getAllBookGroups()
        {
            var bookGroup = await _bookGroupRepository.getAllBookGroups();
            return Ok(bookGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookGroup>> getBookGroupById(int id)
        {
            var bookGroup = await _bookGroupRepository.getBookGroupById(id);
            return Ok(bookGroup);
        }

        [HttpPost]
        public async Task<ActionResult> createBookGroup(BookGroup bookGroup)
        {
            await _bookGroupRepository.createBookGroup(bookGroup);
            return CreatedAtAction(nameof(getBookGroupById), new { id = bookGroup.GroupId }, bookGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateBookGroup(int id,BookGroup bookGroup)
        {
            if (id != bookGroup.GroupId)
            {
                return BadRequest();
            } 
            var (success, message, BookGroup) = await _bookGroupRepository.updateBookGroup(bookGroup);
            return Ok(new {message});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBookGroup(int id)
        {
            var(success, message) = await _bookGroupRepository.deleteBookGroup(id);
            return Ok(new { message });
        }
    }
}
