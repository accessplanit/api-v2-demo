using Constants;
using Services;

namespace APIClient;

public class AccessPlanitUsersElearningPackageClient : AccessPlanitAPIClientBase
{
    public AccessPlanitUsersElearningPackageClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
    {
    }

    protected override APIModule Module => APIModule.UserCourseDateELearning;
}
