namespace CashFlow.Communication.Responses
{
    public class ResponseRegisterExpenseJson
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }

    }
}
