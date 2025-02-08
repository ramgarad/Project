using Microsoft.EntityFrameworkCore;
using Product_Management.Context;
using Product_Management.Models;
using Product_Management.Repository.Interface_Repository;

namespace Product_Management.Repository.Interface_Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize)
        {          
                var products = await _context.Products
                    .Include(p => p.Category)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new Product
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.CategoryName 
                    })
                    .ToListAsync();

                return products;           
        }

        public async Task<int> GetTotalProductCount()
        {

            return await _context.Products.CountAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
