using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Persistence.DapperConnection.pagination
{
    public class PageModel
    {
        public List<IDictionary<string, object>> Records { get; set; }
        public int TotalRecords{ get; set; }

        public int TotalPages { get; set; }

    }
}