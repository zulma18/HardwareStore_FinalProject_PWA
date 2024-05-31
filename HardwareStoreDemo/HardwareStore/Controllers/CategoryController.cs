using FluentValidation;
using FluentValidation.Results;
using HardwareStore.Models;
using HardwareStore.Repositories.Categorys;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<Category> _validator;

        public CategoryController(ICategoryRepository categoryRepository, IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            var categorys = await _categoryRepository.GetAllCategoryAsync();

            return View(categorys);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(category);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    return View(category);
                }

                // Comprobar si ya existe un producto con el mismo nombre
                if (await _categoryRepository.CategoryNameExistsAsync(category.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Ya existe una categoria con este nombre.");

                    return View(category);
                }

                // Agregar el producto si no existe otro con el mismo nombre
                await _categoryRepository.AddCategoryAsync(category);

                TempData["addCategory"] = "Categoria agregada con exito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categorys = await _categoryRepository.GetByIdCategoryAsync(id);

            if (categorys == null)
                return NotFound();

            return View(categorys);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category)
        {
            try
            {
                await _categoryRepository.EditCategoryAsync(category);

                TempData["editCategory"] = "Categoria editada con exito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categorys = await _categoryRepository.GetByIdCategoryAsync(id);

            if (categorys == null)
                return NotFound();

            return View(categorys);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Category category)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(category.Id);

                TempData["deleteCategory"] = "Categoria eliminado con exito";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "No se pudo eliminar la categoria";

                return RedirectToAction(nameof(Index));
            }
        }

    }
}
