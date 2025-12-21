using Constants;
using Models;
using Services;

namespace APIClient
{
    public class AccessPlanitCourseDateBookingClient : AccessPlanitAPIClientBase
    {
		protected override APIModule Module => APIModule.CourseDateBooking;

		public AccessPlanitCourseDateBookingClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

	}
}
