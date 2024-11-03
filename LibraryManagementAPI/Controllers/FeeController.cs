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
    public class FeeController : Controller
    {
        private readonly IFeeRepository _feeRepository;

        public FeeController(IFeeRepository fee)
        {
            _feeRepository = fee;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fee>>> GetAllFees()
        {
            var fees = await _feeRepository.GetAllFees();
            return Ok(fees);
        }

       [HttpGet("{id}")]
        public async Task<ActionResult<Fee>> getFeeById(int id)
        {
            var fees = await _feeRepository.getFeeById(id);
            return Ok(fees);
        }

        [HttpPost]
        public async Task<ActionResult> createFee(Fee fee)
        {
            await _feeRepository.createFee(fee);
            return CreatedAtAction(nameof(getFeeById), new { id = fee.FeeId }, fee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateFee(int id, Fee fee)
        {
            if (id != fee.FeeId)
            {
                return BadRequest();
            }
            var (success, message, f) = await _feeRepository.updateFee(fee);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteFeeById(int id)
        {
            var (success, message) = await _feeRepository.deleteFeeById(id);
            return Ok(new { message });
        }
    }
}
