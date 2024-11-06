using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Entities
{
    public class ProductRate
    {
        [Key]
        public int? ProductRateID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }

        public DateTime? PriceAppliedDate { get; set; }

        public decimal? ProductPrice { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
        
        public Product? Product { get; set; }
        
    }
}
