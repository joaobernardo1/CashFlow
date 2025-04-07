using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
namespace CommonUtilitiesTest

{
    public class RequestExpensesBuilder
    {
        public static RequestExpenseJson Build()
        {
            var faker = new Faker("en");

            return new Faker<RequestExpenseJson>()
            .RuleFor(x => x.Title, faker => faker.Lorem.Sentence())
            .RuleFor(x => x.Time, faker => faker.Date.Past())
            .RuleFor(x => x.Description, faker => faker.Lorem.Paragraph())
            .RuleFor(x => x.PaymentType, faker => faker.PickRandom<PaymentType>())
            .RuleFor(x => x.Amount, faker => faker.Random.Decimal(0, 1000));
        }
    }
}
