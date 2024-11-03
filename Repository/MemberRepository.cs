using DataAccess.DAOs;
using Models;
using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MemberRepository : IMemberRepository
    {
        private MemberDAO memberDAO;
        public MemberRepository(MemberDAO member)
        {
            memberDAO = member;
        }
        public async Task<IEnumerable<Member>> getAllMembers() => await memberDAO.getAllMembers();
        public async Task<Member> getMemberById(int id) => await memberDAO.getMemberById(id);
        public async Task<(bool success, string mess, Member)> createMember(Member member) => await memberDAO.createMember(member);
        public async Task<(bool success, string mess, Member)> updateMember(Member member) => await memberDAO.updateMember(member);
        public async Task<(bool success, string mess)> deleteMember(int id) => await memberDAO.DeleteMember(id);
        public async Task<(bool success, string token, string message)> Login(string username, string password) => await memberDAO.Login(username, password);
    }
}
