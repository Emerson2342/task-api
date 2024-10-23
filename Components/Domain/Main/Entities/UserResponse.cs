using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.ValueObjects;

namespace TaskList.Components.Domain.Main.Entities
{
    public class UserResponse
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public Email Email { get; set; } = new(string.Empty);

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        
        [JsonPropertyName("isEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; } = false;
    }
}
