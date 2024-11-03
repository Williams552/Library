using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface ILiquidatedBookRepository
    {
        Task<IEnumerable<LiquidatedBook>> getAllLiquidatedBook();
        Task<LiquidatedBook> findLiquidatedBookById(int id);
        Task<(bool, string mess, LiquidatedBook)> createLiquidatedBook(LiquidatedBook liquidatedBook);
        Task<(bool, string mess)> deleteLiquidatedBook(int id);
    }
}
