using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HardwareStore.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [Display(Name ="Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El Apellido es Requerido")]
        [Display(Name ="Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El Correo Electronico es Requerido")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Telefono es Requerido")]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "La Direccion es Requerida")]
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La Ciudad es Requerida")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        public string ClientName { get; set; }
    }
}
