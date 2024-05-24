using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HardwareStore.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> GetDataAsync<T, P>(
            string storedProcedure, P parameters, string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            return await dbConnection.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SaveDataAsync<T>(
            string storedProcedure,
            T parameters,
            string connection = "default")
        {
            using IDbConnection dbConnection =
                new SqlConnection(_configuration.GetConnectionString(connection));

            await dbConnection.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        // metodo para guardar las ventas y los detalles de venta
        public async Task<int> SaveDataWithReturnAsync<T>(
        string storedProcedure,
        T parameters,
        string connection = "default")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            var p = new DynamicParameters(parameters);
            p.Add("SaleID", DbType.Int32, direction: ParameterDirection.Output);

            await dbConnection.ExecuteAsync(storedProcedure, p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("SaleID"); // retorna el id de la ultima venta registrada
        }
    }
}
