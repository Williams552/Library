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
    public class AuthorRepository : IAuthorRepository
    {
        private AuthorDAO authorDAO;
        public AuthorRepository (AuthorDAO author)
        {
            authorDAO = author;
        }
        public async Task<IEnumerable<Author>> getAllAuthor() => await authorDAO.getAllAuthor();
        public async Task<Author> findAuthorById(int id) => await authorDAO.findAuthorById(id);
        public async Task<IEnumerable<Author>> findAuthor(string name) => await authorDAO.findAuthor(name);
        public async Task createAuthor(Author author) => await authorDAO.createAuthor(author);
        public async Task updateAuthor(Author author) => await authorDAO.updateAuthor(author);
        public async Task deleteAuthor(int id) => await authorDAO.deleteAuthor(id);
    }
}
