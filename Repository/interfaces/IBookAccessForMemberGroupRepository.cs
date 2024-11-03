using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IBookAccessForMemberGroupRepository
    {
        Task<IEnumerable<BookAccessForMemberGroup>> getAllBookAccessForMemberGroup();
        Task<BookAccessForMemberGroup> getBookAccessForMemberGroupById(int id);
        Task<(bool, string mess, BookAccessForMemberGroup)> createBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup);
        Task<(bool, string mess, BookAccessForMemberGroup)> updateBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup);
        Task<(bool, string mess)> deleteBookAccessForMemberGroup(int id);
    }
}
