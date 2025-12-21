using Models;
using Services;
using Constants;
using APIClient.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Services.Models;
using Models.Interfaces;

namespace APIClient
{
    public abstract class AccessPlanitAPIClientBase : IAPIClient
    {
        public AccessPlanitAPIClientBase(HttpClient httpClient, StateManagementService stateManagementService)
        {
            HttpClient = httpClient;
            StateManagementService = stateManagementService;
            TokenHandler = new TokenHandler(stateManagementService);
        }

        required public HttpClient HttpClient { get; set; }

        public TokenHandler TokenHandler { get; set; }

        public StateManagementService StateManagementService { get; set; }

        public APIObjectResponseHandler APIObjectResponseHandler { get; set; } = new();

        protected virtual APIModule Module { get; set; }

        public void SetAuthentication(Token token) 
            => HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        public virtual async Task<List<T>> GetAll<T>() where T : class, new()
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    MakeRequestUrl<T>()
                    .GetAll()
                    .ToUrlString()
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return await ConvertResponseCollection<T>(response);
                else
                    StateManagementService.SetLatestErrorMessage($"Something went wrong whilst trying to get all records in module");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong. {ex.Message}");
            }

            return null;
        }

        public virtual async Task<List<T>> GetByCriteria<T>(List<Filter> filters, IEnumerable<string>? propertyNames = null) where T : class, new()
        {
            try
            {
                var url = MakeRequestUrl<T>();

                // Add property names if provided
                if (propertyNames != null && propertyNames.Any())
                {
                    url = url.AddQuery(
                        query: QueryOperations.Select,
                        propertyNames: propertyNames
                    );
                }

                // Add filters if provided
                if (filters != null && filters.Any())
                {
                    url = url.AddQuery(
                        query: QueryOperations.Filter,
                        filters: filters
                    );
                }

                var request = new HttpRequestMessage(HttpMethod.Get, url.ToUrlString());

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return await ConvertResponseCollection<T>(response);
                else
                    StateManagementService.SetLatestErrorMessage($"Failed to get records by criteria. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with the API. {ex.Message}");
            }

            return null;
        }        

        public virtual async Task<bool> Create<T>(T model) where T : class, new()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post,
                    MakeRequestUrl<T>()
                    .ToUrlString()
                );

                request = SetJsonContent(request, model);

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                        StateManagementService.SetLatestErrorMessage($"Failed to create entity. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with the API. {ex.Message}");
            }

            return false;
        }

        public virtual async Task<bool> Update<T>(string id, Dictionary<string, object> updatedModelProperties) where T : class, new()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put,
                    MakeRequestUrl()
                    .AddPath(id)
                    .ToUrlString()
                );

                request = SetJsonContent(request, updatedModelProperties);

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    StateManagementService.SetLatestErrorMessage($"Failed to update entity with ID: {id}. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with the API. {ex.Message}");
            }

            return false;
        }

        public virtual async Task<bool> Delete<T>(string id) where T : class, new()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete,
                    MakeRequestUrl<T>()
                    .AddPath(id)
                    .ToUrlString()
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    StateManagementService.SetLatestErrorMessage($"Failed to delete entity with ID: {id}. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with the API. {ex.Message}");
            }

            return false;
        }

        protected bool ValidateModuleIsSupportedByAPI<T>() where T : class, new()
        {
            var model = new T();

            if (model is IAccessPlanitModel accessPlanitModel)
                return true;

            return false;
        }

        public string GetModelID(IAccessPlanitModel model)
        {
            if (string.IsNullOrEmpty(model.ID))
                throw new ArgumentException("The model must have a valid ID.", nameof(model));

            return model.ID;
        }

        public virtual async Task<T> GetById<T>(string id) where T : class, new()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    MakeRequestUrl<T>()
                    .AddQuery(QueryOperations.Filter, "ID", FilterExpressions.Equals, id)
                    .ToUrlString()
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return await ConvertResponse<T>(response);
                else
                    StateManagementService.SetLatestErrorMessage($"Failed to get entity with ID: {id}. Status: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with the API. {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Make url for request
        /// </summary>
        public FluentUrlBuilder MakeRequestUrl<T>() where T : class, new()
        {
            string baseUrl = CreateBaseUrlWithModelProperties<T>();

            var builder = new FluentUrlBuilder(baseUrl);

            return builder;
        }

        public FluentUrlBuilder MakeRequestUrl()
        {
            string baseUrl = CreateBaseUrl();

            var builder = new FluentUrlBuilder(baseUrl);

            return builder;
        }

        public string CreateBaseUrlWithModelProperties<T>() where T : class, new()
        {
            string baseUrl = string.Empty;

            if (ValidateModuleIsSupportedByAPI<T>())
            {
                this.Module = GetModelModule<T>();

                baseUrl = CreateBaseUrl();

                baseUrl = PrepareURLWithTypeProperties<T>(baseUrl);
            }
            else
            {
                baseUrl = CreateBaseUrl();
            }

            return baseUrl;
        }

        public string CreateBaseUrl()
        {
            string baseUrl = HttpClient.BaseAddress.AbsoluteUri + $"{Module.ToModuleString()}";

            return baseUrl;
        }

        public APIModule GetModelModule<T>() where T : class, new()
        {
            try
            {
                var model = new T();

                if (model is IAccessPlanitModel accessPlanitModel)
                {
                    if (accessPlanitModel.Module is APIModule module)
                        return module;

                    throw new ArgumentException("The model does not have a valid APIModule.");
                }
                else
                    throw new ArgumentException("The model is not an IAccessPlanitModel.");
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The model type does not have a corresponding module of:", nameof(APIModule), ex);
            }
        }

        public string PrepareURLWithTypeProperties<T>(string baseUrl) where T : class, new()
        {
            var model = new T();
            var builder = new FluentUrlBuilder(baseUrl);
            var properties = new List<string>();

            foreach (var property in model.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(APIModule))
                    properties.Add(property.Name);
            }

            if (properties.Any())
                builder.AddQuery(QueryOperations.Select, propertyNames: properties);

            return builder.ToUrlString();
        }

        public HttpRequestMessage SetJsonContent<T>(HttpRequestMessage request, T requestContent)
        {

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            var jsonPayload = JsonSerializer.Serialize(requestContent, options);

            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            return request;
        }

        protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken? cancellationToken = default)
        {
            var token = StateManagementService.Token;

            if (token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            try
            {
                return await HttpClient.SendAsync(request, cancellationToken ?? CancellationToken.None); ;
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }

        protected async Task<T?> ConvertResponse<T>(HttpResponseMessage response) where T : class, new() =>
            await APIObjectResponseHandler.ConvertResponseObjectToModel<T>(response);

        protected async Task<List<T>?> ConvertResponseCollection<T>(HttpResponseMessage response) where T : class, new() =>
            await APIObjectResponseHandler.ConvertResponseObjectToModel<List<T>>(response);
    }
}
