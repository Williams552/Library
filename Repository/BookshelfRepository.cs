using System;
using Models;
using DataAccess.DAOs;
using Repository.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookshelfRepository : IBookshelfRepository
    {
        private BookshelfDAO bookshelfDAO;
        public BookshelfRepository(BookshelfDAO bookshelf)
        {
            bookshelfDAO = bookshelf;
        }
        public async Task<IEnumerable<Bookshelf>> getAllBookshelves() => await bookshelfDAO.getAllBookshelves();
        public async Task<Bookshelf> GetBookshelfById(int id) => await bookshelfDAO.getBookshelfById(id);
        public async Task<Bookshelf> getBookshelvesByShelfNumber(int shelfNumber) => await bookshelfDAO.getBookshelvesByShelfNumber(shelfNumber);
        public async Task<(bool, string mess, Bookshelf)> createBookshelves(Bookshelf bookshelf) => await bookshelfDAO.createBookshelves(bookshelf);
        public async Task<(bool, string mess, Bookshelf)> updateBookshelves(Bookshelf bookshelf) => await bookshelfDAO.updateBookshelf(bookshelf);
        public async Task<(bool, string mess)> deleteBookshelves(int shelfNumber) => await bookshelfDAO.deleteBookshelfByShelfNumber(shelfNumber);
    }
}
