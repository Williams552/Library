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
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> getAllStaff()
        {
            var member = await _staffRepository.getAllStaff();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> getStaffById(int id)
        {
            var member = await _staffRepository.getStaffById(id);
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> createStaff(Staff staff)
        {
            await _staffRepository.createStaff(staff);
            return CreatedAtAction(nameof(getStaffById), new { id = staff.StaffId }, staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateStaff(int id, Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest();
            }
            var (success, message, m) = await _staffRepository.updateStaff(staff);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteStaff(int id)
        {
            var (success, message) = await _staffRepository.DeleteStaff(id);
            return Ok(new { message });
        }

        [HttpPost("checkLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var (success, token, message) = await _staffRepository.Login(username, password);

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(new { message, token });
        }
    }
}
