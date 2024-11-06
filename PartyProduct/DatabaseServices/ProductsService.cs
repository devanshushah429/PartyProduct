using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace DatabaseServices
{
    public class ProductsService : IProductsService
    {
        private readonly AppDbContext _context;
        private readonly IProductRatesService _productRatesService;

        public ProductsService (AppDbContext context, IProductRatesService productRatesService)
        {
            _context = context;
            _productRatesService = productRatesService;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            DateTime today = DateTime.Now;

            List<Product> products = await _context.Products
                .GroupJoin(
                    _context.ProductRates
                        .Where(productRate => productRate.PriceAppliedDate <= today),
                    product => product.ProductID,
                    productRate => productRate.ProductID,
                    (product, productRate) => new
                    {
                        Product = product,
                        LatestRate = productRate
                            .OrderByDescending(productRate => productRate.PriceAppliedDate)
                            .FirstOrDefault()
                    })
                .Select(productRate => new Product
                {
                    ProductID = productRate.Product.ProductID,
                    ProductName = productRate.Product.ProductName,
                    ProductPrice = productRate.LatestRate != null ? productRate.LatestRate.ProductPrice : 0,
                    Description = productRate.Product.Description,
                    Created = productRate.Product.Created,
                    Modified = productRate.Product.Modified
                })
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetProductDetails(int? productID)
        {
            Product? productModel = await _context.Products
                .Where(product => product.ProductID == productID)
                .GroupJoin(
                    _context.ProductRates
                        .Where(pr => pr.PriceAppliedDate <= DateTime.Now),
                    product => product.ProductID,
                    productRate => productRate.ProductID,
                    (product, productRates) => new
                    {
                        Product = product,
                        LatestRate = productRates
                            .OrderByDescending(pr => pr.PriceAppliedDate)
                            .FirstOrDefault()
                    })
                .Select(pr => new Product
                {
                    ProductID = pr.Product.ProductID,
                    ProductName = pr.Product.ProductName,
                    ProductPrice = pr.LatestRate != null ? pr.LatestRate.ProductPrice : 0,
                    Description = pr.Product.Description,
                    Created = pr.Product.Created,
                    Modified = pr.Product.Modified
                })
                .FirstOrDefaultAsync();

            return productModel;
        }

        public async Task<bool> DeleteProduct(int productID)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task AddProduct(Product product)
        {
            product.Created = DateTime.Now;
            product.Modified = DateTime.Now;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await _productRatesService.AddProductRate(product);
        }

        public async Task EditProduct(Product product)
        {
            Product? existingProduct = await _context.Products.FindAsync(product.ProductID);
            if (existingProduct != null)
            {
                if (existingProduct.ProductPrice != product.ProductPrice)
                {
                    await _productRatesService.AddProductRate(product);
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Modified = DateTime.Now;

                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
