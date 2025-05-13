using CashFlow.Application.PublicValidator;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.User
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty()
                .WithMessage("ERRO");

            RuleFor(user => user.Email).NotEmpty().
                WithMessage("Email vazio")
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(user => user.Password).SetValidator(new PassowordValidator<RequestRegisterUserJson>());
        }
    }
}
