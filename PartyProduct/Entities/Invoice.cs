using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Invoice
    {
        [Key]
        public int? InvoiceID { get; set; }

        [ForeignKey("Parties")]
        public int? PartyID { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public bool IsPaid { get; set; } = false;

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public Party? Party { get; set; }

        public List<InvoiceWiseProduct>? InvoiceWiseProducts { get; set; }

    }
}
