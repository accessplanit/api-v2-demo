using Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Services
{
    public class TokenHandler : DelegatingHandler
    {
        private StateManagementService StateManagementService { get; set; }

        public TokenHandler(StateManagementService stateManagementService)
        {
            StateManagementService = stateManagementService;
        }

        public Token ConvertAccessPlanitToken(object token)
        {
            if (token is not string tokenString)
            {
                throw new ArgumentException("Token must be a JSON string", nameof(token));
            }

            var jsonObject = JsonDocument.Parse(tokenString).RootElement;

            Token accessPlanitToken = new Token()
            {
                AccessToken = jsonObject.GetProperty("access_token").GetString(),
                TokenType = jsonObject.GetProperty("token_type").GetString(),
                ExpiresIn = jsonObject.GetProperty("expires_in").GetInt32(),
                RefreshToken = jsonObject.GetProperty("refresh_token").GetString(),
                UserID = jsonObject.GetProperty("userID").GetString(),
                TokenExpiry = jsonObject.GetProperty("refresh_token_expires_in").GetInt32()
            };

            return accessPlanitToken;
        }
    }
}
