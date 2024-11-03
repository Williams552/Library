using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.interfaces;
using DataAccess.DAOs;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryDAO categoryDAO;
        public CategoryRepository(CategoryDAO category)
        {
            this.categoryDAO = category;
        }
        public async Task<IEnumerable<Category>> GetAllCategory() => await categoryDAO.getAllCategories();
        public async Task<Category> GetCategoryById(int id) => await categoryDAO.getCategoryById(id);
        public async Task<(bool, string mess, Category)> createCategory(Category category) => await categoryDAO.addCategory(category);
        public async Task<(bool, string mess, Category)> updateCategory(Category category) => await categoryDAO.updateCategory(category);
        public async Task<(bool, string mess)> deleteCategory(int id) => await categoryDAO.deleteCategory(id);
    }
}
