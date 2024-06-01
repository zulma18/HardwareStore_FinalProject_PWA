using HardwareStore.Data;
using HardwareStore.Models;
using HardwareStore.Repositories.Supplier;
public class SupplierRepository : ISupplierRepository
{
    private readonly ISqlDataAccess _dataAccess;
    public SupplierRepository(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _dataAccess.GetDataAsync<Supplier, dynamic>(
            "dbo.spClient_GetAll",
            new { }
            );
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        var SupplierName = await _dataAccess.GetDataAsync<Supplier, dynamic>(
            "dbo.spClient_GetById",
            new { Id = id }
            );

        return SupplierName.FirstOrDefault();
    }

    public async Task EditAsync(Supplier supplier)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spSupplier_Update",
            new
            {
                Supplier.Id,
                Supplier.SupplierName,
                Supplier.Phone,
                Supplier.Email,
                Supplier.Address,
                Supplier.City
            }
            );
    }

    public async Task DeleteAsync(int id)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spClient_Delete",
            new { Id = id }
            );
    }

    public async Task AddAsync(Supplier supplier)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spClient_Insert",
            new { Supplier.SupplierName, Supplier.Email, Supplier.Phone, Supplier.Address, Supplier.City });
    }

}

