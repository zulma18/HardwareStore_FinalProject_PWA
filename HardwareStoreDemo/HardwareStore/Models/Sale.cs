using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HardwareStore.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="El Cliente es Requerido")]
        [Display (Name = "Cliente")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "El Empleado es Requerido")]
        [Display (Name = "Empleado")]
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
        public List<SaleDetail> SaleDetails { get; set; }
    }
}
