


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
        return result;
    }

    public async Task StoreTokenAsync(string token)
    {
        await _sesionStorage.SetItemAsync("authToken", token);
    }
    public async Task StoreUserIdAsync(Guid userId)
    {
        await _sesionStorage.SetItemAsync("userId", userId);
    }

    public async Task<string> GetTokenAsync()
    {
        return await _sesionStorage.GetItemAsync<string>("authToken");
    }
    public async Task<string> GetUserIdAsync()
    {
        return await _sesionStorage.GetItemAsync<string>("userId");
    }
    public async Task RemoveTokenAsync()
    {
         await _sesionStorage.RemoveItemAsync("authToken");
         await _sesionStorage.RemoveItemAsync("userId");
    }

}