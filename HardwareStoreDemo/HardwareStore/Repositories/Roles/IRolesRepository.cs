using HardwareStore.Models;

namespace HardwareStore.Repositories.Roles
{
    public interface IRolesRepository
    {
        Task AddAsync(RolesModel roles);
        Task DeleteAsync(int id);
        Task EditAsync(RolesModel roles);
        Task<IEnumerable<RolesModel>> GetAllAsync();
        Task<RolesModel?> GetByIdAsync(int id);
    }
}
