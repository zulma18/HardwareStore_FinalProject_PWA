using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int ClientID { get; set; }

        public string ClientName { get; set; }

        public string ClientAddress { get; set; }

        public string ClientEmail { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal Total { get; set; }

        [Required]
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
