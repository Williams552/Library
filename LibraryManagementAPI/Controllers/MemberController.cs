using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> getAllMember()
        {
            var member = await _memberRepository.getAllMembers();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> getMemberById(int id)
        {
            var member = await _memberRepository.getMemberById(id);
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMember(Member member)
        {
            var result = await _memberRepository.createMember(member);

            if (!result.success)
            {
                return BadRequest(new { message = result.mess });
            }
            return CreatedAtAction(nameof(getMemberById), new { id = member.MemberId }, member);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateMember(int id, Member member)
        {
            if(id != member.MemberId)
            {
                return BadRequest();
            }
            var (success, message, m) = await _memberRepository.updateMember(member);
            return Ok(new {message});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteMember(int id)
        {
            var(success, message) = await _memberRepository.deleteMember(id);
            return Ok(new {message});
        }

        [HttpPost("checkLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var (success, token, message) = await _memberRepository.Login(username, password);

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(new { message, token });
        }
    }
}
