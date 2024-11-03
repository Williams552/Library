using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAOs;
using Repository.interfaces;

namespace Repository
{
    public class MemberGroupRepository : IMemberGroupRepository
    {
        private MemberGroupDAO memberGroupDAO;
        public MemberGroupRepository(MemberGroupDAO member)
        {
            memberGroupDAO = member;
        }
        public async Task<IEnumerable<MemberGroup>> getAllMemberGroup() => await memberGroupDAO.getAllMemberGroup();
        public async Task<MemberGroup> getMemberGroupById(int id) => await memberGroupDAO.getMemberGroupById(id);
        public async Task<(bool success, string mess, MemberGroup)> createMemberGroup(MemberGroup member) => await memberGroupDAO.createMemberGroup(member);
        public async Task<(bool success, string mess, MemberGroup)> updateMemberGroup(MemberGroup member) => await memberGroupDAO.updateMemberGroup(member);
        public async Task<(bool success, string message)> DeleteMemberGroup(int id) => await memberGroupDAO.DeleteMemberGroup(id);
    }
}
