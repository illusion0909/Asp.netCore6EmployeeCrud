using BusinessWebSoftPvtLmtTaskApi.Data;
using BusinessWebSoftPvtLmtTaskApi.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BusinessWebSoftPvtLmtTaskApi.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _Dbcontext;
        private static string connectionString = "";
        public GenericRepository(ApplicationDbContext context)
        {
            _Dbcontext = context;
            connectionString = _Dbcontext.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _Dbcontext.Dispose();
        }

        public async Task ExecuteAsync(string procedureName, DynamicParameters? parameters = null)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                await sqlCon.OpenAsync();
                await sqlCon.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> ListAsync<T>(string procedureName, DynamicParameters? para = null) 
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<T>(procedureName, para, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> SingleAsync<T>(string procedureName, DynamicParameters? parameters = null)
        {
            using (var sqlCon = new SqlConnection(connectionString))
            {
                await sqlCon.OpenAsync();
                var result = await sqlCon.QuerySingleOrDefaultAsync<T>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    throw new InvalidOperationException($"No record was found for procedure {procedureName}.");
                }
                return result;
            }
        }


    }
}
