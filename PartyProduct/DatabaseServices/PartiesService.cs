using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace DatabaseServices
{
    public class PartiesService : IPartiesService
    {
        private readonly AppDbContext _context;
        private readonly IPartyWiseProductsService _partyWiseProductsService;
        public PartiesService(AppDbContext context, IPartyWiseProductsService partyWiseProductsService)
        {
            _context = context;
            _partyWiseProductsService = partyWiseProductsService;
        }

        #region Get All Parties
        public async Task<List<Party>> GetAllPartiesAsync()
        {
            List<Party> list = await _context.Parties.ToListAsync();
            return list;
        }
        #endregion

        #region Get Basic Details
        public async Task<Party>? GetBasicPartyDetailsAsync(int? id)
        {
            Party partyModel = await _context.Parties.FindAsync(id);
            return partyModel;
        }
        #endregion

        #region Get Complete Details of Party
        public async Task<Party>? GetPartyDetailsAsync(int id)
        {
            Party? party = await _context.Parties.FindAsync(id);
            if (party != null)
            {
                party.PartyWiseProducts = await _partyWiseProductsService.GetAllProductOfPartyByID(id);
            }
            return party;
        }
        #endregion

        #region Add the party in party table
        public async Task<Party> AddPartyAsync(Party partyModel)
        {
            partyModel.Created = DateTime.Now;
            partyModel.Modified = DateTime.Now;
            _context.Parties.Add(partyModel);
            await _context.SaveChangesAsync();
            return partyModel;
        }
        #endregion

        #region Edit the party of specific ID
        public async Task<Party?> EditPartyByPartyIDAsync(Party partyModel)
        {
            if (partyModel == null)
            {
                throw new ArgumentNullException(nameof(partyModel), "Party model cannot be null.");
            }

            Party? existingParty = await _context.Parties.FindAsync(partyModel.PartyID);

            if (existingParty == null)
            {
                return null;
            }

            existingParty.PartyName = partyModel.PartyName;
            existingParty.PhoneNumber = partyModel.PhoneNumber;
            existingParty.Email = partyModel.Email;
            existingParty.Modified = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingParty;
        }
        #endregion

        #region Delete the party
        public async Task<bool> DeleteThePartyAsync(int id)
        {

            Party? existingParty = await GetBasicPartyDetailsAsync(id);
            if (existingParty == null)
            {
                return false;
            }

            try
            {
                _context.Parties.Remove(existingParty);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }
        #endregion
    }
}
