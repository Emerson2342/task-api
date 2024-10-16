

using Blazored.LocalStorage;
using Blazored.SessionStorage;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;

public class Authenticationservice
{
    private readonly HttpClient _http;
    private readonly ISessionStorageService _sesionStorage;

    public Authenticationservice(HttpClient httpClient, ISessionStorageService sessionStorage)
    {
        _http = httpClient;
        _sesionStorage = sessionStorage;
    }

    public async Task<Response> Login(RequestLogin login)
    {
        var response = await _http.PostAsJsonAsync("login", login);


        var result = await response.Content.ReadFromJsonAsync<Response>();
        //await _sesionStorage.SetItemAsync("authToken",login);

        return result;
    }

    public async Task<string> GetTokenAsync()
    {
        return await _sesionStorage.GetItemAsync<string>("authToken");
    }

}