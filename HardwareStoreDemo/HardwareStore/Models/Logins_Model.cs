using System.ComponentModel.DataAnnotations;
namespace HardwareStore.Models
{
    public class Logins_Model
    {
        public int LoginId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(50)]
        [Display(Name = "Usuario")]
        public string LoginUser { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        public string LoginPassword { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [Display(Name = "Rol")]
        public int Id_Rol { get; set; }

        [Display(Name = "Rol")]
        public string Name_Rol { get; set; }

        public RolesModel? Roles { get; set; }
    }
}
