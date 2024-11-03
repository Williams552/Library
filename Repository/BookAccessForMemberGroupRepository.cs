using DataAccess.DAOs;
using Models;
using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookAccessForMemberGroupRepository : IBookAccessForMemberGroupRepository
    {
        private BookAccessForMemberGroupDAO bookAccessForMemberGroupDAO;
        public BookAccessForMemberGroupRepository(BookAccessForMemberGroupDAO bookAccessForMemberGroup)
        {
            bookAccessForMemberGroupDAO = bookAccessForMemberGroup;
        }
        public async Task<IEnumerable<BookAccessForMemberGroup>> getAllBookAccessForMemberGroup() => await bookAccessForMemberGroupDAO.getAllBookAccessForMemberGroup();
        public async Task<BookAccessForMemberGroup> getBookAccessForMemberGroupById(int id) => await bookAccessForMemberGroupDAO.getBookAccessForMemberGroupById(id);
        public async Task<(bool, string mess, BookAccessForMemberGroup)> createBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup) => await bookAccessForMemberGroupDAO.createBookAccessForMemberGroup(bookAccessForMemberGroup);
        public async Task<(bool, string mess, BookAccessForMemberGroup)> updateBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup) => await bookAccessForMemberGroupDAO.updateBookAccessForMemberGroup(bookAccessForMemberGroup);
        public async Task<(bool, string mess)> deleteBookAccessForMemberGroup(int id) => await bookAccessForMemberGroupDAO.deleteBookAccessForMemberGroup(id);
    }
}
