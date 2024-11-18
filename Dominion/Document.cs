using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominion
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public Guid ObjectReference { get; set; }
        public string Name { get; set; }
        public string Extention  { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreationDate { get; set; }
        }
}