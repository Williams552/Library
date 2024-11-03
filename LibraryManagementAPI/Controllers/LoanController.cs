using DataAccess.DAOs;
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
        public async Task<ActionResult<Loan>> GetLoanById(int id)
        {
            var loan = await _item.findById(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateLoan(Loan loan)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Trả về lỗi chi tiết của ModelState
        //    }
        //    await _item.create(loan);
        //    return CreatedAtAction(nameof(GetLoanById), new { id = loan.LoanId }, loan);
        //}
        [HttpPost]
        public async Task<ActionResult> CreateLoan([FromBody] LoanDTO loanDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loan = new Loan
            {
                UserId = loanDto.UserId,
                CopyId = loanDto.CopyId,
                LoanDate = loanDto.LoanDate,
                ReturnDate = loanDto.ReturnDate,
                DueDate = loanDto.DueDate,
                Fine = loanDto.Fine,
                BorrowFee = loanDto.BorrowFee,
                Status = loanDto.Status
            };

            await _item.create(loan);
            return CreatedAtAction(nameof(GetLoanById), new { id = loan.LoanId }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, Loan loan)
        {
            //if (id != loan.LoanId)
            //{
            //    return BadRequest();
            //}
            await _item.update(loan);
            return Ok();
        }

    }
}
