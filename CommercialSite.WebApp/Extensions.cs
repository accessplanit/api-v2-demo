using Microsoft.AspNetCore.Mvc.Routing;
using static Constants.AccessPlanitAPIConfig;

namespace CommercialSite.WebApp
{
    public static class Extensions
    {
        public static IHostApplicationBuilder RegisterAccessPlanitAPIModuleClients(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<AccessPlanitTokenClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitUserClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitCourseClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitCourseDateClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitLoginClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitUserCourseDateClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitUserCourseDateWebClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitInvoiceClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitInvoiceItemClient>
                (config => config.BaseAddress = new Uri(BaseAddress));

			builder.Services.AddHttpClient<AccessPlanitCourseDateBookingClient>
				(config => config.BaseAddress = new Uri(BaseAddress));

            builder.Services.AddHttpClient<AccessPlanitUsersElearningPackageClient>
				(config => config.BaseAddress = new Uri(BaseAddress));

			return builder;
        }
    }
}
