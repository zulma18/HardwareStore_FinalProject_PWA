using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.FirstName)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("El nombre es requerido");

            RuleFor(employee => employee.LastName)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("El apellido es requerido");

            RuleFor(employee => employee.Phone)
                .MaximumLength(20).WithMessage("El numero maximo de caracteres es 20")
                .NotNull().WithMessage("El telefono es requerido");

            RuleFor(employee => employee.Email)
                .MaximumLength(50).WithMessage("El numero de caracteres debe estar entre 50")
                .NotNull().WithMessage("El correo es requerido");

            RuleFor(employee => employee.Salary)
                .GreaterThan(0).WithMessage("No se aceptan varlores negativos")
                .NotNull().WithMessage("El salario es requerido");

            RuleFor(employee => employee.Address)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La direccion es requerida");

            RuleFor(employee => employee.City)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La ciudad es requerida");
        }
    }
}
