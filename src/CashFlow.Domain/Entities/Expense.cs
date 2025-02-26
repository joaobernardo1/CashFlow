using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class Expense
    {
        public string Title = string.Empty;
        public string? Description;
        public DateTime Date;
        public decimal Amount;
        public PaymentType PaymentType;
    }
}
