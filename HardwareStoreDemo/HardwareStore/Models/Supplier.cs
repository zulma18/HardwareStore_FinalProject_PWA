using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "El nombre del Proveedor es Requerido")]
        [Display (Name = "Nombre del Proveedor")]
        public string SupplierName { get; set; }

        [Required (ErrorMessage ="El Telefono es Requerido")]
        [Display (Name = "Telefono")]
        public string Phone { get; set; }

        [Required (ErrorMessage = "El correo electronico es requerido")]
        [Display (Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required (ErrorMessage = "La direccion es requerida")]
        [Display (Name = "Direccion")]
        public string Address { get; set; }

        [Required (ErrorMessage ="La ciudad es Requerida")]
        [Display (Name = "Ciudad")]
        public string City { get; set; }
    }
}
