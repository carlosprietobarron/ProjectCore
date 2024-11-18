using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Persistence.DapperConnection;
using System.Linq;

namespace Persistence.DapperConnection.pagination
{
    public class PageRepository : IPagination
    {
        private readonly  IFactioryConnection _factoryConnection;
        

        public PageRepository(IFactioryConnection connection){
            _factoryConnection = connection;
        }
        public async Task<PageModel> returnPage(string storeProcedure, int page, int pageSize, IDictionary<string, object> filterParams, string sortColumn)
        {
            PageModel pageModel = new PageModel();
            List<IDictionary<string, object>> _ResultList = null;
            int totalRecords = 0;
            int totalPages = 0;
            try
            {
                var connection = _factoryConnection.GetConnection();
                DynamicParameters dynamicParameters = new DynamicParameters();
                    foreach (var param in filterParams)
                    {
                      dynamicParameters.Add("@"+param.Key, param.Value);
                    }

                    dynamicParameters.Add("@page", page);
                    dynamicParameters.Add("@pageSize", pageSize);
                    dynamicParameters.Add("sortColumn", sortColumn);

                    dynamicParameters.Add("@totalRecords", totalRecords, System.Data.DbType.Int32);
                    dynamicParameters.Add("@totalPages", totalPages, System.Data.DbType.Int32);
                var results = await connection.QueryAsync(storeProcedure, null, commandType: System.Data.CommandType.StoredProcedure);
                _ResultList = results.Select(result => (IDictionary<string, object>)result).ToList();

                pageModel.Records = _ResultList;
                pageModel.TotalRecords = dynamicParameters.Get<int>("@totalRecords");
                pageModel.TotalPages = dynamicParameters.Get<int>("totalPages");
            }
            catch (System.Exception e)
            {
                
                throw new Exception("Could not create page", e);
            }
            finally{
                _factoryConnection.CloseConnection();
            }

            return pageModel;
        }
    }
}