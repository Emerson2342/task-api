using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.ValueObjects;
using TaskList.Components.Domain.Shared.Entities;

namespace TaskList.Components.Domain.Main.Entities
{
    public class User : Entity
    {
       
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public Email Email { get; set; } = new(string.Empty);
        [JsonPropertyName("password")]
        public Password Password { get; set; } = new(string.Empty);
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("verificationCode")]
        public string VerificationCode { get; set; } = string.Empty;
        [JsonPropertyName("isEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; } = false;


        public IList<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
        public User() { }

        public User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
            VerificationCode = Guid.NewGuid().ToString("N")[..10].Replace('.', 'l').ToUpper();
            Token = NewToken();
        }
        public static string NewToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }

    }
}
