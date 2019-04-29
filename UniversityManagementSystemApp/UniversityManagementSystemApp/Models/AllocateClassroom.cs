using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class AllocateClassroom
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public int DayId { get; set; }

        
        public int FromTime { get; set; }
        public int ToTime { get; set; }
    }
}