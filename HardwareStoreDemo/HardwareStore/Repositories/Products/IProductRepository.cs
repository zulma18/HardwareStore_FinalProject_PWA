using HardwareStore.Models;

namespace HardwareStore.Repositories.Products
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task EditProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product?> GetByIdProductAsync(int id);

        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category?> GetByIdCategoryAsync(int id);
        Task<IEnumerable<Supplier>> GetAllSupplierAsync();
        Task<Supplier?> GetByIdSupplierAsync(int id);

        //met para comprobar si ya existe el prod en el sistm
        Task<bool> ProductNameExistsAsync(string productName); 

    }
}
