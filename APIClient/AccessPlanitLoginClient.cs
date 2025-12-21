using Constants;
using Models;
using Services;
using System.Net.Http.Json;

namespace APIClient;

public class AccessPlanitLoginClient : AccessPlanitAPIClientBase
{
    protected override APIModule Module => APIModule.Login;

    public AccessPlanitLoginClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
    {
    }

    public async Task<LoginResultModel> Login(LoginForm loginForm)
    {
        string username = loginForm.Username;
        string password = loginForm.Password;

        var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "UserName", username },
                { "Password", password }
            });

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, 
                    MakeRequestUrl()
                    .ToUrlString()
                    );

                request.Content = formContent;

                var response = await base.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResultModel>();
                    if (result.Success)
                        return result;
                    else
                        return new LoginResultModel{ Message = result.Message};
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage("incorrect credentials");
                    return new LoginResultModel();
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"{ex.Message}");
                return new LoginResultModel();
            }
    }
}
