using Entities;

namespace ServiceContracts
{
    public interface IInvoiceWiseProductsService
    {
        public Task<List<int?>> GetAllID(int invoiceID);

        public Task AddInvoiceWiseProduct(InvoiceWiseProduct model);
        
        public Task EditInvoiceWiseProduct(InvoiceWiseProduct model);
        
        public Task<int?> DeleteInvoiceWiseProduct(int invoiceWiseProductID);
        
        public Task<InvoiceWiseProduct> GetDetails(int invoiceWiseProductID);
    }
}
