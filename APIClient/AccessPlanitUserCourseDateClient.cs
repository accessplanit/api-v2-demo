using Models;
using Services;
using Constants;

namespace APIClient
{
    public class AccessPlanitUserCourseDateClient : AccessPlanitAPIClientBase
    {
        public AccessPlanitUserCourseDateClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

        protected override APIModule Module => APIModule.UserCourseDate;

        public async Task<bool> CreateUserCourseDate(UserCourseDate userCourseDate)
           => await Create(userCourseDate);
    }
}
