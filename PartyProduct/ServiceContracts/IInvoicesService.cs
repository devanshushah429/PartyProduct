using Entities;

namespace ServiceContracts
{
    public interface IInvoicesService
    {
        public Task<List<Invoice>> GetAllInvoices();

        public Task<List<Invoice>> GetInvoicesByPartyID(int partyID);

        public Task<Invoice>? GetInvoiceDetails(int invoiceID);

        public Task<Invoice> AddInvoice(Invoice model);

        //public Task EditInvoice(Invoice model);

        public Task<List<InvoiceWiseProduct>> GetInvoiceWiseProducts(int partyID);
    }
}
