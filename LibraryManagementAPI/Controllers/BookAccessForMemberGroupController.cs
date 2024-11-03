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
    public class BookAccessForMemberGroupController : Controller
    {
        private readonly IBookAccessForMemberGroupRepository _bookAccessForMemberGroupRepository;
        public BookAccessForMemberGroupController(IBookAccessForMemberGroupRepository bookAccessForMemberGroupRepository)
        {
            _bookAccessForMemberGroupRepository = bookAccessForMemberGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookGroup>>> getAllBookAccessForMemberGroup()
        {
            var bookGroup = await _bookAccessForMemberGroupRepository.getAllBookAccessForMemberGroup();
            return Ok(bookGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookGroup>> getBookAccessForMemberGroupById(int id)
        {
            var bookGroup = await _bookAccessForMemberGroupRepository.getBookAccessForMemberGroupById(id);
            return Ok(bookGroup);
        }

        [HttpPost]
        public async Task<ActionResult> createBookAccessForMemberGroup(BookAccessForMemberGroup bookGroup)
        {
            await _bookAccessForMemberGroupRepository.createBookAccessForMemberGroup(bookGroup);
            return CreatedAtAction(nameof(getBookAccessForMemberGroupById), new { id = bookGroup.Id }, bookGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateBookAccessForMemberGroup(int id, BookAccessForMemberGroup bookGroup)
        {
            if (id != bookGroup.GroupId)
            {
                return BadRequest();
            }
            var (success, message, BookGroup) = await _bookAccessForMemberGroupRepository.updateBookAccessForMemberGroup(bookGroup);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBookAccessForMemberGroup(int id)
        {
            var (success, message) = await _bookAccessForMemberGroupRepository.deleteBookAccessForMemberGroup(id);
            return Ok(new { message });
        }
    }
}
