using Bogus;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CommonUtilitiesTest;

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
            Assert.True(response.IsValid);
        }
    }
}
