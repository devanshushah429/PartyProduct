using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace DatabaseServices
{
    public class InvoiceWiseProductsService : IInvoiceWiseProductsService
    {
        private readonly AppDbContext _context;
        private readonly IProductsService _productsService;
        public InvoiceWiseProductsService(AppDbContext context, IProductsService productsService)
        {
            _context = context;
            _productsService = productsService;
        }

        #region Get The List of product ID to exclude
        public async Task<List<int?>> GetAllID(int invoiceID)
        {
            List<int?> ids = await _context.InvoiceWiseProducts.Where(iwp => iwp.InvoiceID == invoiceID).Select(iwp => iwp.ProductID).ToListAsync();
            return ids;
        }
        #endregion

        #region Add Invoice Wise Product
        public async Task AddInvoiceWiseProduct(InvoiceWiseProduct model)
        {
            Product? productModel = await _productsService.GetProductDetails(model.ProductID);
            model.Subtotal = model.Quantity * productModel.ProductPrice;
            _context.InvoiceWiseProducts.Add(model);
            Invoice? invoiceModel = await _context.Invoices.FindAsync(model.InvoiceID);
            invoiceModel.TotalPrice += model.Subtotal;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Edit Invoice Product 
        public async Task EditInvoiceWiseProduct(InvoiceWiseProduct model)
        {
            Product? productModel = await _productsService.GetProductDetails(model.ProductID);

            model.Subtotal = model.Quantity * productModel.ProductPrice;

            InvoiceWiseProduct? existingProduct = await _context.InvoiceWiseProducts
                .FirstOrDefaultAsync(iwp => iwp.InvoiceWiseProductID == model.InvoiceWiseProductID);

            Invoice? invoiceModel = await _context.Invoices.FindAsync(existingProduct.InvoiceID);
            invoiceModel.TotalPrice += (model.Subtotal - existingProduct.Subtotal);



            existingProduct.Quantity = model.Quantity;

            existingProduct.Subtotal = model.Subtotal;
            _context.InvoiceWiseProducts.Update(existingProduct);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete Invoice Wise Product
        public async Task<int?> DeleteInvoiceWiseProduct(int invoiceWiseProductID)
        {
            InvoiceWiseProduct? model = await GetDetails(invoiceWiseProductID);
            Invoice? invoiceModel = await _context.Invoices.FindAsync(model.InvoiceID);
            invoiceModel.TotalPrice -= model.Subtotal;
            _context.InvoiceWiseProducts.Remove(model);
            await _context.SaveChangesAsync();
            return model.InvoiceID;
        }
        #endregion

        #region Get Details
        public async Task<InvoiceWiseProduct> GetDetails(int invoiceWiseProductID)
        {
            InvoiceWiseProduct? existingProduct = await _context.InvoiceWiseProducts
                .FirstOrDefaultAsync(iwp => iwp.InvoiceWiseProductID == invoiceWiseProductID);
            existingProduct.Product = await _productsService.GetProductDetails(existingProduct.ProductID);
            return existingProduct;
        }






        #endregion
    }
}
