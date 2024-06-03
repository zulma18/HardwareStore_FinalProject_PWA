using HardwareStore.Models;

namespace HardwareStore.Repositories.Logins
{
    public interface ILoginsRepository
    {
        Task AddLoginAsync(Logins_Model login );
        Task DeleteLoginAsync(int id);
        Task EditLoginAsync(Logins_Model login);
        Task<IEnumerable<Logins_Model>> GetAllLoginAsync();

        Task<IEnumerable<Logins_Model>> GetAllAsyncLogin();
        Task<Logins_Model?> GetByIdLoginAsync(int id);

        Task<IEnumerable<RolesModel>> GetAllRolesAsync();
        Task<RolesModel?> GetByIdRolesAsync(int id);
    }
}
