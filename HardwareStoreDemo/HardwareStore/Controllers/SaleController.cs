using FluentValidation;
using FluentValidation.Results;
using HardwareStore.Models;
using HardwareStore.Repositories.Sales;
using HardwareStore.Services.Email;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HardwareStore.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IValidator<Sale> _validator;

        private readonly IEmailService _emailService;

        public SaleController(ISaleRepository saleRepository, IValidator<Sale> validator, IEmailService emailService)
        {
            _saleRepository = saleRepository;

            _validator = validator;

            _emailService = emailService;
        }

        // GET: SaleController
        public async Task<ActionResult> Index()
        {
            return View(await _saleRepository.GetAllAsync());
        }

        // GET: SaleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(id);

            sale.SaleDetails = (ICollection<SaleDetail>)await _saleRepository.GetSaleDetailsByIdAsync(id);

            return View(sale);
        }

        [HttpPost]
        public async Task<ActionResult> Details()
        {
            return View();
        }

        // metodo para la peticion ajax, envia los detalles del producto seleccionado a la vista
        [HttpGet]
        public async Task<IActionResult> SearchProductById(int id)
        {
            try
            {
                var product = await _saleRepository.GetProductByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Json(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: SaleController/Create
        public async Task<ActionResult> Create()
        {
            await ViewBagLists();

            var sale = new Sale
            {
                SaleDetails = new List<SaleDetail>()
            };

            return View(sale);
        }


        // POST: SaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Sale sale)
        {
            try
            {
                if (sale.SaleDetails == null)
                {
                    TempData["message"] = "Debe Agregar Almenos 1 Producto";

                    await ViewBagLists();

                    return View(sale);
                }

                ValidationResult validationResult =
                    await _validator.ValidateAsync(sale);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(this.ModelState);

                    await ViewBagLists();

                    return View(sale);
                }

                await _saleRepository.AddAsync(sale);

                var client = await _saleRepository.GetClientByIdAsync(sale.ClientID);

                Dictionary<string, string> data = new Dictionary<string, string>
                {
                    { "Subject", "Factura de Compra" },
                    { "RecepientName",(client.FirstName + ' ' + client.LastName).ToString() },
                    { "EmailTo", client.Email },
                    { "Address", client.Address  },
                    { "City", client.City }
                };

                _emailService.SendEmail(data, sale.SaleDetails.ToList());

                TempData["addSale"] = "Se registro la venta correctamente";

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                await ViewBagLists();

                return View(sale);
            }
        }

        private async Task ViewBagLists()
        {
            var products = await _saleRepository.GetAllProductsAsync();
            ViewBag.Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductName));

            var clients = await _saleRepository.GetAllClientsAsync();
            ViewBag.Clients = new SelectList(clients, nameof(Client.Id), nameof(Client.ClientName));

            var employees = await _saleRepository.GetAllEmployeesAsync();
            ViewBag.Employees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.EmployeeName));
        }

    }
}
