using HardwareStore.Models;

namespace HardwareStore.Repositories.Sales
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<IEnumerable<SaleDetail?>> GetSaleDetailsByIdAsync(int id);
    }
}