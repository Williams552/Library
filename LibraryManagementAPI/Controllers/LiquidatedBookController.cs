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
    public class LiquidatedBookController : Controller
    {
        private readonly ILiquidatedBookRepository _liquidatedBookRepository;
        public LiquidatedBookController(ILiquidatedBookRepository liquidatedBookRepository)
        {
            _liquidatedBookRepository = liquidatedBookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiquidatedBook>>> getAllLiquidatedBook()
        {
            var member = await _liquidatedBookRepository.getAllLiquidatedBook();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiquidatedBook>> findLiquidatedBookById(int id)
        {
            var member = await _liquidatedBookRepository.findLiquidatedBookById(id);
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> createLiquidatedBook(LiquidatedBook liquidatedBook)
        {
            await _liquidatedBookRepository.createLiquidatedBook(liquidatedBook);
            return CreatedAtAction(nameof(findLiquidatedBookById), new { id = liquidatedBook.LiquidatedId }, liquidatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteMember(int id)
        {
            var (success, message) = await _liquidatedBookRepository.deleteLiquidatedBook(id);
            return Ok(new { message });
        }
    }
}
