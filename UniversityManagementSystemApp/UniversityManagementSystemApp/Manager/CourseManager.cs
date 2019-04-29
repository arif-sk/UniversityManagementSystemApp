using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class CourseManager
    {
        public string Message { get; set; }
        private CourseGateway _courseGateway=new CourseGateway();
        public bool Save(Course course)
        {
           return _courseGateway.Save(course);
        }

        public void ValidateCourse(Course course)
        {
            if (course.Code.Length<5)
            {
                Message = "Code must be at least five (5) characters long!";
            }else if (course.Credit<0.5||course.Credit>5)
            {
                Message = "Credit cannot be less than 0.5 and more than 5.0!";
            }
        }
        public void IsCourseAvailable(Course course)
        {
            Course cou = GetByCourse(course);
            if (cou != null)
            {
                Message = "Code and Name must be unique.";
            }
        }

        public Course GetByCourse(Course course)
        {
            return _courseGateway.GetByCourse(course);
        }

        public List<Course> GetCourseByDepartmentId(Department department)
        {
            return _courseGateway.GetCourseByDepartmentId(department);
        }

        public Course GetCourseByCourseId(Course course)
        {
            return _courseGateway.GetCourseByCourseId(course);
        }

        public CourseTeacherViewModel IsCourseAssigned(EnrollCourseTeacher enrollCourseTeacher)
        {
            return _courseGateway.IsCourseAssigned(enrollCourseTeacher);
        }

        public List<CourseTeacherViewModel> GetAllCourses(CourseTeacherViewModel courseTeacherViewModel)
        {
            return _courseGateway.GetAllCourses(courseTeacherViewModel);
           
            
        }

        public List<Course> GetCourseByStudentId(Student student)
        {
            List<Course> courses = _courseGateway.GetCourseByStudentId(student);
            return courses;
        }


        public bool UnassignAllCourses()
        {
            return _courseGateway.UnassignAllCourses();
        }
    }
}