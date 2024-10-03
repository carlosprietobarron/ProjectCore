using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Dominion
{
    public class Price
    {
        public Guid PriceId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PromotionalPrice { get; set; }
        public  Guid CourseId { get; set; }
        public required Course Course { get; set; }
    }
}