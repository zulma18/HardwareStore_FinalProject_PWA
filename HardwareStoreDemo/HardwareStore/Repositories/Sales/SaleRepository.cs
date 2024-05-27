using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Sales
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public SaleRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _dataAccess.GetDataAsync<Sale, dynamic>(
                "dbo.spSale_GetAll",
                new { }
                );
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dataAccess.GetDataAsync<Product, dynamic>(
                "dbo.spProduct_GetInfoList",
                new { }
                );
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            try
            {
                var products = await _dataAccess.GetDataAsync<Product, dynamic>(
                    "dbo.spProduct_GetInfoForSaleById",
                    new { Id = id }
                );

                return products.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SaleDetail?>> GetSaleDetailsByIdAsync(int id)
        {
            return await _dataAccess.GetDataAsync<SaleDetail, dynamic>(
                "dbo.spSalesDetail_GetById",
                new { Id = id }
                );
        }



        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _dataAccess.GetDataAsync<Client, dynamic>(
                "dbo.spClient_GetInfoList",
                new { }
                );
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            try
            {
                var client = await _dataAccess.GetDataAsync<Client, dynamic>(
                    "dbo.spClient_GetById",
                    new { Id = id }
                );

                return client.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _dataAccess.GetDataAsync<Employee, dynamic>(
                "dbo.spEmployee_GetInfoList",
                new { }
                );
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            var sale = await _dataAccess.GetDataAsync<Sale, dynamic>(
                "dbo.spSale_GetById",
                new { Id = id }
                );

            return sale.FirstOrDefault();
        }

        public async Task AddAsync(Sale sale)
        {
            // Guarda la venta y recupera el ultimo id generado
            var saleID = await _dataAccess.SaveDataWithReturnAsync(
                "dbo.spSale_Insert",
                new { sale.ClientID, sale.EmployeeID });

            foreach (var saleDetail in sale.SaleDetails) 
            {
                saleDetail.SaleID = saleID; // Asignar el SaleID obtenido
                await _dataAccess.SaveDataAsync("dbo.spSalesDetail_Insert", // inserta los detalles de venta
                    new { saleDetail.SaleID, saleDetail.ProductID, saleDetail.Quantity });
            }
        }
    }
}
