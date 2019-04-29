using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class StudentEnrollCourses
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
       // public string DepartmentName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}