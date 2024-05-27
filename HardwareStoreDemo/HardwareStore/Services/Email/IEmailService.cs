using HardwareStore.Models;

namespace HardwareStore.Services.Email
{
    public interface IEmailService
    {
        void SendEmail(Dictionary<string, string> data, List<SaleDetail> detailsList);
    }
}