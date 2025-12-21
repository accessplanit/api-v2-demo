using Constants;
using Models;
using Newtonsoft.Json;
using Services;
using System.Text;

namespace APIClient
{
    public class AccessPlanitAPIBasketClient : AccessPlanitAPIClientBase
    {
        public AccessPlanitAPIBasketClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }


        public async Task<Basket> GetBasketForUser(string user)
        {
            var requestUri = BaseUrl + $"/{user}";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUri
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Basket>(responseString);
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage("Something went wrong trying to get the basket.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Basket> RemoveItemFromBasket(string uniqueBasketID, int itemID)
        {
            var requestUri = BaseUrl + $"/{uniqueBasketID}/{itemID}";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Delete,
                    requestUri
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<Basket>(responseString);
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage($"Something went wrong trying to delete the item with ID: {itemID.ToString()} from Basket: {uniqueBasketID}.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Basket> AddItemToBasket(string userID, string itemID, int IDType)
        {
            var requestUri = BaseUrl + $"/{userID}/{itemID}/{IDType}";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    requestUri
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Basket>(responseString);
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage($"Something went wrong trying to add the item with ID: {itemID.ToString()} to basket for user: {userID}.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Basket> RemoveUserFromBasketItem(string uniqueBasketID, int itemID, string userID)
        {
            var requestUri = BaseUrl + $"/{uniqueBasketID}/{itemID}/{userID}";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Delete,
                    requestUri
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Basket>(responseString);
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage($"Something went wrong trying to remove the user with ID: {userID} for item with ID: {itemID.ToString()} from Basket: {uniqueBasketID}.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Basket> AddUsersToBasketItem(string uniqueBasketID, int itemID, string[] userIDs)
        {
            var requestUri = BaseUrl + $"/{uniqueBasketID}/{itemID}/users";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Put,
                    requestUri
                );

                string jsonBody = JsonConvert.SerializeObject(userIDs);

                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Basket>(responseString);
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage($"Something went wrong trying to add users to item with ID: {itemID.ToString()} from Basket: {uniqueBasketID}.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CompleteBasket(string uniqueBasketID)
        {
            var requestUri = BaseUrl + $"/{uniqueBasketID}/complete";

            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    requestUri
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage($"Error trying to complete Basket: {uniqueBasketID}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return false;
            }
        }

        private string BaseUrl => AccessPlanitAPIConfig.BaseBasketAddress;
    }

}
