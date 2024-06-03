using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Logins
{
    public class LoginsRepository : ILoginsRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public LoginsRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Logins_Model>> GetAllAsyncLogin()
        {
            return await _dataAccess.GetDataAsync<Logins_Model, dynamic>(
                "dbo.spLogin_Login",
                new { }
                );
        }

        public async Task<IEnumerable<Logins_Model>> GetAllLoginAsync()
        {

            return await _dataAccess.GetDataAsync<Logins_Model, dynamic>(
                "spLogins_GetAll",
                new { }
                );
        }

        public async Task<IEnumerable<RolesModel>> GetAllRolesAsync()
        {
            return await _dataAccess.GetDataAsync<RolesModel, dynamic>(
                "spRole_GetInfoList",
                new { }
                );
        }
        public async Task<RolesModel> GetByIdRolesAsync(int id)
        {
            try
            {
                var category = await _dataAccess.GetDataAsync<RolesModel, dynamic>(
                    "spRole_GetById",
                    new { Id_Rol = id }
                    );

                return category.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<Logins_Model?> GetByIdLoginAsync(int id)
        {
            var product = await _dataAccess.GetDataAsync<Logins_Model, dynamic>(
                "spLogins_GetById",
                new { LoginId = id }
                );
            return product.FirstOrDefault();
        }

        public async Task AddLoginAsync(Logins_Model logins)
        {
            await _dataAccess.SaveDataAsync(
            "spLogins_Insert",
                new { logins.LoginUser, logins.LoginPassword, logins.Id_Rol }
                );
        }

        public async Task EditLoginAsync(Logins_Model logins)
        {
            await _dataAccess.SaveDataAsync(
                "spLogins_Update",
                new { logins.LoginId, logins.LoginUser, logins.LoginPassword, logins.Id_Rol }
            );
        }


        public async Task DeleteLoginAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "spLogins_Delete",
                new { LoginId = id }
                );
        }
    }
}
