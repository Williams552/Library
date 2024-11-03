using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public  interface IFeeRepository
    {
        Task<IEnumerable<Fee>> GetAllFees();
        Task<Fee> getFeeById(int id);
        Task<(bool, string mess, Fee)> createFee(Fee fee);
        Task<(bool, string mess, Fee)> updateFee(Fee fee);
        Task<(bool, string mess)> deleteFeeById(int id);
    }
}
