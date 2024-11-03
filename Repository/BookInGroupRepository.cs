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
    public class BookInGroupRepository : IBookInGroupRepository
    {
        private BookInGroupDAO bookInGroupDAO;
        public BookInGroupRepository(BookInGroupDAO bookInGroup)
        {
            bookInGroupDAO = bookInGroup;
        }
        public async Task<IEnumerable<BookInGroup>> getAllBookInGroups() => await bookInGroupDAO.getAllBookInGroups();
        public async Task<BookInGroup> getBookInGroupById(int id) => await bookInGroupDAO.getBookInGroupById(id);
        public async Task<(bool, string mess, BookInGroup)> createBookInGroup(BookInGroup bookInGroup) => await bookInGroupDAO.createBookInGroup(bookInGroup);
        public async Task<(bool, string mess, BookInGroup)> updateBookInGroup(BookInGroup bookInGroup) => await bookInGroupDAO.updateBookGroup(bookInGroup);
        public async Task<(bool, string mess)> deleteBookInGroup(int id) => await bookInGroupDAO.deleteBookInGroup(id);
    }
}
