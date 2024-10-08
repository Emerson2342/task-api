using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public Password Password { get; set; }
        public string PartialToken { get; set; }
        public string VerificationCode { get; private set; }
        public bool IsEmailConfirmed { get; set; } = false;
        protected User() { }

        public User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
            VerificationCode = Guid.NewGuid().ToString("N")[..10].Replace('.', 'l').ToUpper();
            PartialToken = NewToken();
        }
        public static string NewToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }

    }
}
