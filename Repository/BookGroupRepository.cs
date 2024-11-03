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
    public class BookGroupRepository : IBookGroupRepository
    {
        private BookGroupDAO bookGroupDAO;
        public BookGroupRepository(BookGroupDAO bookGroup)
        {
            bookGroupDAO = bookGroup;
        }
        public async Task<IEnumerable<BookGroup>> getAllBookGroups() => await bookGroupDAO.getAllBookGroups();
        public async Task<BookGroup> getBookGroupById(int id) => await bookGroupDAO.getBookGroupById(id);
        public async Task<(bool, string mess, BookGroup)> createBookGroup(BookGroup bookGroup) => await bookGroupDAO.createBookGroup(bookGroup);
        public async Task<(bool, string mess, BookGroup)> updateBookGroup(BookGroup bookGroup) => await bookGroupDAO.updateBookGroup(bookGroup);
        public async Task<(bool, string mess)> deleteBookGroup(int id) => await bookGroupDAO.deleteBookGroup(id);

    }
}
