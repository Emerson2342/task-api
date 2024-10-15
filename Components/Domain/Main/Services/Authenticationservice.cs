

using Blazored.LocalStorage;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;

public class Authenticationservice
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public Authenticationservice(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _http = httpClient;
        _localStorage = localStorageService;
    }

    public async Task<Response> Login(RequestLogin login)
    {
        var response = await _http.PostAsJsonAsync("login", login);


        var result = await response.Content.ReadFromJsonAsync<Response>();
        await _localStorage.SetItemAsync("authToken", "123456");

        return result;
    }

    public async Task<string> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }

    public async Task LoadToken()
    {
        await _localStorage.SetItemAsync("authToken", "123456");
    }


}