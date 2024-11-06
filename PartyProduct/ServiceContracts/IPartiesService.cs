using Entities;

namespace ServiceContracts
{
    public interface IPartiesService
    {
        public Task<List<Party>> GetAllPartiesAsync();

        public Task<Party>? GetBasicPartyDetailsAsync(int? id);
        
        public Task<Party>? GetPartyDetailsAsync(int id);
        
        public Task<Party> AddPartyAsync(Party partyModel);
        
        public Task<Party?> EditPartyByPartyIDAsync(Party partyModel);
        
        public Task<bool> DeleteThePartyAsync(int id);
    }
}
