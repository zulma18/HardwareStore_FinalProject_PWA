using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Categorys
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public CategoryRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _dataAccess.GetDataAsync<Category, dynamic>(
                "spCategory_GetAll",
                new { }
                );
        }

        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            var category = await _dataAccess.GetDataAsync<Category, dynamic>(
                "spCategory_GetById",
                new { Id = id }
                );

            return category.FirstOrDefault();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dataAccess.SaveDataAsync(
                "spCategory_Insert",
                new { category.CategoryName, category.Description }
                );
        }
        public async Task EditCategoryAsync(Category category)
        {
            await _dataAccess.SaveDataAsync(
                "spCategory_Update",
                category
                );
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "spCategory_Delete",
                new { Id = id }
                );
        }

        public async Task<bool> CategoryNameExistsAsync(string categoryName)
        {
            var category = await _dataAccess.GetDataAsync<Category, dynamic>(
                "spCategory_GetByName",
                new { CategoryName = categoryName }
            );

            return category.Any();
        }
    }
}
