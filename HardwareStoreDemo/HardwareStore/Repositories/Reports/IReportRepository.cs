using HardwareStore.Models;

namespace HardwareStore.Repositories.Reports
{
    public interface IReportRepository
    {
        Task<IEnumerable<ProductReport>> GetProductReportAsync();
        Task<IEnumerable<SaleReport>> GetSaleReportAsync();
    }
}