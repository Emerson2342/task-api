using TaskList.Components.Domain.Main.DTOs;
using TaskList.Components.Domain.Main.Entities;
using TaskList.Components.Domain.Main.UseCases.Contracts;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using TaskList.Components.Domain.Main.ValueObjects;

namespace TaskList.Components.Domain.Main.UseCases.Create
{
    public class Handler
    {
        private readonly IRepository _repository;
        private readonly string ipLocal = "192.168.10.10";

        public Handler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> CreateUser(RequestCreateUser newUser)
        {
            //var exists = await _repository.AnyAsync(newUser.Email);

            //if (exists)
            //    return new Response("Já existe uma conta com esse email", 401);

            Email userEmail;
            Password userPassword;

            userEmail = new Email(newUser.Email);
            userPassword = new Password(newUser.Password);

            User user = new(newUser.Name, userEmail, userPassword);
            await _repository.SaveAsync(user);

            return new Response("Usuário criado com sucesso!" +
                  " Favor verificar seu email para confirmaçao de conta!", new ResponseData(newUser.Name, newUser.Email, null));

        }
    }
}
