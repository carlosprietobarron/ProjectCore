using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DapperConnection
{
    public interface IFactioryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}