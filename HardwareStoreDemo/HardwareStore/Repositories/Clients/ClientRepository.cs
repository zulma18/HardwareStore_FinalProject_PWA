using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public ClientRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dataAccess.GetDataAsync<Client, dynamic>(
                "dbo.spClient_GetAll",
                new { }
                );
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            var university = await _dataAccess.GetDataAsync<Client, dynamic>(
                "dbo.spClient_GetById",
                new { Id = id }
                );

            return university.FirstOrDefault();
        }

        public async Task EditAsync(Client client)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spClient_Update",
                new
                {
                    client.Id,
                    client.FirstName,
                    client.LastName,
                    client.Email,
                    client.Phone,
                    client.Address,
                    client.City
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

        public async Task AddAsync(Client client)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spClient_Insert",
                new { client.FirstName, client.LastName, client.Email, client.Phone, client.Address, client.City });
        }

    }
}