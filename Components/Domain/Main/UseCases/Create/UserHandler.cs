using System.IdentityModel.Tokens.Jwt;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.Services;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Domain.Main.ValueObjects;

namespace TaskList.Components.Domain.Main.UseCases.Create
{
    public class UserHandler
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;
        private readonly string Ip = Configuration.Ip.IpAddress;
      
        public UserHandler(IUserRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Response> CreateUser(RequestCreateUser newUser)
        {
            var exists = await _repository.AnyAsync(newUser.Email);



            if (exists)
                return new Response("Já existe uma conta com esse email", 401);
            Email userEmail;
            Password userPassword;
            try
            {
                userEmail = new Email(newUser.Email);
                userPassword = new Password(newUser.Password);

                User user = new(newUser.Name, userEmail, userPassword);
                string link = $"<a href='https://{Ip}/confirmation/{user.Token}' target='_blank'>Clique aqui para confirmar seu e-mail</a>" +
                    $"<br>Se preferir, cole isso no seu navegador <br> " +
                    $"https://{Ip}/confirmation/{user.Token}";

                var email = new EmailService();

                email.Send(
                    user.Name,
                    user.Email.Address,
                    "Link de verificação",
                    $"Clique no link para confirmar o email\n{link}"
                    );

                await _repository.SaveAsync(user);

                return new Response("Usuário criado com sucesso!" +
                    " Favor verificar seu email para confirmaçao de conta!", 201);
            }
            catch (Exception ex)
            {
                return new Response($"Erro ao criar usuário. {ex.Message}", 500);
            }
        }

        public async Task<Response> GetUser(string userId)
        {
            var user = await _repository.GetUserByTokenAsync(userId);

            if (user == null)
                return new Response($"Usuário inválido!", 400);


            return new Response("Usuário válido!", user);
        }
        public async Task<Response> ConfirmEmail(string token)
        {
            var user = await _repository.GetUserByTokenFromRouteAsync(token);

            if (user == null)
                return new Response($"Token inválido!", 400);

            if (user.IsEmailConfirmed)
                return new Response($"Este Email já foi confirmado", 400);

            user.IsEmailConfirmed = true;
            user.Token = User.NewToken();

            _repository.UpdateUser(user);
            await _repository.SaveChangesAsync();

            return new Response("Email verificado, conta liberada para o acesso!", 201);
        }
        public async Task<Response> Login(RequestLogin login)
        {
            var user = await _repository.GetUserByEmailAsync(login.Email);

            if (user == null || !Password.Verify(user.Password.PassWord, login.Password))
                return new Response($"Usuário e/ou senha estão incorretos", 400);

            if (!user.IsEmailConfirmed)
                return new Response($"Email não confirmado, favor confirmar o email!", 400);

            var token = _tokenService.CreateToken(user);

            return new Response("Login efetuado com sucesso!", user, token);
        }
        public async Task<Response> ChangePasswordLogged(string userToken, RequestPassword newPassword)
        {
            string password = newPassword.NewPassword;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(userToken);

            string id = jwtToken.Claims.FirstOrDefault(c => c.Type == "subject")?.Value ?? string.Empty;

            var user = await _repository.GetUserByTokenAsync(id);

            if (user == null)
                return new Response("Email não cadastrado.", 400);

            if (Password.Verify(user.Password.PassWord, newPassword.NewPassword))
                return new Response("Nova senha não pode ser igual à anterior!", 400);

            user.Password = new Password(newPassword.NewPassword);
            _repository.UpdateUser(user);
            await _repository.SaveChangesAsync();

            return new Response($"Senha alterada com sucesso!", 201);
        }

        public async Task<Response> ResetPasswordNotLogged(RequestEmail requestEmail)
        {
            string userEmail = requestEmail.Email;
            var user = await _repository.GetUserByEmailAsync(userEmail);

            if (user == null)
                return new Response("Email não cadastrado.", 400);

            user.Token = User.NewToken();
            _repository.UpdateUser(user);
            await _repository.SaveChangesAsync();

            string link = $"<a href='https://{Ip}/reset-password/{user.Token}' target='_blank'>Clique aqui para confirmar seu e-mail</a>" +
                    $"<br>Se preferir, cole isso no seu navegador <br> " +
                    $"https://{Ip}/reset-password/{user.Token}";

            EmailService email = new();

            email.Send(
                user.Name,
                user.Email.Address,
                "Recuperar Senha",
                $"Clique no link para recuperar a senha\n{link}"
                );
            return new Response($"Email enviado com sucesso!", 201);
        }

        public async Task<Response> ResetPassword(string token, string newPassword)
        {
            var user = await _repository.GetUserByTokenFromRouteAsync(token);

            if (user == null)
                return new Response($"Token inválido!", 400);
            if (string.IsNullOrEmpty(newPassword))
                return new Response("Senha não pode ser vazia!", 400);


            if (Password.Verify(user.Password.PassWord, newPassword))
                return new Response("Nova senha não pode ser igual à anterior!", 400);

            user.Password = new Password(newPassword);
            user.Token = User.NewToken();
            _repository.UpdateUser(user);
            await _repository.SaveChangesAsync();

            return new Response($"Senha alterada com sucesso!", 201);
        }

    }
}
