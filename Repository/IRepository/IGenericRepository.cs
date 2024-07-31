using Dapper;

namespace BusinessWebSoftPvtLmtTaskApi.Repository.IRepository
{
   public interface IGenericRepository : IDisposable
    {
        Task ExecuteAsync(string procedureName, DynamicParameters? parameters = null);
        Task<T> SingleAsync<T>(string procedureName, DynamicParameters? parameters = null);
        Task<IEnumerable<T>> ListAsync<T>(string procedureName, DynamicParameters? para = null);
    }
}
