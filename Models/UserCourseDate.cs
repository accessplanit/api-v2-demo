using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserCourseDate
    {
		public string ID { get; set; }

		public string UserId { get { return ID.Split('~').First(); } }

        public string CourseDateId { get { return ID.Split('~').Last(); } }

        public string UserFullName { get; set; }

        public string CompanyName { get; set; }

        public string CourseOrAliasName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime DateStamp { get; set; }

        public string Status { get; set; }
    }
}
