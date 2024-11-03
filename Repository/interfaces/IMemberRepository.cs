using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.Common;

namespace Repository.interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> getAllMembers();
        Task<Member> getMemberById(int id);
        Task<(bool success, string mess, Member)> createMember(Member member);
        Task<(bool success, string mess, Member)> updateMember(Member member);
        Task<(bool success, string mess)> deleteMember(int id);
        Task<(bool success, string token, string message)> Login(string username, string password);
    }
}
