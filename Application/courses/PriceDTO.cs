using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Application.courses
{
    public class PriceDTO
    {
        public Guid PriceId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PromotionalPrice { get; set; }
        public  Guid CourseId { get; set; }
    }
}