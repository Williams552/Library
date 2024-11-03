using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> getAllBook();
        Task<Book> findBookById(int id);
        Task<IEnumerable<Book>> findBook(string name);
        Task<Book> createBook(Book book);
        Task<Book> updateBook(Book book);
        Task<bool> deleteBook(int id);
    }
}
