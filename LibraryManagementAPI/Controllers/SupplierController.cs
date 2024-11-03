using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.interfaces;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET: SupplierController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> getAllSuppliers()
        {
            var supplier = await _supplierRepository.getAllSuppliers();
            return Ok(supplier);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> getSupplierById(int id)
        {
            var supplier = await _supplierRepository.getSupplierById(id);
            if(supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }


        [HttpPost]
        public async Task<IActionResult> createSupplier(Supplier supplier)
        {
            await _supplierRepository.createSupplier(supplier);
            return CreatedAtAction(nameof(getSupplierById), new { id = supplier.SupplierId }, supplier);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateSupplier(int id, Supplier supplier)
        {
            if(id != supplier.SupplierId)
            {
                return BadRequest();
            }
            var (success, message, s) = await _supplierRepository.updateSupplier(supplier);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteSupplier(int id)
        {
            var (success, message) = await _supplierRepository.deleteSupplier(id);
            return Ok(new { message });
        }
    }
}
