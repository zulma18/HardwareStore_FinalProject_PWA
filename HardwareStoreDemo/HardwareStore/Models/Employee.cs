using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El telefono es requerido")]
        [Display(Name = "Telefono")]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato debe ser ####-####.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "EL salario es requerido")]
        [Display(Name = "Salario")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "La direccion es requerida")]
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ciudad es requerida")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "Nombre del Empleado")]
        public string ?EmployeeName { get; set; }
    }
}
