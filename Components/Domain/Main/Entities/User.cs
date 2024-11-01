using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
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
        [JsonPropertyName("isEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; } = false;


        public IList<TaskEntity>? Tasks { get; set; }
        public User() { }

        private User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
            Token = NewToken();
        }

        public static UserResult CreateUser(string name, string email, string password)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception($"Favor preencher o campo NOME!");
            if (string.IsNullOrEmpty(email))
                throw new Exception($"Favor preencher o campo EMAIL!");
            if (string.IsNullOrEmpty(password))
                throw new Exception($"Favor preencher o campo SENHA!");

            Email userEmail = new(email);
            Password userPassword = new(password);

            User user = new(name, userEmail, userPassword);
            return new UserResult(new Response("Usuário criado com sucesso!", user), user);
         
        }
        public class UserResult
        {
            public Response Response { get; set; }
            public User? User { get; set; }

            public UserResult(Response reponse, User user)
            {
                Response = reponse;
                User = user;
            }

            public UserResult(Response response)
            {
                Response = response;
            }
        }
        public static string NewToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }
    }
}
