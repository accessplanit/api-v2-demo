using Constants;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CourseDate : AccessPlanitModelBaseClass, IAccessPlanitModel
    {
        public string ID { get; set; }

        public string CourseID { get; set; }

        public string CompanyName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string CourseType { get; set; }

        public string Status { get; set; }

        public decimal Cost { get; set; }

        public string DisplayCost => Cost.ToString("C");

        public string VenueLabel { get; set; }

        public string VenueTown { get; set; }

        public double DurationInHours { get; set; }

        public int Places { get; set; }

        public int SpacesLeft { get; set; }

        public APIModule Module { get; set; } = APIModule.CourseDate;
    }
}
