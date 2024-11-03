using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> getAllSuppliers();
        Task<Supplier> getSupplierById(int supplierId);
        Task<(bool, string mess, Supplier)> createSupplier(Supplier supplier);
        Task<(bool, string mess, Supplier)> updateSupplier(Supplier supplier);
        Task<(bool, string mess)> deleteSupplier(int supplierId);
    }
}
