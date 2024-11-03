using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.interfaces;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookInGroupController : Controller
    {
        private readonly IBookInGroupRepository _bookInGroupRepository;
        public BookInGroupController(IBookInGroupRepository bookInGroupRepository)
        {
            _bookInGroupRepository = bookInGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookInGroup>>> getAllBookInGroups()
        {
            var bookInGroup = await _bookInGroupRepository.getAllBookInGroups();
            return Ok(bookInGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookInGroup>> getBookInGroupById(int id)
        {
            var bookInGroup = await _bookInGroupRepository.getBookInGroupById(id);
            return Ok(bookInGroup);
        }

        [HttpPost]
        public async Task<ActionResult> createBookInGroup(BookInGroup bookInGroup)
        {
            await _bookInGroupRepository.createBookInGroup(bookInGroup);
            return CreatedAtAction(nameof(getBookInGroupById), new { id = bookInGroup.Id }, bookInGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateBookInGroup(int id, BookInGroup bookInGroup)
        {
            if (id != bookInGroup.Id)
            {
                return BadRequest();
            }
            var (success, message, BookInGroup) = await _bookInGroupRepository.updateBookInGroup(bookInGroup);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBookInGroup(int id)
        {
            var (success, message) = await _bookInGroupRepository.deleteBookInGroup(id);
            return Ok(new { message });
        }
    }
}
