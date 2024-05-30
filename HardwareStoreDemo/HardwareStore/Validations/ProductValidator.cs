using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.ProductName)
                .Length(3, 100).WithMessage("El numero de caracteres minimo debe es 3 ")
                .NotNull().WithMessage("El nombre del producto es requerido");

            RuleFor(product => product.CategoryID)
                .NotNull().WithMessage("El Id de la Categoria es requerido");

            RuleFor(product => product.SupplierID)
                .NotNull().WithMessage("El Id del Proveedor es requerido");

            RuleFor(product => product.UnitPrice)
            .NotNull().WithMessage("El precio por unidad es requerido")
            .GreaterThan(0).WithMessage("El precio por unidad debe ser mayor que 0");

            RuleFor(product => product.UnitsInStock)
            .NotNull().WithMessage("Las unidades en stock son requeridas")
            .GreaterThanOrEqualTo(0).WithMessage("Las unidades en stock deben ser mayor o igual a 0");
        }
    }
}
