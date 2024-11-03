using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IBookInGroupRepository
    {
        Task<IEnumerable<BookInGroup>> getAllBookInGroups();
        Task<BookInGroup> getBookInGroupById(int id);
        Task<(bool, string mess, BookInGroup)> createBookInGroup(BookInGroup bookInGroup);
        Task<(bool, string mess, BookInGroup)> updateBookInGroup(BookInGroup bookInGroup);
        Task<(bool, string mess)> deleteBookInGroup(int id);
    }
}
