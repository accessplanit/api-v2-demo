using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserCourseDateWeb : UserCourseDate
    {
        public int CourseDateBookingID { get; set; }

        public string OnlinePlatformCandidateJoinUrl { get; set; }

        public string CourseDuration { get; set; }

        public string CourseOverview { get; set; }
    }
}
