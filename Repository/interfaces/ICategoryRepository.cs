using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task<(bool, string mess, Category)> createCategory(Category category);
        Task<(bool, string mess, Category)> updateCategory(Category category);
        Task<(bool, string mess)> deleteCategory(int id);
    }
}
