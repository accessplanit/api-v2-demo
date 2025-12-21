using Models;
using Services;
using Constants;

namespace APIClient
{
    public class AccessPlanitUserClient : AccessPlanitAPIClientBase
    {
        // Set module to for the request is base class
        protected override APIModule Module => APIModule.User;

        public AccessPlanitUserClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

        // Example of updating users details
        public async Task<bool> UpdateUser(string userid, Dictionary<string, object> updatedUserDetails)
            => await Update<User>(userid, updatedUserDetails);
	}
}