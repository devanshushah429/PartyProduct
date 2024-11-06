using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace DatabaseServices
{
    public class PartyWiseProductsService: IPartyWiseProductsService
    {
        private readonly AppDbContext _context;
        public PartyWiseProductsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PartyWiseProduct>> GetAllProductOfPartyByID(int id)
        {

            List<PartyWiseProduct> list = await _context.PartyWiseProducts.Where(p => p.PartyID == id).Include(p => p.Product).ToListAsync();
            //.Join(_context.Products,
            //        partyWiseProduct => partyWiseProduct.ProductID,
            //        product => product.ProductID,
            //        (partyWiseProduct, product) => new PartyWiseProductModel() { Product = product })
            //.ToListAsync();
            return list;
        }

        public void AddPartyWiseProduct(PartyWiseProduct partyWiseProductModel)
        {
            _context.PartyWiseProducts.Add(partyWiseProductModel);
            _context.SaveChanges();
        }

        public async Task<List<Product?>> GetProductIDsOfParty(int partyID)
        {
            List<Product> availableProducts = await _context.Products
                .Where(product => !_context.PartyWiseProducts
                .Where(partyWiseProduct => partyWiseProduct.PartyID == partyID)
                .Select(partyWiseProduct => partyWiseProduct.ProductID)
                .Contains(product.ProductID))
                .ToListAsync();
            return availableProducts;
        }
    }
}
