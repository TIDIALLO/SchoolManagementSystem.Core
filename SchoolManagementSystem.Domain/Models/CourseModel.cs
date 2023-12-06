namespace SchoolManagementSystem.Domain.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }

        public TeacherModel Teacher { get; set; }
        public IList<EnrollmentModel> Enrollments { get; set; }
    }
}