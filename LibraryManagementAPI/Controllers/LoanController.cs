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
    public class LoanController : Controller
    {
        private readonly ILoanRepository _item;

        public LoanController(ILoanRepository item)
        {
            _item = item;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetAll()
        {
            var item = await _item.getAll();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetAuthorById(int id)
        {
            var author = await _item.findById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(Loan author)
        {
            await _item.create(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.LoanId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Loan author)
        {
            if (id != author.LoanId)
            {
                return BadRequest();
            }
            await _item.update(author);
            return Ok();
        }

    }
}
