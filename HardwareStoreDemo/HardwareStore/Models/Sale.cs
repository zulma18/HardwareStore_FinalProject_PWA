using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="El Cliente es Requerido")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "El Empleado es Requerido")]
        public int EmployeeID { get; set; }

        [Display(Name = "Cliente")]
        public string ClientName { get; set; }

        [Display(Name = "Direccion")]
        public string ClientAddress { get; set; }

        [Display(Name ="Correo Electronico")]
        public string ClientEmail { get; set; }

        [Display(Name ="Empleado")]
        public string EmployeeName { get; set; }

        [Display(Name = "Fecha de Venta")]
        public DateTime SaleDate { get; set; }

        public decimal Total { get; set; }

        [Required(ErrorMessage ="Debe Seleccionar al menos 1 Producto")]
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
