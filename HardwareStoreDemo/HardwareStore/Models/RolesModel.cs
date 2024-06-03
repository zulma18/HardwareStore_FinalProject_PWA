using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class RolesModel
    {
        public int Id_Rol { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string? Name_Rol { get; set; }
    }
}
