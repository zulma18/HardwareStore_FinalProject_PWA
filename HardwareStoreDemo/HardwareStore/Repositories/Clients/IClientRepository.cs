
using HardwareStore.Models;

namespace HardwareStore.Repositories.Clients
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task DeleteAsync(int id);
        Task EditAsync(Client client);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
    }
}