using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IBookshelfRepository
    {
        Task<IEnumerable<Bookshelf>> getAllBookshelves();
        Task<Bookshelf> GetBookshelfById(int id);
        Task<Bookshelf> getBookshelvesByShelfNumber(int shelfNumber);
        Task<(bool, string mess, Bookshelf)> createBookshelves(Bookshelf bookshelf);
        Task<(bool, string mess, Bookshelf)> updateBookshelves(Bookshelf bookshelf);
        Task<(bool, string mess)> deleteBookshelves(int shelfNumber);
    }
}
