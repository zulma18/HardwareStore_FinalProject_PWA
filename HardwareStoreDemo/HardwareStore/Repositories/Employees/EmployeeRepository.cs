using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public EmployeeRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dataAccess.GetDataAsync<Employee, dynamic>(
                "dbo.spEmployee_GetAll",
                new { }
                );
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var university = await _dataAccess.GetDataAsync<Employee, dynamic>(
                "dbo.spEmployee_GetById",
                new { Id = id }
                );

            return university.FirstOrDefault();
        }

        public async Task EditAsync(Employee employee)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spEmployee_Update",
                new {employee.Id, employee.FirstName, employee.LastName, employee.Phone, employee.Email, employee.Salary,
                    employee.Address, employee.City}
                );
        }

        public async Task DeleteAsync(int id)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spEmployee_Delete",
                new { Id = id }
                );
        }

        public async Task AddAsync(Employee employee)
        {
            await _dataAccess.SaveDataAsync(
                "dbo.spEmployee_Insert",
                new { employee.FirstName, employee.LastName, employee.Phone, employee.Email, employee.Salary, employee.Address, employee.City });
        }
    }
}
