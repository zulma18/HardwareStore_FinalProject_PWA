using FluentValidation;
using HardwareStore.Models;

namespace HardwareStore.Validations
{
    public class LoginValidator : AbstractValidator<Logins_Model>
    {
        public LoginValidator()
        {
            RuleFor(product => product.LoginUser)
                .Length(3, 50).WithMessage("El numero de caracteres minimo debe es 3 ")
                .NotNull().WithMessage("El nombre del administrador es requerido");

            RuleFor(product => product.LoginPassword)
                .NotNull().WithMessage("la contraseña es requerido");

            RuleFor(product => product.Id_Rol)
                .NotNull().WithMessage("El Id del rol es requerido");
        }
    }
}
