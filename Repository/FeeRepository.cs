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
    public class FeeRepository : IFeeRepository
    {
        private FeeDAO feeDAO;
        public FeeRepository(FeeDAO fee)
        {
            feeDAO = fee;
        }
        public async Task<IEnumerable<Fee>> GetAllFees() => await feeDAO.getAllFees();
        public async Task<Fee> getFeeById(int id) => await feeDAO.getFeeById(id);
        public async Task<(bool, string mess, Fee)> createFee(Fee fee) => await feeDAO.createFee(fee);
        public async Task<(bool, string mess, Fee)> updateFee(Fee fee) => await feeDAO.updateFee(fee);
        public async Task<(bool, string mess)> deleteFeeById(int id) => await feeDAO.deleteFee(id);
    }
}
