using Models;

namespace Services;

public class CourseDateService
{
    public CourseDate? CurrentCourseDate { get; private set; }

    public void SetCourseDate(CourseDate courseDate)
        => CurrentCourseDate = courseDate;

    public CourseDate? GetCourseDate()
        => CurrentCourseDate;
}
