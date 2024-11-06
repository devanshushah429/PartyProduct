using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PartyWiseProduct
    {
        [Key]
        public int? PartyWiseProductID { get; set; }

        [ForeignKey("Parties")]
        public int? PartyID { get; set; }

        [ForeignKey("Products")]
        public int? ProductID { get; set; }

        public Party? Party { get; set; }

        public Product? Product { get; set; }
    }
}
