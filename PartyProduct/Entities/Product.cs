using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    public class Product
    {
        [Key]
        public int? ProductID { get; set; }

        public string? ProductName { get; set; }
        
        [NotMapped]
        public decimal? ProductPrice { get; set; }

        public string? Description { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public ICollection<ProductRate>? ProductRates { get; set; }

        public ICollection<PartyWiseProduct>? PartyWiseProducts { get; set; }

        public ICollection<InvoiceWiseProduct>? InvoiceWiseProducts { get; set; }
    }
}
