using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(client => client.FirstName)
                .Length(4, 50).WithMessage("El numero de caracteres debe estar entre 4 y 50")
                .NotNull().WithMessage("El nombre es requerido");

            RuleFor(client => client.LastName)
                .Length(4, 50).WithMessage("El numero de caracteres debe estar entre 4 y 50")
                .NotNull().WithMessage("El apellido es requerido");

            RuleFor(client => client.Email)
                .MaximumLength(50).WithMessage("El numero de caracteres debe estar entre 50")
                .NotNull().WithMessage("El correo es requerido");

            RuleFor(client => client.Phone)
                .MaximumLength(20).WithMessage("El numero maximo de caracteres es 20")
                .NotNull().WithMessage("El telefono es requerido");

            RuleFor(client => client.Address)
                .Length(3, 250).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La direccion es requerida");

            RuleFor(client => client.City)
                .Length(3, 50).WithMessage("El numero de caracteres debe estar entre 3 y 50")
                .NotNull().WithMessage("La ciudad es requerida");
        }
    }
}
