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
    public class BookRepository : IBookRepository
    {
        private BookDAO bookDAO;
        public BookRepository(BookDAO book)
        {
            bookDAO = book;
        }
        public async Task<IEnumerable<Book>> getAllBook() => await bookDAO.getAllBook();
        public async Task<Book> findBookById(int id) => await bookDAO.findBookById(id);
        public async Task<IEnumerable<Book>> findBook(string name) => await bookDAO.findBook(name);
        public async Task<Book> createBook(Book book) => await bookDAO.createBook(book);
        public async Task<Book> updateBook(Book book) => await bookDAO.updateBook(book);
        public async Task<bool> deleteBook(int id) => await bookDAO.deleteBook(id);
    }
}
