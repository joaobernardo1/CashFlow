using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using MigraDoc.DocumentObjectModel;

namespace CashFlow.Application.PublicValidator
{
    public class PassowordValidator<T> : PropertyValidator<T,string>
    {
        public override string Name => "PasswordValidator";
        private const string ERROR_MESSAGE = "ErrorMessage";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            if(password.Length < 8)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            if(Regex.IsMatch(password, @"[A-Z]+") == false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            if(Regex.IsMatch(password, @"[a-z]+") == false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            if(Regex.IsMatch(password, @"[0-9]+") == false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            if(Regex.IsMatch(password, @"[\!\@\#\$\%\^\&\*\(\)\+\=\-]+") == false)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, "Invalid Password");
                return false;
            }
            return true;
        }
    }
}
