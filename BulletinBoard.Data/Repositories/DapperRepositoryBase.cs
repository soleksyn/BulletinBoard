using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BulletinBoard.Data.Repositories
{
    public abstract class DapperRepositoryBase
    {
        protected readonly string _connectionString;

        protected DapperRepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected async Task<int> ExecuteAsync(string storedProcedure, object parameters = null)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure {storedProcedure}: {ex.Message}", ex);
            }
        }

        protected async Task<T> GetObjectAsync<T>(string storedProcedure, object parameters = null)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure {storedProcedure}: {ex.Message}", ex);
            }
        }

        protected async Task<IEnumerable<T>> GetCollectionAsync<T>(string storedProcedure, object parameters = null)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure {storedProcedure}: {ex.Message}", ex);
            }
        }
    }
}
