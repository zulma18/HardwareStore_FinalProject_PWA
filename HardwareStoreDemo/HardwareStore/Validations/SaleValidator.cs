using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.ClientID)
                .NotNull().WithMessage("El Cliente es Requerido")
                .NotEmpty().WithMessage("El Cliente no puede estar Vacio");

            RuleFor(sale => sale.EmployeeID)
                .NotNull().WithMessage("El Empleado es Requerido")
                .NotEmpty().WithMessage("El Empleado no puede estar Vacio");

            RuleFor(sale => sale.SaleDetails)
                .NotNull().WithMessage("Debe agregar al menos 1 producto")
                .NotEmpty();
        }
    }
}
