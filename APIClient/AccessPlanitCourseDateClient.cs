using Models;
using Services;
using Constants;
using static APIClient.APIModel.APIModelUtility;
using Services.Models;

namespace APIClient
{
    public class AccessPlanitCourseDateClient : AccessPlanitAPIClientBase
    {
        protected override APIModule Module => APIModule.CourseDate;

        public AccessPlanitCourseDateClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

        public async Task<List<CourseDate>?> GetCourseDatesForCourseId(string courseId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                MakeRequestUrl<CourseDate>()
                .AddQuery(propertyName: GetPropertyName<CourseDate>(c => c.CourseID), query: QueryOperations.Filter, filterExpression: FilterExpressions.Equals, filterValue: courseId)
                .ToUrlString()
                );

            try
            {
                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var results = await ConvertResponseCollection<CourseDate>(response);

                    return results;
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage("Something went wrong trying to communicate with the API");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }                

        public async Task<List<CourseDate>?> GetCourseDates()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                MakeRequestUrl()
                .ToUrlString()
                );

            try
            {
                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var results = await ConvertResponseCollection<CourseDate>(response);

                    return results;
                }
                else
                {
                    StateManagementService.SetLatestErrorMessage("Something went wrong trying to communicate with the API");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
