


using Blazored.SessionStorage;
using TaskList.Components.Domain.Main.DTOs.UserDTOs;
using TaskList.Components.Domain.Main.UseCases.ResponseCase;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

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
        if (result == null)
            return new Response("Erro ao converter o Json", 500);
        return result;
    }

    public async Task<bool> StoreTokenAsync(string token)
    {
        var isValid = ValidateToken(token);
        await _sesionStorage.SetItemAsync("authToken", token);
        return isValid;
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
    public async Task Logout()
    {
        await _sesionStorage.RemoveItemAsync("authToken");
        await _sesionStorage.RemoveItemAsync("userId");
    }

    public bool ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "subject");

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;

                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

}