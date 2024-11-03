using Models;
using DataAccess.DAOs;
using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoanRepository : ILoanRepository
    {
        private LoanDAO itemDAO;
        public LoanRepository(LoanDAO item)
        {
            itemDAO = item;
        }
        public async Task<IEnumerable<Loan>> getAll() => await itemDAO.getAll();
        public async Task<Loan> findById(int id) => await itemDAO.findById(id);
        public async Task<Loan> create(Loan loan) => await itemDAO.create(loan);
        public async Task<Loan> update(Loan loan) => await itemDAO.update(loan);
    }
}
