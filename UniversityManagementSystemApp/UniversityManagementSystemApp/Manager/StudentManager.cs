using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class StudentManager
    {
        
        private StudentGateway _studentGateway=new StudentGateway();
        public string Message { get; set; }
        public int GetNumberOfStudent(Student student)
        {
            return _studentGateway.GetNumberOfStudent(student);
        }

        public string CreateRegNo(Student student,Department department)
        {
            string registrationNo = department.Code + "-" + student.CreatedAt.Year + "-";
            int count = GetNumberOfStudent(student) + 1;
            string r = count.ToString();
            string addZero = "";

            int len = 3 - r.Length;
            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }
            registrationNo += addZero + r;
            return registrationNo;
        }

        public void ValidateEmailAddress(Student student)
        {
            IsEmailValid(student);
            IsEmailAvailable(student);
            IsContactNumberValid(student);
        }

        private void IsEmailAvailable(Student student)
        {
            Student s = _studentGateway.IsEmailAvailable(student);
            if (s!=null)
            {
                Message = "Email Address is already registered!";
            }
        }

        private void IsEmailValid(Student student)
        {
            string email = student.Email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                Message = null;
                
            }
            else
            {
                Message = "Please Provide a valid Email Address!";
            }
            
        }

        private void IsContactNumberValid(Student student)
        {
            if (!Regex.Match(student.ContactNo, @"^(?:\+88|01)?(?:\d{11}|\d{13})$").Success)
            {
                Message = "Please Provide a valid Contact Number!";
            }
            
        }

        public bool Save(Student student)
        {
            return _studentGateway.Save(student);
        }

        public List<Student> GetAllStudent()
        {
            return _studentGateway.GetAllStudent();
        }

        public StudentDepartmentViewModel GetStudentById(Student student)
        {
            return _studentGateway.GetStudentById(student);
        }

        public void ValidateStudent(StudentEnrollCourses studentEnrollCourses)
        {
            StudentEnrollCourses st = IsStudentEnrolledTheCourseBefore(studentEnrollCourses);
            if (st!=null)
            {
                Message = "Student Already Enrolled this Course";
            }
        }

        private StudentEnrollCourses IsStudentEnrolledTheCourseBefore(StudentEnrollCourses studentEnrollCourses)
        {
            return _studentGateway.GetStudentByEnrollCourseId(studentEnrollCourses);
        }

        public bool SaveStudentResult(StudentResult studentResult)
        {
            return  _studentGateway.SaveStudentResult(studentResult);
        }

        public List<StudentResultViewModel> GetStudentResult(StudentResult studentResult)
        {
            return _studentGateway.GetStudentResult(studentResult);
        }

        public StudentDepartmentViewModel GetStudentInfoById(StudentResult studentResult)
        {
            return _studentGateway.GetStudentInfoById(studentResult);
        }
    }
}