using FluentValidation;
using FluentValidation.Results;
using HardwareStore.Models;
using HardwareStore.Repositories.Employees;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<Employee> _validator;

        public EmployeeController(IEmployeeRepository employeeRepository, IValidator<Employee> validator)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllAsync();

            return View(employees);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                ValidationResult validationResult =
                    await _validator.ValidateAsync(employee);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    return View(employee);
                }

                await _employeeRepository.AddAsync(employee);

                TempData["addEmployee"] = "Empleado Agregado con exito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(employee);
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Employee employee)
        {
            try
            {
                await _employeeRepository.EditAsync(employee);

                TempData["editEmployee"] = "Empleado Editado con exito";

                return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(employee);
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Employee employee)
        {
            try
            {
                await _employeeRepository.DeleteAsync(employee.Id);

                TempData["deleteEmployee"] = "Empleado Eliminado con exito";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "No se pudo Eliminar el empleado";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
