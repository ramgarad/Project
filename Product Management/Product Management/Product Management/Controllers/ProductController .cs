using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Management.Models;
using Product_Management.Repository.Interface_Repository;

namespace Product_Management.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProducts(pageNumber, pageSize);
            int totalProducts = await _productRepository.GetTotalProductCount();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryRepository.GetCategories(), "CategoryId", "CategoryName");
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(await _categoryRepository.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryRepository.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
