using HardwareStore.Models;

namespace HardwareStore.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task DeleteAsync(int id);
        Task EditAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
    }
}