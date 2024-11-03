using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> getAllAuthor();
        Task<Author> findAuthorById(int id);
        Task<IEnumerable<Author>> findAuthor(string name);
        Task createAuthor(Author author);
        Task updateAuthor(Author author);
        Task deleteAuthor(int id);
    }
}
