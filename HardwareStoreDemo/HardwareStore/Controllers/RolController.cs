using HardwareStore.Models;
using HardwareStore.Repositories.Roles;
using HardwareStore.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace HardwareStore.Controllers
{
    public class RolController : Controller
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IValidator<RolesModel> _validator;

        public RolController(IRolesRepository rolesRepository, IValidator<RolesModel> validator)
        {
            _rolesRepository = rolesRepository;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {

            var logins = await _rolesRepository.GetAllAsync();

            return View(logins);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RolesModel rolModel)
        {

            try
            {
                FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(rolModel);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(ModelState);
                    return View(rolModel);
                }

                await _rolesRepository.AddAsync(rolModel);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(rolModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var rol = await _rolesRepository.GetByIdAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RolesModel rolModel)
        {
            try
            {
                FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(rolModel);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(ModelState);
                    return View(rolModel);
                }

                await _rolesRepository.EditAsync(rolModel);

                TempData["message"] = "Datos editados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(rolModel);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var login = await _rolesRepository.GetByIdAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(RolesModel rolModel)
        {
            try
            {
                await _rolesRepository.DeleteAsync(rolModel.Id_Rol);

                TempData["message"] = "Datos eliminados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return View(rolModel);
            }
        }
    }
}
