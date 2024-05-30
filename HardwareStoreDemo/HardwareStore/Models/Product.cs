using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del producto debe tener entre 3 y 100 caracteres")]
        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        public int CategoryID { get; set; }

        [Display(Name = "Categoría")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido")]
        [Display(Name = "Proveedor")]
        public int SupplierID { get; set; }

        [Display(Name = "Proveedor")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "El precio unitario es requerido")]
        [Range(0.01, 10000, ErrorMessage = "El precio unitario debe estar entre 0.01 y 10000")]
        [Display(Name = "Precio Unitario")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Las unidades en stock son requeridas")]
        [Range(0, int.MaxValue, ErrorMessage = "Las unidades en stock deben ser un valor positivo")]
        [Display(Name = "Unidades stock")]
        public int UnitsInStock { get; set; }

        public Category? Category { get; set; }

        public Supplier? Supplier { get; set; }

    }
}
