using HardwareStore.Models;

public interface ISupplierRepository
{
    Task AddAsync(Supplier supplier);
    Task DeleteAsync(int id);
    Task EditAsync(Supplier supplier);
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int id);
}