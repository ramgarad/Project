using Product_Management.Models;

namespace Product_Management.Repository.Interface_Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<List<Category>> GetCategories(int pageNumber, int pageSize);
        Task<int> GetTotalCategoryCount();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
