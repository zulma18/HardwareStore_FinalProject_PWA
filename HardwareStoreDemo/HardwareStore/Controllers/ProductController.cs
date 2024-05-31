using FluentValidation;
using FluentValidation.Results;
using HardwareStore.Models;
using HardwareStore.Repositories.Products;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniExcelLibs;

namespace HardwareStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<Product> _validator;

        public ProductController(IProductRepository productRepository, IValidator<Product> validator)
        {
            _productRepository = productRepository;

            _validator = validator;
        }


        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var products = await _productRepository.GetAllProductAsync();

            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            await ViewBagLists();

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(product);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    await ViewBagLists();

                    return View(product);
                }

                // Comprobar si ya existe un producto con el mismo nombre
                if (await _productRepository.ProductNameExistsAsync(product.ProductName))
                {
                    ModelState.AddModelError("ProductName", "Ya existe un producto con este nombre.");

                    await ViewBagLists();

                    return View(product);
                }

                // Agregar el producto si no existe otro con el mismo nombre
                await _productRepository.AddProductAsync(product);
                TempData["addProduct"] = "Se registró el producto correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await ViewBagLists();

                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImportData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportData(IFormFile file)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(directoryPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                using (var stream = System.IO.File.OpenRead(path))
                {
                    var rows = stream.Query<Product>();

                    foreach (var row in rows)
                    {
                        try
                        {
                            await _productRepository.AddProductAsync(row);
                        } catch (Exception)
                        {
                            TempData["message"] = "La importacion Fallo por favor revice que su archivo contenga los campos necesarios";

                            return View();
                        }
                    }
                }
            }
            else
            {
                TempData["message"] = "El archivo esta Vacio o no se Ingreso!";

                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id);

            if (product == null)
                return NotFound();

            await ViewBagLists();

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {

            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(product);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    await ViewBagLists();

                    return View(product);
                }

                await _productRepository.EditProductAsync(product);

                TempData["editProduct"] = "Producto editado con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                await ViewBagLists();

                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdProductAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Product product)
        {
            try
            {
                await _productRepository.DeleteProductAsync(product.Id);

                TempData["deleteProduct"] = "Producto eliminado con éxito";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "No se pudo eliminar el producto";

                return RedirectToAction(nameof(Index));
            }
        }

        private async Task ViewBagLists()
        {
            var categories = await _productRepository.GetAllCategoryAsync();
            ViewBag.Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.CategoryName));

            var suppliers = await _productRepository.GetAllSupplierAsync();
            ViewBag.Suppliers = new SelectList(suppliers, nameof(Supplier.Id), nameof(Supplier.SupplierName));
        }
    }
}
