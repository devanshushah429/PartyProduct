using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;


namespace DatabaseServices
{
    public class InvoicesService : IInvoicesService
    {
        private readonly AppDbContext _context;
        private readonly IProductsService _productsService;
        public InvoicesService(AppDbContext context, IProductsService productsService)
        {
            _context = context;
            _productsService = productsService;
        }

        #region Get All Invoices
        public async Task<List<Invoice>> GetAllInvoices()
        {
            List<Invoice> list = await _context.Invoices.ToListAsync();
            return list;
        }
        #endregion

        #region Get Invoices Of Specific Party Using Party ID
        public async Task<List<Invoice>> GetInvoicesByPartyID(int partyID)
        {
            List<Invoice> list = await _context.Invoices.Where(invoice => invoice.PartyID == partyID).ToListAsync();
            return list;

        }
        #endregion

        #region Get Invoice Details By Invoice ID
        public async Task<Invoice>? GetInvoiceDetails(int invoiceID)
        {
            Invoice? invoiceModel = await _context.Invoices
                .Include(i => i.InvoiceWiseProducts)!
                .ThenInclude(iwp => iwp.Product)
                .Include(i => i.Party)
                .FirstOrDefaultAsync(i => i.InvoiceID == invoiceID);

            //List<InvoiceWiseProductModel> invoiceWiseProductModels = await _context.InvoiceWiseProducts.ToListAsync();
            return invoiceModel;
        }
        #endregion

        #region Add Invoice
        public async Task<Invoice> AddInvoice(Invoice model)
        {
            model.Created = DateTime.Now;
            model.Modified = DateTime.Now;
            List<InvoiceWiseProduct> invoiceWiseProducts = model.InvoiceWiseProducts;
            List<InvoiceWiseProduct> selectedList = invoiceWiseProducts.Where(invoiceWiseProducts => invoiceWiseProducts.IsSelected).ToList();
            Decimal? totalPrice = 0;
            foreach (InvoiceWiseProduct iwp in selectedList)
            {
                Product product = await _productsService.GetProductDetails(iwp.ProductID);
                iwp.InvoiceID = model.InvoiceID;
                iwp.Subtotal = iwp.Quantity * product.ProductPrice;
                totalPrice += iwp.Subtotal;
            }
            model.InvoiceWiseProducts = selectedList;
            model.TotalPrice = totalPrice;
            _context.Invoices.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Edit Invoice
        //public async Task EditInvoice(Invoice model)
        //{

        //}
        #endregion

        #region Get All Invoice of Specific Party
        public async Task<List<InvoiceWiseProduct>> GetInvoiceWiseProducts(int partyID)
        {
            List<PartyWiseProduct> partyWiseProducts = await _context.PartyWiseProducts
                                        .Where(x => x.PartyID == partyID)
                                        .ToListAsync();

            List<InvoiceWiseProduct> invoiceWiseProducts = new List<InvoiceWiseProduct>();

            foreach (var partyProduct in partyWiseProducts)
            {
                Product productDetails = await _productsService.GetProductDetails(partyProduct.ProductID);
                invoiceWiseProducts.Add(new InvoiceWiseProduct
                {
                    Product = productDetails,
                    ProductID = partyProduct.ProductID
                });
            }

            return invoiceWiseProducts;
        }
        #endregion
    }
}
