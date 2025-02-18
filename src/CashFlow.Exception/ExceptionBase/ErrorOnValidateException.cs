namespace CashFlow.Exception.ExceptionBase
{
     public class ErrorOnValidateException : CashFlowException
    {
        public List<string> ErrorMessages { get; private set; }

        public ErrorOnValidateException(string errorMessage)
        {
            ErrorMessages = [errorMessage];
        }

        public ErrorOnValidateException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
