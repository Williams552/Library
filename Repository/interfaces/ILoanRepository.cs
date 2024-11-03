using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> getAll();
        Task<Loan> findById(int id);
        Task<Loan> create(Loan loan);
        Task<Loan> update(Loan loan);
    }
}
