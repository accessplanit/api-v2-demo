using Constants;
using Models.Interfaces;

namespace Models
{
	public class Course : AccessPlanitModelBaseClass, IAccessPlanitModel
	{
		public APIModule Module { get; set; } = APIModule.Course;
        public bool? Advertise { get; set; }
		public bool? AutoComplete { get; set; }
		public List<string>? AwardedAwardIDs { get; set; }
		public List<string>? AwardedAwardLabels { get; set; }
		public List<string>? CategoryIDs { get; set; }
		public List<string>? CategoryLabels { get; set; }
		public double? CertificateNumber { get; set; }
		public string? CertificatePrefix { get; set; }
		public bool? CheckSecurityOnUpdateAndDelete { get; set; }
		public string? Colour { get; set; }
		public List<string>? ConfigCodeCodes { get; set; }
		public double? Cost { get; set; }
		public string? CostType { get; set; } // Enum: "Unknown", "Candidate", "Session", "Hour", "Day", "CandidateDay"
		public string? CourseType { get; set; } // Enum: "Unknown", "Class", "ELearning", etc.
		public bool? Coursework { get; set; }
		public string? CurrencyCode { get; set; }
		public string? CurrentConfigCode { get; set; }
		public bool? CurrentLoggedInUserIsDeveloper { get; set; }
		public bool? CurrentLoggedInUserHasDeveloperActionPermission { get; set; }
        public string? DateCreated { get; set; }
		public string? DateUpdated { get; set; }
		public string? Description { get; set; }
		public double? Duration { get; set; }
		public string? DurationAsString { get; set; }
		public string? DurationType { get; set; } // Enum: "Day", "Hour", "Minute"
		public string? ExternalID { get; set; }
		public string? Fundable { get; set; }
		public string ID { get; set; }
		public List<string>? IndustryIDs { get; set; }
		public List<string>? IndustryLabels { get; set; }
		public bool? IsModule { get; set; }
		public string? JoiningInstructionsText { get; set; }
		public string? Label { get; set; }
		public string? LastUpdatedBy { get; set; }
		public string? MinimumPassScore { get; set; }
		public string? Overview { get; set; }
		public List<string>? PreferredAwardIDs { get; set; }
		public List<string>? PreferredAwardLabels { get; set; }
		public List<string>? ProgressionCourseIDs { get; set; }
		public List<string>? ProgressionCourseNames { get; set; }
		public List<string>? ProviderCompaniesIDs { get; set; }
		public List<string>? RequiredAwardIDs { get; set; }
		public List<string>? RequiredAwardLabels { get; set; }
		public string? SageDepartmentCode { get; set; }
		public string? SageNominalCode { get; set; }
		public List<string>? SessionCourseIDs { get; set; }
		public List<string>? SessionCourseNames { get; set; }
		public bool? SetCompletedIfPassed { get; set; }
		public string? Summary { get; set; }
		public double? TrainingPoints { get; set; }
		public string? TrainingPointsTypeID { get; set; }
		public string? TrainingPointsTypeLabel { get; set; }
		public bool? Unit { get; set; }
		public List<string>? UnitAwardIDs { get; set; }
		public List<string>? UnitAwardLabels { get; set; }
		public int? UnitsRequired { get; set; }
		public List<string>? UserTypeIDs { get; set; }
		public List<string>? UserTypeLabels { get; set; }
		public bool? UsesLogDataSource { get; set; }
		public string? WaitingList { get; set; } // Enum: "Default", "Enabled", "Disabled"
    }
}
