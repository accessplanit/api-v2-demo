using CommercialSite.WebApp.App.Modals;
using Constants;
using Models;
using Services.Models;

namespace CommercialSite.WebApp.App
{
    public partial class Index
    {

        private readonly List<string> InvoiceDefaultColumns = new(["id", "datecreated", "reference", "companyname", "targetidtype", "targetlabel", "targetid", "amount", "currencycode", "datedue", "status", "amountpaid", "outstanding", "ponumber", "address", "invoiceaddress", "invoicepostcode", "invoicecountry", "invoicecounty", "invoiceemail", "invoicetown", "taxrate"]);
        private readonly List<string> InvoiceItemDefaultColumns = new(["id", "invoiceid", "amount", "dynamiclabel", "reference", "tax", "sourceid", "sourceidtype"]);
        private readonly List<string> UsersElearningPackagesDefaultColumns = new(["id", "coursename", "userid", "datestamp", "datefirstviewed", "completionstatus", "successstatus", "candidatedirectelearningurl"]);
        private readonly List<string> WebUserCourseDateColumns = new(["id", "userid", "datestamp", "coursedateid", "userfullname", "companyname", "CourseOrAliasName", "startdate", "enddate", "status", "CourseDateBookingID", "OnlinePlatformCandidateJoinUrl", "CourseOverview"]);
        private readonly List<string> UserCourseDateColumns = new(["id", "userid", "datestamp", "coursedateid", "userfullname", "companyname", "CourseOrAliasName", "startdate", "enddate", "status", "CourseDateBookingID"]);

        IQueryable<UserCourseDate>? courses;
        IQueryable<CourseDateBooking>? bookings;
        IQueryable<Invoice>? Invoices;
        IQueryable<UsersELearningPackage>? UsersElearningPackages;
        IQueryable<UserCourseDateWeb>? WebCourses;

        private InvoiceModal invoiceModal { get; set; }
        private Invoice selectedInvoice { get; set; }

        private BookingModal bookingModal { get; set; }
        private WebCourseModal webCourseModal { get; set; }

        private ElearningModal elearningModal { get; set; }
        public bool IsLoggedIn { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Initial state setup
                IsLoggedIn = StateManagementService.IsLoggedIn;

                // Subscribe to state change notifications
                StateManagementService.OnChange += StateHasChangedSafely;

                if(IsLoggedIn)
                {
                    var userCourseDates = await AccessPlanitUserCourseDateClient.GetByCriteria<UserCourseDate>([new Filter("ID", FilterExpressions.Contains, StateManagementService.LoggedInUser.ID)],UserCourseDateColumns);
                    courses = userCourseDates?.AsQueryable();

                    var courseDateBookings = await AccessPlanitCourseDateBookingClient.GetByCriteria<CourseDateBooking>([new Filter("UserID", FilterExpressions.Contains, StateManagementService.LoggedInUser.ID)]);
                    bookings = courseDateBookings?.AsQueryable();

                    var webUserCourseDates = await AccessPlanitUserCourseDateWebClient.GetByCriteria<UserCourseDateWeb>([new Filter("ID", FilterExpressions.Contains, StateManagementService.LoggedInUser.ID)], WebUserCourseDateColumns);
                    WebCourses = webUserCourseDates?.AsQueryable();

                    await SetupInvoicesForUser(StateManagementService.LoggedInUser.ID);

                    await SetupElearningCourseDatesForUser(StateManagementService.LoggedInUser.ID);
                }

            }
            catch (Exception ex)
            {
                StateManagementService.SetLatestErrorMessage($"Something went wrong: {ex.Message}");
                throw;
            }
        }

        // This method ensures the state is updated in a thread-safe way
        private void StateHasChangedSafely()
        {
            IsLoggedIn = StateManagementService.IsLoggedIn;
            InvokeAsync(() => StateHasChanged());
        }

        private async Task SetupInvoicesForUser(string userID)
        {
            var invoices = await AccessPlanitInvoiceClient.GetByCriteria<Invoice>([new Filter("targetid", FilterExpressions.Equals, userID)], InvoiceDefaultColumns);

            foreach (var invoice in invoices)
            {
                var invoiceItems = await AccessPlanitInvoiceItemClient.GetByCriteria<InvoiceItem>([new Filter("invoiceid", FilterExpressions.Equals, invoice.ID)], InvoiceItemDefaultColumns);
                invoice.Items = invoiceItems;
            }

            Invoices = invoices.AsQueryable();
        }

        private async Task SetupElearningCourseDatesForUser(string userID)
        {
            var usersElearningPackages = await AccessPlanitUsersElearningPackageClient.GetByCriteria<UsersELearningPackage>([new Filter("userid", FilterExpressions.Equals, userID)], UsersElearningPackagesDefaultColumns);

            UsersElearningPackages = usersElearningPackages.AsQueryable();
        }

        #region Open Modal Methods
        private async Task GetAndViewBooking(int bookingID)
        {
            var Bookings = await AccessPlanitCourseDateBookingClient.GetByCriteria<CourseDateBooking>([new Filter("id", FilterExpressions.Equals, bookingID.ToString())]);

            if (bookings.Any())
                ViewBooking(bookings.FirstOrDefault());
        }

        private async Task GetAndViewInvoice(string invoiceID)
        {
            var invoices = await AccessPlanitInvoiceClient.GetByCriteria<Invoice>([new Filter("invoiceid", FilterExpressions.Equals, invoiceID)], InvoiceDefaultColumns);

            if (invoices.Any())
            {
                var invoice = invoices.FirstOrDefault();
                var invoiceItems = await AccessPlanitInvoiceItemClient.GetByCriteria<InvoiceItem>([new Filter("invoiceid", FilterExpressions.Equals, invoice.ID)], InvoiceItemDefaultColumns);
                invoice.Items = invoiceItems;

                ViewInvoice(invoice);
            }
        }

        private async Task ViewInvoice(Invoice invoice)
        {
            InvoiceService.SetInvoice(invoice);
            invoiceModal.Open();
        }

        private async Task ViewWebCourse(UserCourseDateWeb course)
        {
            webCourseModal.Open(course);
        }

        private async Task ViewBooking(CourseDateBooking booking)
        {
            bookingModal.Open(booking);
        }

        private async Task ViewUsersELearningPackage(string packageUrl)
        {
            elearningModal.Open(packageUrl);
        }

        #endregion
    }
}
