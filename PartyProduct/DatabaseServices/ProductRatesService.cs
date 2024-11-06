using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace DatabaseServices
{
    public class ProductRatesService : IProductRatesService
    {
        private readonly AppDbContext _context;

        public ProductRatesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductRate>> GetAllProductRates()
        {
            List<ProductRate> list = await _context.ProductRates
                .Include(pr=>pr.Product)
                .ToListAsync();

            return list;
        }

        public async Task AddProductRate(Product product)
        {
            _context.ProductRates.Add(new ProductRate
            {
                ProductID = product.ProductID,
                ProductPrice = product.ProductPrice,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                PriceAppliedDate = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }
    }
}
