using HardwareStore.Data;
using HardwareStore.Models;

namespace HardwareStore.Repositories.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly ISqlDataAccess _dataAccess;

        public ReportRepository(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<SaleReport>> GetSaleReportAsync()
        {

            return await _dataAccess.GetDataAsync<SaleReport, dynamic>(
                "dbo.spSale_InfoForGraph",
                new { }
                );
        }

        public async Task<IEnumerable<ProductReport>> GetProductReportAsync()
        {

            return await _dataAccess.GetDataAsync<ProductReport, dynamic>(
                "dbo.spProduct_InfoForGraph",
                new { }
                );
        }
    }
}
