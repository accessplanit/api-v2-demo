using Constants;
using Models;
using Services;

namespace APIClient
{
    public class AccessPlanitTokenClient : AccessPlanitAPIClientBase
    {
        private const string SuperAPIUserID = "<<ENTER USER ID HERE>>";
        private const string SuperAPIPassword = "<<ENTER PASSWORD HERE>>";
        private const string SuperUserAPIKey = "<<ENTER API KEY HERE>>";

        protected override APIModule Module => APIModule.Token;

        public AccessPlanitTokenClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }


        /// <summary>
        /// Requests an authentication token using the configured user credentials.
        /// </summary>
        /// <remarks>If the token request fails or an exception occurs, an error message is set in the
        /// state management service. The returned token is also stored in the state management service upon successful
        /// retrieval.</remarks>
        /// <returns>A <see cref="Token"/> object representing the authentication token if the request is successful; otherwise,
        /// <see langword="null"/>.</returns>
        public async Task<Token>? GetToken()
        {
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "UserName", SuperAPIUserID },
                { "Password", SuperAPIPassword }
            });

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, 
                    MakeRequestUrl()
                    .ToUrlString()
                    );

                request.Content = formContent;

                var response = await base.SendAsync(request);

                if (response is not null && response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var token = TokenHandler.ConvertAccessPlanitToken(responseContent);
                    StateManagementService.SetToken(token);
                    return token;
                }
                else
                {
                    var status = response is null ? "no response" : response.StatusCode.ToString();
                    StateManagementService.SetLatestErrorMessage($"Could not get an API token. Status: {status}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Could not get an API token. {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Retrieves the current API key as a bearer token for authentication purposes.
        /// </summary>
        /// <remarks>The returned token is immediately set as the current token in the state management
        /// service. Use this method when direct API key authentication is required in token form.</remarks>
        /// <returns>A <see cref="Token"/> object containing the API key as the access token. The returned token is always
        /// non-null.</returns>
        public async Task<Token>? GetAPIKeyAsToken()
        {
            var token = new Token()
            {
                AccessToken = SuperUserAPIKey,
                TokenType = "Bearer"
            };

            StateManagementService.SetToken(token);
            return token;
        }
    }
}