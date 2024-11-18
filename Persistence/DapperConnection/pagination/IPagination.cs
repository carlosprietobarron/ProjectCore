using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DapperConnection.pagination
{
    public interface IPagination
    {
        Task<PageModel> returnPage(
            string storeProcedure, 
            int page, 
            int pageSize, 
            IDictionary<string, object> filterParams,
            string sortColumn);
    }
}