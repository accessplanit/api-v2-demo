using Models;
using Services;
using Constants;

namespace APIClient
{
    public class AccessPlanitUserCourseDateWebClient : AccessPlanitAPIClientBase
    {
        public AccessPlanitUserCourseDateWebClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

        protected override APIModule Module => APIModule.UserCourseDateWeb;

    }
}
