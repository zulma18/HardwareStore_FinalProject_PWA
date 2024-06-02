using HardwareStore.Data;
using HardwareStore.Models;

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
            "dbo.spSupplier_GetAll",
            new { }
            );
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        var SupplierName = await _dataAccess.GetDataAsync<Supplier, dynamic>(
            "dbo.spSupplier_GetById",
            new { Id = id }
            );

        return SupplierName.FirstOrDefault();
    }

    public async Task EditAsync(Supplier supplier)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spSupplier_Update",
            new { supplier.Id, supplier.SupplierName, supplier.Phone, supplier.Email, supplier.Address, supplier.City }
            );
    }

    public async Task DeleteAsync(int id)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spSupplier_Delete",
            new { Id = id }
            );
    }

    public async Task AddAsync(Supplier supplier)
    {
        await _dataAccess.SaveDataAsync(
            "dbo.spSupplier_Insert",
            new { supplier.SupplierName, supplier.Email, supplier.Phone, supplier.Address, supplier.City });
    }

}

