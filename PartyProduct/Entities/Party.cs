using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Party
    {
        [Key]
        public int? PartyID { get; set; }

        public string? PartyName { get; set; } 

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public List<PartyWiseProduct>? PartyWiseProducts { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
