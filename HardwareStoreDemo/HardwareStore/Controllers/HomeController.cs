using HardwareStore.Models;
using HardwareStore.Repositories.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HardwareStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportRepository _reportRepository;

        public HomeController(ILogger<HomeController> logger, IReportRepository reportRepository)
        {
            _logger = logger;

            _reportRepository = reportRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> SaleReport()
        {
            var saleReport = await _reportRepository.GetSaleReportAsync();

            return Json(saleReport);
        }

        [HttpGet]
        public async Task<JsonResult> ProductReport()
        {
            var productReport = await _reportRepository.GetProductReportAsync();

            return Json(productReport);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
