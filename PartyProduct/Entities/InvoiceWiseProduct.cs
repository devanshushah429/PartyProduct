using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class InvoiceWiseProduct
    {
        [Key]
        public int? InvoiceWiseProductID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }

        [ForeignKey("Invoices")]
        public int? InvoiceID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Subtotal { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;

        public Product? Product { get; set; }

        public Invoice? Invoice { get; set; }

    }
}
