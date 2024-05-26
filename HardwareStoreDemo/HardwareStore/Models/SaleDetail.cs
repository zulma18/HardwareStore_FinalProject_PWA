using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }

        public int SaleID { get; set; }

        [Required (ErrorMessage="Seleccione el Producto")]
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        [Required(ErrorMessage = "La cantidad es Obligatoria")]
        public int Quantity { get; set; }

        public int Stock { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DetailTotal { get; set; }
    }
}
