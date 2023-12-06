namespace SchoolManagementSystem.Domain.Models
{
    public class EnrollmentModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public StudentModel Student { get; set; }
        public CourseModel Course { get; set; }
    }
}