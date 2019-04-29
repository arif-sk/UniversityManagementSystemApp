namespace UniversityManagementSystemApp.Models
{
    public class CourseTeacherViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string SemesterName { get; set; }
        public int DepartmentId { get; set; }
        public string Status { get; set; }
        public string CourseStatus { get; set; }
    }
}