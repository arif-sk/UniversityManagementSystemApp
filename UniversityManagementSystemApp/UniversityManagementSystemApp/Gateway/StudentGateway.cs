using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class StudentGateway:CommonGateway
    {
        public int GetNumberOfStudent(Student student)
        {
            Command.CommandText = @"SELECT * FROM Student where DepartmentId='"+student.DepartmentId+"' and year(CreatedAT)='"+student.CreatedAt.Year+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Student s = null;
            int i = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    s=new Student()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Address = reader["Address"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],
                        
                    };
                    i = i + 1;
                }

            }
            reader.Close();
            Connection.Close();
            return i;
        }

        public Student IsEmailAvailable(Student student)
        {
            Command.CommandText = @"SELECT * FROM Student where Email='" + student.Email + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Student s = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    s = new Student()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Address = reader["Address"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],

                    };
                }

            }
            reader.Close();
            Connection.Close();
            return s;
        }

        public bool Save(Student student)
        {
            string query = @"INSERT INTO Student Values('" + student.Name + "','" + student.Email + "','" + student.RegistrationNo + "','"+student.ContactNo+"','"+student.Address+"','" + student.CreatedAt + "'," + student.DepartmentId + ")";
            Command.CommandText = query;
            Connection.Open();
            bool result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result;
        }

        public List<Student> GetAllStudent()
        {
            Command.CommandText = @"SELECT * FROM Student order by RegistrationNo";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Student> students=new List<Student>();
            Student s = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    s = new Student()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        RegistrationNo = reader["RegistrationNo"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        Address = reader["Address"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],
                        

                    };
                    students.Add(s);
                }

            }
            reader.Close();
            Connection.Close();
            return students;
        }


        public StudentDepartmentViewModel GetStudentById(Student student)
        {
            Command.CommandText = @"select s.Id as StudentId,s.Name as StudentName,s.Email as StudentEmail,s.DepartmentId as DepartmentId,s.RegistrationNo,d.Name as DepartmentName from Student s inner join Department d on s.DepartmentId=d.Id where s.Id='" + student.Id + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            StudentDepartmentViewModel sdv = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sdv=new StudentDepartmentViewModel()
                    {
                        StudentId = (int)reader["StudentId"],
                        StudentName = reader["StudentName"].ToString(),
                        
                        StudentEmail = reader["StudentEmail"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],
                        DepartmentName = reader["DepartmentName"].ToString(),
                        RegistrationNo = reader["RegistrationNo"].ToString()
                    };
                    
                    
                }

            }
            reader.Close();
            Connection.Close();
            return sdv;
        }

        public StudentEnrollCourses GetStudentByEnrollCourseId(StudentEnrollCourses studentEnrollCourses)
        {

            Command.CommandText = @"Select * from StudentEnroleCourse where StudentId='"+studentEnrollCourses.StudentId+"' and CourseId='"+studentEnrollCourses.CourseId+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            
            StudentEnrollCourses sec= null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sec=new StudentEnrollCourses()
                    {
                        Id = (int)reader["Id"],
                        StudentId = (int)reader["StudentId"],
                        CourseId = (int)reader["CourseId"],
                        DepartmentId = (int)reader["DepartmentId"]
                    };
                    
                }
            }
        
            reader.Close();
            Connection.Close();
            return sec;
        }

        public bool SaveStudentResult(StudentResult studentResult)
        {
            string query = @"INSERT INTO StudentResult Values('" + studentResult.StudentId + "','" + studentResult.DepartmentId + "','" + studentResult.CourseId + "','" + studentResult.GradeId + "')";
            Command.CommandText = query;
            Connection.Open();
            bool result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result;
        }

        public List<StudentResultViewModel> GetStudentResult(StudentResult studentResult)
        {
            Command.CommandText = @"select c.Code as CourseCode,c.Name as CourseName,ISNULL(g.Name,'Not Graded Yet') as GradeName from StudentEnroleCourse sec left join Course c on sec.CourseId=c.Id left join StudentResult sr on sr.CourseId=c.Id left join Grade g on sr.GradeId=g.Id where sec.StudentId='"+studentResult.StudentId+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<StudentResultViewModel> studentResultViewModels=new List<StudentResultViewModel>();
            StudentResultViewModel srv = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    srv=new StudentResultViewModel()
                    {
                        CourseCode = reader["CourseCode"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        GradeName = reader["GradeName"].ToString(),
                    };
                    studentResultViewModels.Add(srv);

                }
            }

            reader.Close();
            Connection.Close();
            return studentResultViewModels;
        }

        public StudentDepartmentViewModel GetStudentInfoById(StudentResult studentResult)
        {
            Command.CommandText = @"select s.Id as StudentId,s.Name as StudentName,s.Email as StudentEmail,s.DepartmentId as DepartmentId,s.RegistrationNo,d.Name as DepartmentName from Student s inner join Department d on s.DepartmentId=d.Id where s.Id='" + studentResult.StudentId + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            StudentDepartmentViewModel sdv = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sdv = new StudentDepartmentViewModel()
                    {
                        StudentId = (int)reader["StudentId"],
                        StudentName = reader["StudentName"].ToString(),

                        StudentEmail = reader["StudentEmail"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],
                        DepartmentName = reader["DepartmentName"].ToString(),
                        RegistrationNo = reader["RegistrationNo"].ToString()
                    };


                }

            }
            reader.Close();
            Connection.Close();
            return sdv;
        }
    }
}