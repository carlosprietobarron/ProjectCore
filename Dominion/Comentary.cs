using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominion
{
    public class Comentary
    {
        public Guid ComentaryId { get; set; }
        public required string StudentName { get; set; }
        public int Rating { get; set; }
        public string? ComentaryText { get; set; }
        public Guid CourseId { get; set; }

        public DateTime? CreateDate { get; set; }

        public Course? Course { get; set; }
    }
}