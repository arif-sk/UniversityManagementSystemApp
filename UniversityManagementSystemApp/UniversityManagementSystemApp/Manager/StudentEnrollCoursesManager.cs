using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class StudentEnrollCoursesManager
    {
        private StudentEnrollCoursesGateway _studentEnrollCoursesGateway = new StudentEnrollCoursesGateway();
        public bool Save(StudentEnrollCourses studentEnrollCourses)
        {
            return _studentEnrollCoursesGateway.Save(studentEnrollCourses);
        }

        public List<Course> GetEnrolledCourseByStudentId(Student student)
        {
            return _studentEnrollCoursesGateway.GetEnrolledCourseByStudentId(student);
        }
    }
}