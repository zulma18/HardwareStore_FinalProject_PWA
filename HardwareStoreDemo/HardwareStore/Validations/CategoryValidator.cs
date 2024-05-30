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
                .Length(2, 50).WithMessage("El numero de caracteres debe estar entre 2 y 50");

            RuleFor(category => category.Description)
                .NotNull().WithMessage("La descripcion es requerido")
                .Length(3, 250).WithMessage("El numero de caracteres debe estar entre 3 y 250");
        }
    }
}
