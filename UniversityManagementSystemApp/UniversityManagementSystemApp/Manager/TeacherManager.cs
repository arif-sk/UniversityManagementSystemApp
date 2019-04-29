using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class TeacherManager
    {
        public string Message { get; set; }
        private TeacherGateway _teacherGateway = new TeacherGateway();
        public bool Save(Teacher teacher)
        {
            return _teacherGateway.Save(teacher);
        }

        public void ValidateTeacher(Teacher teacher)
        {
            if (string.IsNullOrEmpty(teacher.Name)&&
                string.IsNullOrEmpty(teacher.Address)&&
                string.IsNullOrEmpty(teacher.Email)&&
                string.IsNullOrEmpty(teacher.ContactNo)&&
                teacher.DesignationId==0&&
                teacher.DepartmentId==0&&
                teacher.Credit<0)
            {
                Message = "Invalid Input!";
            }
        }

        public Teacher GetByTeacher(Teacher teacher)
        {
            return _teacherGateway.GetByTeacher(teacher);
        }

        public void IsTeacherAvailable(Teacher teacher)
        {
            Teacher t =GetByTeacher(teacher);
            if (t!=null)
            {
                Message = "Email must be unique!";
            }
        }

        public List<Teacher> GetTeacherByDepartmentId(Department department)
        {
            return _teacherGateway.GetTeacherByDepartmentId(department);
        }

        public Teacher GetTeacherById(Teacher teacher)
        {
            return _teacherGateway.GetTeacherById(teacher);
        }

        public double GetReaminCredit(Teacher teacher)
        {
            return _teacherGateway.GetReaminCredit(teacher);
        }

        public bool EnrollCourseAndTeacher(EnrollCourseTeacher enrollCourseTeacher)
        {
            return _teacherGateway.EnrollCourseAndTeacher(enrollCourseTeacher);
        }
    }
}