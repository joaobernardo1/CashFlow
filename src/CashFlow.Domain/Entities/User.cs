using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = Roles.MEMBER;
        public Guid UserIdentifyer { get; set; }
    }
}
