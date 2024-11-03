using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IBookGroupRepository
    {
        Task<IEnumerable<BookGroup>> getAllBookGroups();
        Task<BookGroup> getBookGroupById(int id);
        Task<(bool, string mess, BookGroup)> createBookGroup (BookGroup bookGroup);
        Task<(bool, string mess, BookGroup)> updateBookGroup (BookGroup bookGroup);
        Task<(bool, string mess)> deleteBookGroup (int id);
    }
}
