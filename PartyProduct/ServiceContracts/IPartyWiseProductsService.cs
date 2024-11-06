using Entities;

namespace ServiceContracts
{
    public interface IPartyWiseProductsService
    {
        public Task<List<PartyWiseProduct>> GetAllProductOfPartyByID(int id);

        public void AddPartyWiseProduct(PartyWiseProduct partyWiseProductModel);

        public Task<List<Product?>> GetProductIDsOfParty(int partyID);
    }
}
