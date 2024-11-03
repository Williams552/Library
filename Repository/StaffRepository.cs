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
    public class StaffRepository : IStaffRepository
    {
        private StaffDAO staffDAO;
        public StaffRepository(StaffDAO staff)
        {
            staffDAO = staff;
        }
        public async Task<IEnumerable<Staff>> getAllStaff() => await staffDAO.getAllStaff();
        public async Task<Staff> getStaffById(int id) => await staffDAO.getStaffById(id);
        public async Task<(bool success, string mess, Staff)> createStaff(Staff staff) => await staffDAO.createStaff(staff);
        public async Task<(bool success, string mess, Staff)> updateStaff(Staff staff) => await staffDAO.updateStaff(staff);
        public async Task<(bool success, string message)> DeleteStaff(int id) => await staffDAO.DeleteStaff(id);
        public async Task<(bool success, string token, string message)> Login(string username, string password) => await staffDAO.Login(username, password);
    }
}
