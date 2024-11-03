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
    public class SupplierRepository : ISupplierRepository
    {
        private SupplierDAO supplierDAO;
        public SupplierRepository (SupplierDAO supplier)
        {
            supplierDAO = supplier;
        }
        public async Task<IEnumerable<Supplier>> getAllSuppliers() => await supplierDAO.getAllSuppliers();
        public async Task<Supplier> getSupplierById(int supplierId) => await supplierDAO.getSupplierById(supplierId);
        public async Task<(bool, string mess, Supplier)> createSupplier(Supplier supplier) => await supplierDAO.createSupplier(supplier);
        public async Task<(bool, string mess, Supplier)> updateSupplier(Supplier supplier) => await supplierDAO.updateSupplier(supplier);
        public async Task<(bool, string mess)> deleteSupplier(int supplierId) => await supplierDAO.deleteSupplier(supplierId);
    }
}
