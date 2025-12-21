using Constants;
using Models.Interfaces;

namespace Models
{
	public class CourseDateBooking : AccessPlanitModelBaseClass, IAccessPlanitModel
	{
		public APIModule Module { get; set; } = APIModule.CourseDateBooking;
		public string ID { get; set; }
		public string? DateCreated { get; set; }
		public string? BookingType { get; set; }
		public string? BookingUserName { get; set; }
		public string? BookingStatus { get; set; }
		public string? PONumber { get; set; }
		public string? AgentName { get; set; }
		public string? UserID { get; set; }

		public string[] InvoiceIDs { get; set; }

		public string? BookingSummary { get; set; }
	}
}
