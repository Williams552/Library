using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IMemberGroupRepository
    {
        Task<IEnumerable<MemberGroup>> getAllMemberGroup();
        Task<MemberGroup> getMemberGroupById(int id);
        Task<(bool success, string mess, MemberGroup)> createMemberGroup(MemberGroup member);
        Task<(bool success, string mess, MemberGroup)> updateMemberGroup(MemberGroup member);
        Task<(bool success, string message)> DeleteMemberGroup(int id);
    }
}
