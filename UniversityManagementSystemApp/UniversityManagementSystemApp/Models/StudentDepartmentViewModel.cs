using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class StudentDepartmentViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string RegistrationNo { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}