namespace HardwareStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int CategoryID { get; set; }

        public int SupplierID { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}
