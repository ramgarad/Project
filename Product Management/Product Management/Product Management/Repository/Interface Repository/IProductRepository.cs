using Product_Management.Models;

namespace Product_Management.Repository.Interface_Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize);
        Task<int> GetTotalProductCount();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
