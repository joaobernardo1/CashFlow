using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_IS_REQUIRED);
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_GREATHER_ZERO);
            RuleFor(expense => expense.Time).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessage.FUTURE_EXPENSES);
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessage.PAYMENT_TYPE_INVALID);
        }
    }
}
