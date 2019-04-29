using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class StudentEnrollCoursesGateway : CommonGateway
    {
        public bool Save(StudentEnrollCourses studentEnrollCourses)
        {

            bool result = false;

            string query = @"INSERT INTO StudentEnroleCourse(StudentId,CourseId,DepartmentId,CreatedDate) Values (" + studentEnrollCourses.StudentId + "," + studentEnrollCourses.CourseId + "," + studentEnrollCourses.DepartmentId + ",'" + studentEnrollCourses.CreatedDateTime + "')";
          
            Command.CommandText = query;

            Connection.Open();
            result = Command.ExecuteNonQuery() > 0;
            Connection.Close();


            return result;
        }


        public List<Course> GetEnrolledCourseByStudentId(Student student)
        {
            Command.CommandText = @"select c.Id,c.Name from Student s inner join StudentEnroleCourse sec on s.Id=sec.StudentId inner join Course c on sec.CourseId=c.Id where s.Id='"+student.Id+"' order by c.Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Id = (int)reader["Id"];
                course.Name = reader["Name"].ToString();

                courses.Add(course);
            }

            reader.Close();
            Connection.Close();
            return courses; 
        }
    }
}