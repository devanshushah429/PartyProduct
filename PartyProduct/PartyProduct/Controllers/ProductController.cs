using PartyProduct.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Entities;

namespace PartyProduct.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        #region List all the products
        [Route("Product")]
        [Route("Products")]
        [HttpGet]
        public async Task<IActionResult> Index(string searchName, string minPrice, string maxPrice, string sortOrder)
        {
            List<Product> products = await _productsService.GetAllProducts();

            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(p => p.ProductName.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(minPrice) && decimal.TryParse(minPrice, out decimal priceMin))
            {
                products = products.Where(p => p.ProductPrice >= priceMin).ToList();
            }

            if (!string.IsNullOrEmpty(maxPrice) && decimal.TryParse(maxPrice, out decimal priceMax))
            {
                products = products.Where(p => p.ProductPrice <= priceMax).ToList();
            }
            ViewBag.ProductName = searchName;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            // Apply sorting based on the selected order
            ViewBag.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.PriceSort = sortOrder == "Price" ? "price_desc" : "Price";

            
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName).ToList();
                    break;
                case "Price":
                    products = products.OrderBy(p => p.ProductPrice).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.ProductPrice).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName).ToList();
                    break;
            }
            return View(products);
        }


        #endregion

        #region Show Details of Product
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Product? productModel = await _productsService.GetProductDetails(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }
        #endregion

        #region Show Form to create new Product or Edit the Product of given id
        [HttpGet]
        public async Task<IActionResult> AddEditProduct(int? id)
        {
            Product? productModel = new Product();
            if (id != null)
            {
                productModel = await _productsService.GetProductDetails(id);
            }
            return View("AddEditPage", productModel);
        }
        #endregion

        #region Add the new product or edit
        [HttpPost]
        public async Task<IActionResult> AddEditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == null)
                {
                    // Case 1: Add new product
                    await _productsService.AddProduct(product);
                }
                else
                {
                    await _productsService.EditProduct(product);
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }
        #endregion

        #region Open the Confirm Delete Page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _productsService.GetProductDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        #endregion

        #region Handle POST request to delete a product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productsService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
