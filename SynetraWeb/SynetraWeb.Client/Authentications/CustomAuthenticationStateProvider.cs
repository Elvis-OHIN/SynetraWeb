using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;

namespace SynetraWeb.Client.Authentications
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var identity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public void AuthenticateUser(string userIdentifier)
            {
                var identity = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, userIdentifier),
        }, "Custom Authentication");

                var user = new ClaimsPrincipal(identity);

                NotifyAuthenticationStateChanged(
                    Task.FromResult(new AuthenticationState(user)));
            }
        public async Task UpdateAuthenticationState(AuthenticationModel authenticationModel)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = new();
                if (authenticationModel is not null)
                {
                    claimsPrincipal = SetClaims(authenticationModel.Username!);
                    await localStorageService.SetItemAsStringAsync("Authentication", Serialize(authenticationModel));
                }
                else
                {
                    await localStorageService.RemoveItemAsync("Authentication");
                    claimsPrincipal = anonymous;
                }
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch
            {
                await Task.FromResult(new AuthenticationState(anonymous));
            }


        }

        private ClaimsPrincipal SetClaims(string email) => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, email)
        }, "CustomAuth"));
        private AuthenticationModel Deserialize(string serializeString) => JsonSerializer.Deserialize<AuthenticationModel>(serializeString)!;
        private string Serialize(AuthenticationModel model) => JsonSerializer.Serialize(model);
    }

    
}
