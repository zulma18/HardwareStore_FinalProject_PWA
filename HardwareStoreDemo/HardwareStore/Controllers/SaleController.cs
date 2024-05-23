using HardwareStore.Models;
using HardwareStore.Repositories.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HardwareStore.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleRepository _saleRepository;
        private SelectList _productsList;
        private SelectList _clientsList;
        private SelectList _employeesList;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;

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
            var products = await _saleRepository.GetAllProductsAsync();
            ViewBag.Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductName));


            var clients = await _saleRepository.GetAllClientsAsync();
            _clientsList = new SelectList(clients,
                                                nameof(Client.Id),
                                                nameof(Client.ClientName));

            var employees = await _saleRepository.GetAllEmployeesAsync();
            _employeesList = new SelectList(employees,
                                                nameof(Employee.Id),
                                                nameof(Employee.EmployeeName));

            ViewBag.Clients = _clientsList;
            ViewBag.Employees = _employeesList;
            return View();
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
                    var products = await _saleRepository.GetAllProductsAsync();
                    ViewBag.Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductName));

                    var clients = await _saleRepository.GetAllClientsAsync();
                    ViewBag.Clients = new SelectList(clients, nameof(Client.Id), nameof(Client.ClientName));

                    var employees = await _saleRepository.GetAllEmployeesAsync();
                    ViewBag.Employees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.EmployeeName));

                    return View(sale);
                }

                await _saleRepository.AddAsync(sale);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // En caso de error, recargar las listas desplegables
                var products = await _saleRepository.GetAllProductsAsync();
                ViewBag.Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductName));

                var clients = await _saleRepository.GetAllClientsAsync();
                ViewBag.Clients = new SelectList(clients, nameof(Client.Id), nameof(Client.ClientName));

                var employees = await _saleRepository.GetAllEmployeesAsync();
                ViewBag.Employees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.EmployeeName));

                return View(sale);
            }
        }

    }
}
