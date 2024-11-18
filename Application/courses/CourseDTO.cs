using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;
using Microsoft.Identity.Client;

namespace Application.courses
{
    public class CourseDTO
    {
        public Guid CourseId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public byte[]? FrontPhoto  { get; set; }

        public ICollection<TeacherDTO>? Teachers { get; set; }
        public Price? PriceDTO { get; set; }
        public ICollection<ComentaryDTO>? Comentaries { get; set; }
    }

   
}