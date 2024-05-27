using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.CategoryName)
                .NotNull().WithMessage("El nombre categoria es requerido")
                .Length(7, 50).WithMessage("El numero de caracteres debe estar entre 7 y 50");

            RuleFor(category => category.Description)
                .NotNull().WithMessage("La descripcion es requerido")
                .Length(20, 250).WithMessage("El numero de caracteres debe estar entre 20 y 250");
        }
    }
}
