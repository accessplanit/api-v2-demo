using Constants;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : AccessPlanitModelBaseClass, IAccessPlanitModel
    {
        public APIModule Module { get; set; } = APIModule.User;
        public int ActiveCoursesCount { get; set; }
        public int ActiveInvoicesCount { get; set; }
        public int ActiveTasksCount { get; set; }
        public string Address { get; set; }
        public string AgentID { get; set; }
        public string AlternativeAddress { get; set; }
        public List<string> AlternativeCountry { get; set; }
        public string AlternativeCounty { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 characters.")]
        public string AlternativePhone { get; set; }
        public string AlternativePostcode { get; set; }
        public string AlternativeTown { get; set; }
        public int AwardsCount { get; set; }
        public string BillingAddress { get; set; }
        public List<string> BillingCountry { get; set; }
        public string BillingCounty { get; set; }
        public string BillingEmail { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 characters.")]
        public string BillingPhone { get; set; }
        public string BillingPostcode { get; set; }
        public string BillingTown { get; set; }
        public bool CheckSecurityOnUpdateAndDelete { get; set; }
        public List<string> ContactMethod { get; set; }
        public string CostCentre { get; set; }
        public List<string> Country { get; set; }
        public string County { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public List<string> CurrencyID { get; set; }
        public string CurrencyLabel { get; set; }
        public string CurrentConfigCode { get; set; }
        public bool DailyTaskSummaryOptOut { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DOB { get; set; }
        public List<string> EditPageMainRoleID { get; set; }
        public string Email { get; set; }
        public string EthnicOriginID { get; set; }
        public string EthnicOriginLabel { get; set; }
        public string ExternalID { get; set; }
        public string Fax { get; set; }
        public string Forenames { get; set; }
        public string FullAddress { get; set; }
        public string Fullname { get; set; }
        public bool HasEmail { get; set; }
        public string ID { get; set; }
        public string JobTitle { get; set; }
        public DateTime LastBooking { get; set; }
        public DateTime LastLoggedOn { get; set; }
        public DateTime LatestCourseDateAttended { get; set; }
        public DateTime LatestCourseDateBooked { get; set; }
        public string MainCompanyCompanyGroupIDs { get; set; }
        public string MainCompanyGroupID { get; set; }
        public string MainCompanyID { get; set; }
        public string MainCompanyName { get; set; }
        public List<string> MainRoleID { get; set; }
        public bool MainRoleIsDefault { get; set; }
        public string MainRoleLabel { get; set; }
        public DateTime MainUserCompanyEndDate { get; set; }
        public DateTime MainUserCompanyStartDate { get; set; }
        public string MainUserTypeLabel { get; set; }
        public string ManagedCompaniesIDs { get; set; }
        public string ManagedCompaniesLabels { get; set; }
        public string ManagedUsersIDs { get; set; }
        public string ManagedUsersNames { get; set; }
        public string ManagerUsersIDs { get; set; }
        public string ManagerUsersNames { get; set; }
        public string MaritalStatus { get; set; }
        public bool MarketingOptIn { get; set; }
        public string MiddleNames { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 characters.")]
        public string Mobile { get; set; }
        public int MoodleID { get; set; }
        public string MyDirectManagersEmailAddresses { get; set; }
        public string MyEmployerEmailAddresses { get; set; }
        public string MyManagersEmailAddresses { get; set; }
        public string NationalityID { get; set; }
        public string NationalityLabel { get; set; }
        public string NINumber { get; set; }
        public string NINumberNoValidation { get; set; }
        public int NotesCount { get; set; }
        public int OpportunitiesCount { get; set; }
        public DateTime PasswordExpires { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 characters.")]
        public string Phone { get; set; }
        public string Postcode { get; set; }
        public List<string> PreferredLanguageCode { get; set; }
        public string PreferredLanguageName { get; set; }
        public string PreviousMainRoleID { get; set; }
        public bool RestrictBooking { get; set; }
        public string SageTaxCode { get; set; }
        public string Sex { get; set; }
        public string ShippingAddress { get; set; }
        public List<string> ShippingCountry { get; set; }
        public string ShippingCounty { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number must be between 10 and 15 characters.")]
        public string ShippingPhone { get; set; }
        public string ShippingPostcode { get; set; }
        public string ShippingTown { get; set; }
        public bool SmsAuthorised { get; set; }
        public string SpecialNeedNames { get; set; }
        public string Status { get; set; }
        public string Surname { get; set; }
        public string Tags { get; set; }
        public bool TaxExempt { get; set; }
        public List<string> Title { get; set; }
        public string Town { get; set; }
        public Guid UniqueID { get; set; }
        public string UpdatedByID { get; set; }
        public string UpdatedByName { get; set; }
        public string UserCompaniesIDs { get; set; }
        public bool UsesLogDataSource { get; set; }

        public User() { }

        public User(User other) { 
            if(other != null)
            {
                ActiveCoursesCount = other.ActiveCoursesCount;
                ActiveInvoicesCount = other.ActiveInvoicesCount;
                ActiveTasksCount = other.ActiveTasksCount;
                Address = other.Address;
                AgentID = other.AgentID;
                AlternativeAddress = other.AlternativeAddress;
                AlternativeCountry = other.AlternativeCountry;
                AlternativeCounty = other.AlternativeCounty;
                AlternativePhone = other.AlternativePhone;
                AlternativePostcode = other.AlternativePostcode;
                AlternativeTown = other.AlternativeTown;
                AwardsCount = other.AwardsCount;
                BillingAddress = other.BillingAddress;
                BillingCountry = other.BillingCountry;
                BillingCounty = other.BillingCounty;
                BillingEmail = other.BillingEmail;
                BillingPhone = other.BillingPhone;
                BillingPostcode = other.BillingPostcode;
                BillingTown = other.BillingTown;
                CheckSecurityOnUpdateAndDelete = other.CheckSecurityOnUpdateAndDelete;
                ContactMethod = other.ContactMethod;
                CostCentre = other.CostCentre;
                Country = other.Country;
                County = other.County;
                CreatedByID = other.CreatedByID;
                CreatedByName = other.CreatedByName;
                CurrencyID = other.CurrencyID;
                CurrencyLabel = other.CurrencyLabel;
                CurrentConfigCode = other.CurrentConfigCode;
                DailyTaskSummaryOptOut = other.DailyTaskSummaryOptOut;
                DateCreated = other.DateCreated;
                DateUpdated = other.DateUpdated;
                DOB = other.DOB;
                EditPageMainRoleID = other.EditPageMainRoleID;
                Email = other.Email;
                EthnicOriginID = other.EthnicOriginID;
                EthnicOriginLabel = other.EthnicOriginLabel;
                ExternalID = other.ExternalID;
                Fax = other.Fax;
                Forenames = other.Forenames;
                FullAddress = other.FullAddress;
                Fullname = other.Fullname;
                HasEmail = other.HasEmail;
                ID = other.ID;
                JobTitle = other.JobTitle;
                LastBooking = other.LastBooking;
                LastLoggedOn = other.LastLoggedOn;
                LatestCourseDateAttended = other.LatestCourseDateAttended;
                LatestCourseDateBooked = other.LatestCourseDateBooked;
                MainCompanyCompanyGroupIDs = other.MainCompanyCompanyGroupIDs;
                MainCompanyGroupID = other.MainCompanyGroupID;
                MainCompanyID = other.MainCompanyID;
                MainCompanyName = other.MainCompanyName;
                MainRoleID = other.MainRoleID;
                MainRoleIsDefault = other.MainRoleIsDefault;
                MainRoleLabel = other.MainRoleLabel;
                MainUserCompanyEndDate = other.MainUserCompanyEndDate;
                MainUserCompanyStartDate = other.MainUserCompanyStartDate;
                MainUserTypeLabel = other.MainUserTypeLabel;
                ManagedCompaniesIDs = other.ManagedCompaniesIDs;
                ManagedCompaniesLabels = other.ManagedCompaniesLabels;
                ManagedUsersIDs = other.ManagedUsersIDs;
                ManagedUsersNames = other.ManagedUsersNames;
                ManagerUsersIDs = other.ManagerUsersIDs;
                ManagerUsersNames = other.ManagerUsersNames;
                MaritalStatus = other.MaritalStatus;
                MarketingOptIn = other.MarketingOptIn;
                MiddleNames = other.MiddleNames;
                Mobile = other.Mobile;
                MoodleID = other.MoodleID;
                MyDirectManagersEmailAddresses = other.MyDirectManagersEmailAddresses;
                MyEmployerEmailAddresses = other.MyEmployerEmailAddresses;
                MyManagersEmailAddresses = other.MyManagersEmailAddresses;
                NationalityID = other.NationalityID;
                NationalityLabel = other.NationalityLabel;
                NINumber = other.NINumber;
                NINumberNoValidation = other.NINumberNoValidation;
                NotesCount = other.NotesCount;
                OpportunitiesCount = other.OpportunitiesCount;
                PasswordExpires = other.PasswordExpires;
                Phone = other.Phone;
                Postcode = other.Postcode;
                PreferredLanguageCode = other.PreferredLanguageCode;
                PreferredLanguageName = other.PreferredLanguageName;
                PreviousMainRoleID = other.PreviousMainRoleID;
                RestrictBooking = other.RestrictBooking;
                SageTaxCode = other.SageTaxCode;
                Sex = other.Sex;
                ShippingAddress = other.ShippingAddress;
                ShippingCountry = other.ShippingCountry;
                ShippingCounty = other.ShippingCounty;
                ShippingPhone = other.ShippingPhone;
                ShippingPostcode = other.ShippingPostcode;
                ShippingTown = other.ShippingTown;
                SpecialNeedNames = other.SpecialNeedNames;
                Status = other.Status;
                Surname = other.Surname;
                Tags = other.Tags;
                TaxExempt = other.TaxExempt;
                Title = other.Title;
                Town = other.Town;
                UniqueID = other.UniqueID;
                UpdatedByID = other.UpdatedByID;
                UpdatedByName = other.UpdatedByName;
                UserCompaniesIDs = other.UserCompaniesIDs;
                UsesLogDataSource = other.UsesLogDataSource;
            }
        }
	}
}

