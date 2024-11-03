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
    public class MemberGroupController : Controller
    {
        private readonly IMemberGroupRepository _memberGroupRepository;
        public MemberGroupController(IMemberGroupRepository memberGroupRepository)
        {
            _memberGroupRepository = memberGroupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberGroup>>> getAllMemberGroup()
        {
            var member = await _memberGroupRepository.getAllMemberGroup();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberGroup>> getMemberByIdGroup(int id)
        {
            var member = await _memberGroupRepository.getMemberGroupById(id);
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> createMemberGroup(MemberGroup member)
        {
            await _memberGroupRepository.createMemberGroup(member);
            return CreatedAtAction(nameof(getMemberByIdGroup), new { id = member.GroupId }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateMemberGroup(int id, MemberGroup member)
        {
            if (id != member.GroupId)
            {
                return BadRequest();
            }
            var (success, message, m) = await _memberGroupRepository.updateMemberGroup(member);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteMemberGroup(int id)
        {
            var (success, message) = await _memberGroupRepository.DeleteMemberGroup(id);
            return Ok(new { message });
        }
    }
}
