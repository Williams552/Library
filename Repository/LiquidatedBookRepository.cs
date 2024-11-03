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
    public class LiquidatedBookRepository : ILiquidatedBookRepository
    {
        private LiquidatedBookDAO liquidatedBookDAO;
        public LiquidatedBookRepository(LiquidatedBookDAO liquidatedBook)
        {
            liquidatedBookDAO = liquidatedBook;
        }
        public async Task<IEnumerable<LiquidatedBook>> getAllLiquidatedBook() => await liquidatedBookDAO.getAllLiquidatedBook();
        public async Task<LiquidatedBook> findLiquidatedBookById(int id) => await liquidatedBookDAO.findLiquidatedBookById(id);
        public async Task<(bool, string mess, LiquidatedBook)> createLiquidatedBook(LiquidatedBook liquidatedBook) => await liquidatedBookDAO.createLiquidatedBook(liquidatedBook);
        public async Task<(bool, string mess)> deleteLiquidatedBook(int id) => await liquidatedBookDAO.deleteLiquidatedBook(id);
    }
}
