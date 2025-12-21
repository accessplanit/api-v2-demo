using Services;

namespace APIClient
{
	public class AccessPlanitAPIClient : AccessPlanitAPIClientBase
	{
		public AccessPlanitAPIClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
		{
		}
	}
}