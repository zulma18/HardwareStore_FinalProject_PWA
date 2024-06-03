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

        public async Task<IEnumerable<T>> GetData1Async<T, U, V, P>(
        string storedProcedure,
        P parameters,
        Func<T, U, V, T>? map = null,
        string connection = "default",
        string splitOn = "Id")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            if (map == null)
            {
                return await dbConnection.QueryAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            else
            {
                return await dbConnection.QueryAsync<T, U, V, T>(
                    storedProcedure,
                    map,
                    parameters,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> GetData2Async<T, U, V, W, P>(
        string storedProcedure,
        P parameters,
        Func<T, U, V, W, T>? map = null,
        string connection = "default",
        string splitOn = "Id")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            if (map == null)
            {
                return await dbConnection.QueryAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            else
            {
                return await dbConnection.QueryAsync<T, U, V, W, T>(
                    storedProcedure,
                    map,
                    parameters,
                    splitOn: splitOn,
                    commandType: CommandType.StoredProcedure);
            }
        }

        // metodo para guardar las ventas y los detalles de venta
        public async Task<int> SaveDataWithReturnAsync<T>(
        string storedProcedure,
        T parameters,
        string connection = "default")
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString(connection));

            var p = new DynamicParameters(parameters);
            // aqui con p.Add solo agregamos un nuevo parametro "SaleID" que es de tipo entero y se le asignara el valor Output
            // (que viene de salida del proceso almacenado)
            p.Add("SaleID", DbType.Int32, direction: ParameterDirection.Output);

            await dbConnection.ExecuteAsync(storedProcedure, p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("SaleID"); // retorna el id de la ultima venta registrada
        }
    }
}
