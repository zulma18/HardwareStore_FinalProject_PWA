


namespace HardwareStore.Repositories.Supplier
{
    public interface ISupplierRepository
    {
        Task AddAsync(Models.Supplier supplier);
        Task AddSupplierAsync(SupplierRepository supplier);
        Task AddSupplierAsync(Models.Supplier supplier);
        Task DeleteAsync(int id);
        Task DeleteSupplierAsync(int id);
        Task EditAsync(Models.Supplier supplier);
        Task EditSupplierAsync(Models.Supplier supplier);
        Task<string?> GetAllAsync();
        Task<string?> GetAllSuppliersAsync();
        Task<string?> GetByIdAsync(int id);
        Task<Supplier> GetByIdSupplierAsync(int id);
        Task<string?> GetSupplierByIdAsync(int id);
    }
}