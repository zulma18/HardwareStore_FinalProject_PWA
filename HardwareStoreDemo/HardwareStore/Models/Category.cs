using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es requerido")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre de la categoría solo puede contener letras y espacios")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "El nombre de la categoría debe tener entre 7 y 50 caracteres")]
        [Display(Name = "Categoria")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(250, MinimumLength = 20, ErrorMessage = "La descripcion debe tener entre 20 y 250 caracteres")]
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
    }
}
