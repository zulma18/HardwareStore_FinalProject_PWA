using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Roles
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public RolesRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<RolesModel>> GetAllAsync()
        {
            return await _dataAccess.GetDataAsync<RolesModel, dynamic>(
                "dbo.spRole_GetAll",
                new { }
                );
        }

        public async Task<RolesModel?> GetByIdAsync(int id)
        {
            var roles = await _dataAccess.GetDataAsync<RolesModel, dynamic>(
                "dbo.spRole_GetById",
                new { Id_Rol = id }
                );

            return roles.FirstOrDefault();
        }

        public async Task EditAsync(RolesModel roles)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spRole_Update",
                new
                {
                    roles.Id_Rol,
                    roles.Name_Rol,
                }
                );
        }

        public async Task DeleteAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spRole_Delete",
                new { Id_Rol = id }
                );
        }

        public async Task AddAsync(RolesModel roles)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spRole_Insert",
                new { roles.Name_Rol });
        }
    }
}
