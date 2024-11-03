using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> getAllCategory()
        {
            var cat = await _categoryRepository.GetAllCategory();
            return Ok(cat);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> getCategoryById(int id)
        {
            var cat = await _categoryRepository.GetCategoryById(id);
            return Ok(cat);
        }

        [HttpPost]
        public async Task<ActionResult> createCategory(Category category)
        {
            await _categoryRepository.createCategory(category);
            return CreatedAtAction(nameof(getCategoryById), new {id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateCategory(int id, Category category)
        {
            if(id != category.CategoryId)
            {
                return BadRequest();
            }
            var (success, message, cat) = await _categoryRepository.updateCategory(category);
            return Ok( new {message});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCategory(int id)
        {
            var (success, message) = await _categoryRepository.deleteCategory(id);
            return Ok(new { message });
        }
    }
}
