using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;


namespace UniversityManagementSystemApp.Gateway
{
    public class CourseGateway : CommonGateway
    {
        public bool Save(Course course)
        {




            bool result = false;
            string query = @"INSERT INTO Course  Values('" + course.Code + "','" + course.Name + "'," + course.Credit +
                           ",'" + course.Description + "'," + course.DepartmentId + ",'" + course.SemesterId + "','unassigned','')";
            Command.CommandText = query;
            Connection.Open();
            result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result;

        }

        public Course GetByCourse(Course course)
        {
            Command.CommandText = @"SELECT * FROM Course where Code='" + course.Code + "' OR Name='" + course.Name + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Course c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Course();
                    c.Id = (int) reader["Id"];
                    c.Code = reader["Code"].ToString();
                    c.Name = reader["Name"].ToString();
                    c.Credit = Convert.ToDouble(reader["Credit"]);
                    c.Description = reader["Description"].ToString();
                    c.DepartmentId = (int) reader["DepartmentId"];
                    c.SemesterId = (int) reader["SemesterId"];
                }

            }
            reader.Close();
            Connection.Close();
            return c;
        }

        public Course GetCourseByCourseId(Course course)
        {
            Command.CommandText = @"SELECT * FROM Course where Id='" + course.Id + "' order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Course c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Course();
                    c.Id = (int) reader["Id"];
                    c.Code = reader["Code"].ToString();
                    c.Name = reader["Name"].ToString();
                    c.Credit = Convert.ToDouble(reader["Credit"]);
                    c.Description = reader["Description"].ToString();
                    c.DepartmentId = (int) reader["DepartmentId"];
                    c.SemesterId = (int) reader["SemesterId"];
                }

            }
            reader.Close();
            Connection.Close();
            return c;
        }

        public List<Course> GetAllCourse()
        {
            Command.CommandText = @"SELECT * FROM Course order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            Course c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Course();
                    c.Id = (int) reader["Id"];
                    c.Code = reader["Code"].ToString();
                    c.Name = reader["Name"].ToString();
                    c.Credit = Convert.ToDouble(reader["Credit"]);
                    c.Description = reader["Description"].ToString();
                    c.DepartmentId = (int) reader["DepartmentId"];
                    c.SemesterId = (int) reader["SemesterId"];
                    courses.Add(c);
                }

            }
            reader.Close();
            Connection.Close();
            return courses;
        }

        public CourseTeacherViewModel IsCourseAssigned(EnrollCourseTeacher enrollCourseTeacher)
        {
            Command.CommandText =
                @"select c.Id as CourseId,c.Name as CourseName, c.CourseAssignTo as TeacherName from Course c where c.Id='" +
                enrollCourseTeacher.CourseId + "' and c.Status='assigned'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            CourseTeacherViewModel ect = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ect = new CourseTeacherViewModel();
                    ect.CourseId = (int)reader["CourseId"];
                    ect.CourseName = reader["CourseName"].ToString();
                    ect.TeacherName = reader["TeacherName"].ToString();

                }

            }
            reader.Close();
            Connection.Close();
            return ect;
        }

        public List<CourseTeacherViewModel> GetAllCourses(CourseTeacherViewModel courseTeacherViewModel)
        {
            Command.CommandText =
                @"select c.Code as CourseCode,c.Name CourseName,s.Name as SemesterName,c.CourseAssignTo as CourseTeacher,c.Status as CourseStatus from Course c left join Department d on c.DepartmentId=d.Id left join Semester s on c.SemesterId=s.Id  where d.Id='" + courseTeacherViewModel.DepartmentId + "' order by c.Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<CourseTeacherViewModel> courseTeacherViewModels = new List<CourseTeacherViewModel>();
            CourseTeacherViewModel ect = null;
            //temp.Code = reader["Code"] != null ? reader["Code"].ToString() : "-1";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ect = new CourseTeacherViewModel();
                    ect.CourseCode = reader["CourseCode"].ToString();
                    ect.CourseName = reader["CourseName"].ToString();
                    ect.SemesterName = reader["SemesterName"].ToString();
                    ect.TeacherName = reader["CourseTeacher"].ToString();
                    ect.CourseStatus = reader["CourseStatus"].ToString();
                    courseTeacherViewModels.Add(ect);
                }

            }
            reader.Close();
            Connection.Close();
            return courseTeacherViewModels;
        }

        public List<Course> GetCourseByDepartmentId(Department department)
        {
            Command.CommandText = @"SELECT * FROM Course where DepartmentID='"+department.Id+"' order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            Course c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Course();
                    c.Id = (int)reader["Id"];
                    c.Code = reader["Code"].ToString();
                    c.Name = reader["Name"].ToString();
                    c.Credit = Convert.ToDouble(reader["Credit"]);
                    c.Description = reader["Description"].ToString();
                    c.DepartmentId = (int)reader["DepartmentId"];
                    c.SemesterId = (int)reader["SemesterId"];
                    courses.Add(c);
                }

            }
            reader.Close();
            Connection.Close();
            return courses;
        }

        public List<Course> GetCourseByStudentId(Student student)
        {
            Command.CommandText = @"select c.Id,c.Code,c.Name from Student s inner join Department d on s.DepartmentId=d.Id inner join Course c on d.Id=c.DepartmentId where s.Id='"+student.Id+"' order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            Course c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Course();
                    c.Id = (int)reader["Id"];
                    c.Code = reader["Code"].ToString();
                    c.Name = reader["Name"].ToString();
                    courses.Add(c);
                }

            }
            reader.Close();
            Connection.Close();
            return courses;
        }


        public bool UnassignAllCourses()
        {
            string query1 = @"Update EnrollCourseTeacher set Status='unassigned';";
            string query2 = @"Update Course set Status='unassigned',CourseAssignTo='';";
            Command.CommandText = query1;
            Connection.Open();
            bool result1 = Command.ExecuteNonQuery() > 0;
            Connection.Close();
            Command.CommandText = query2;
            Connection.Open();
            bool result2 = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result1 && result2; 
        }
    }
}