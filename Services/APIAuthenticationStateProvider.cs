using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        private readonly StateManagementService _stateManagementService;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage, StateManagementService stateManagementService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _stateManagementService = stateManagementService;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Token token = null;
            try
            {
                string tokenFromStorage = await _localStorage.GetItemAsync<string>("authToken");
                token = JsonSerializer.Deserialize<Token>(tokenFromStorage);
            }
            catch (Exception ex)
            {
                _stateManagementService.SetLatestErrorMessage(ex.Message);
            }

            if (token is null)
            {
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            return 
                new AuthenticationState( 
                    new ClaimsPrincipal(
                        new ClaimsIdentity(
                            [
                            new Claim(token.AccessToken, "AccessToken"),
                            new Claim(token.UserID, "UserID")
                            ], "AccessPlanit"
                        )
                    )
                );
        }

        public async Task MarkUserAsAuthenticated(Token token)
        {
            try
            {
                await SetTokenInLocalStorage(token);

                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity( 
                    [ 
                    new Claim(token.AccessToken, "AccessToken"),
                    new Claim(token.UserID, "UserID")
                    ], "AccessPlanit"));

                var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

                NotifyAuthenticationStateChanged(authState);
            }
            catch(Exception ex){
                _stateManagementService.SetLatestErrorMessage(ex.Message);
            }
        }

        public async Task SetTokenInLocalStorage(Token token) =>
            await _localStorage.SetItemAsync("authToken", token);          

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");

            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
        }
    }
}
