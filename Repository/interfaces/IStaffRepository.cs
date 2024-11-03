using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> getAllStaff();
        Task<Staff> getStaffById(int id);
        Task<(bool success, string mess, Staff)> createStaff(Staff staff);
        Task<(bool success, string mess, Staff)> updateStaff(Staff staff);
        Task<(bool success, string message)> DeleteStaff(int id);
        Task<(bool success, string token, string message)> Login(string username, string password);
    }
}
