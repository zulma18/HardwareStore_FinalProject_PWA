using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class SupplierValidator: AbstractValidator<Supplier>
    {

        public SupplierValidator()
        {
            RuleFor(supplier => supplier.SupplierName)
                .Length(4, 50).WithMessage("El numero de caracteres debe estar entre 4 y 50")
                .NotNull().WithMessage("El nombre es requerido");

            RuleFor(supplier => supplier.Phone)
                .MaximumLength(20).WithMessage("El numero maximo de caracteres es 20")
                .NotNull().WithMessage("El telefono es requerido");

            RuleFor(supplier => supplier.Email)
                .MaximumLength(50).WithMessage("El numero de caracteres debe estar entre 50")
                .NotNull().WithMessage("El correo es requerido");

            RuleFor(supplier => supplier.Address)
                .Length(3, 250).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La direccion es requerida");

            RuleFor(supplier => supplier.City)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La ciudad es requerida");
        }
    }
}
