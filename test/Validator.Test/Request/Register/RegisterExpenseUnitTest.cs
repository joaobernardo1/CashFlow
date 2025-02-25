using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonUtilitiesTest;
using Shouldly;

namespace Validator.Test.Request.Register
{
    public class RegisterExpenseUnitTest
    {
        [Fact]
        public void Sucess()
        {
            var request = RequestExpensesBuilder.Build();
            var validator = new RegisterExpenseValidator();
            var response = validator.Validate(request);
            response.IsValid.ShouldBeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void Error_Title_Is_Required(string title)
        {
            var request = RequestExpensesBuilder.Build();
            request.Title = title;
            var validator = new RegisterExpenseValidator();
            var response = validator.Validate(request);

            response.IsValid.ShouldBeFalse();
            response.Errors.ShouldSatisfyAllConditions(
            x => x.ShouldBeUnique(),
            x => x.ShouldContain(x => x.ErrorMessage == ResourceErrorMessage.TITLE_IS_REQUIRED));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_Amount_Greather_Zero(decimal amount)
        {
            var request = RequestExpensesBuilder.Build();
            request.Amount = amount;
            var validator = new RegisterExpenseValidator();
            var response = validator.Validate(request);

            response.IsValid.ShouldBeFalse();
            response.Errors.ShouldSatisfyAllConditions(
                x => x.ShouldBeUnique(),
                x => x.ShouldContain(x => x.ErrorMessage == ResourceErrorMessage.AMOUNT_GREATHER_ZERO)
                );
        }
        [Fact]
        public void Error_Future_Expenses()
        {
            var request = RequestExpensesBuilder.Build();
            var validator = new RegisterExpenseValidator();
            request.Time = DateTime.UtcNow.AddDays(1);
            var response = validator.Validate(request);

            response.IsValid.ShouldBeFalse();
            response.Errors.ShouldSatisfyAllConditions(
                x => x.ShouldBeUnique(),
                x => x.ShouldContain(x => x.ErrorMessage == ResourceErrorMessage.FUTURE_EXPENSES)
                );
        }
        [Fact]
        public void Error_PaymentType_Invalid()
        {
            var request = RequestExpensesBuilder.Build();
            var validator = new RegisterExpenseValidator();
            request.PaymentType = (PaymentType)800;
            var response = validator.Validate(request);

            response.IsValid.ShouldBeFalse();
            response.Errors.ShouldSatisfyAllConditions(
                x => x.ShouldBeUnique(),
                x => x.ShouldContain(x=>x.ErrorMessage == ResourceErrorMessage.PAYMENT_TYPE_INVALID)
                );
        }
    }
}
