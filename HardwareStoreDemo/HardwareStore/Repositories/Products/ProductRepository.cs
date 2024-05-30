using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public ProductRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {

            return await _dataAccess.GetDataAsync<Product, dynamic>(
                "spProduct_GetAll",
                new { }
                );
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _dataAccess.GetDataAsync<Category, dynamic>(
                "spCategory_GetInfoList",
                new { }
                );
        }
        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            try
            {
                var category = await _dataAccess.GetDataAsync<Category, dynamic>(
                    "spCategory_GetById",
                    new { Id = id }
                    );

                return category.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Supplier>> GetAllSupplierAsync()
        {
            return await _dataAccess.GetDataAsync<Supplier, dynamic>(
                "spSupplier_GetInfoList",
                new { }
                );
        }

        public async Task<Supplier?> GetByIdSupplierAsync(int id)
        {
            try
            {
                var supplier = await _dataAccess.GetDataAsync<Supplier, dynamic>(
                    "spSupplier_GetById",
                    new { Id = id }
                    );

                return supplier.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Product?> GetByIdProductAsync(int id)
        {
            var product = await _dataAccess.GetDataAsync<Product, dynamic>(
                "spProduct_GetById",
                new { Id = id }
                );
            return product.FirstOrDefault();
        }

        public async Task AddProductAsync(Product product)
        {
            await _dataAccess.SaveDataAsync(
                "spProduct_Insert",
                new { product.ProductName, product.CategoryID, product.SupplierID, product.UnitPrice, product.UnitsInStock }
                );
        }

        public async Task EditProductAsync(Product product)
        {
            await _dataAccess.SaveDataAsync(
                "spProduct_Update",
                new { product.Id, product.ProductName, product.CategoryID, product.SupplierID, product.UnitPrice, product.UnitsInStock }
            );
        }


        public async Task DeleteProductAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "spProduct_Delete",
                new { Id = id }
                );
        }

        //met para comprobar si ya existe el prod en el sistm 
        public async Task<bool> ProductNameExistsAsync(string productName)
        {
            var product = await _dataAccess.GetDataAsync<Product, dynamic>(
                "spProduct_GetByName",
                new { ProductName = productName }
            );
            return product.Any();
        }

        public async Task<bool> ProductNameExistsAsync(string productName, int productId)
        {
            var products = await _dataAccess.GetDataAsync<Product, dynamic>(
                "spProduct_GetByNameExcludingId",
                new { ProductName = productName, ProductId = productId }
            );
            return products.Any();
        }
    }
}
