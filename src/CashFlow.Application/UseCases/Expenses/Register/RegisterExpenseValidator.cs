﻿using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required.");
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The amount most be greater than zero.");
            RuleFor(expense => expense.Time).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expanses cannot be for the future");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("The payment type is invalid");
        }
    }
}
