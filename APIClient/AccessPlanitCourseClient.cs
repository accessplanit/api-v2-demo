using static APIClient.APIModel.APIModelUtility;

using Models;
using Services;
using Constants;
using Services.Models;

namespace APIClient
{
    public class AccessPlanitCourseClient : AccessPlanitAPIClientBase
    {
        protected override APIModule Module => APIModule.Course;

        public AccessPlanitCourseClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
        {
        }

        public async Task<List<Course>?> GetCourses()
            => await GetAll<Course>();

        public async Task<Course> GetCourse(string courseID)
            => await GetById<Course>(courseID);

        public async Task<bool> DeleteCourse(string courseID)
            => await Delete<Course>(courseID);

        public async Task<List<Course>?> GetFilteredCourses(string? label = null, string? courseType = null)
        {
            try
            {
                var filters = new List<Filter>();

                if (!string.IsNullOrEmpty(label))
                    filters.Add(new Filter(GetPropertyName<Course>(c => c.Label), FilterExpressions.Contains, label));

                if (!string.IsNullOrEmpty(courseType))
                    filters.Add(new Filter(GetPropertyName<Course>(c => c.CourseType), FilterExpressions.Equals, courseType));

                var requestUrl = MakeRequestUrl<Course>().GetAll();

                if (filters.Count > 0)
                {
                    requestUrl.AddQuery(
                        query: QueryOperations.Filter,
                        filters: filters
                    );
                }

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    requestUrl.ToUrlString()
                );

                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return await ConvertResponse<List<Course>>(response);
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

        public async Task<Course> UpdateCourse(Course originalCourseDetails, Course updateCourseDetails)
        {
            var updatedProperties = Course.GetUpdatedProperties(originalCourseDetails, updateCourseDetails);

            var request = new HttpRequestMessage(HttpMethod.Put,
                MakeRequestUrl()
                .AddPath(originalCourseDetails.ID)
                .ToUrlString()
                );

            request = SetJsonContent(request, updatedProperties);

            try
            {
                var response = await SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var updatedCourseDetails = originalCourseDetails;

                    updatedCourseDetails.UpdateProperties(updatedProperties);

                    return updatedCourseDetails;
                }
            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong with API request. {ex.Message}");
            }

            // Return the original course details if the update was unsuccessful
            return originalCourseDetails;
        }
    }
}
