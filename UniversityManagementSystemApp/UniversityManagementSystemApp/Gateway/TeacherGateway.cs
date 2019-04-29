using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class TeacherGateway : CommonGateway
    {
        public bool Save(Teacher teacher)
        {

            bool result = false;

            string query = "INSERT INTO Teacher Values('" + teacher.Name + "','" + teacher.Address + "','" + teacher.Email + "','" + teacher.ContactNo + "'," + teacher.DesignationId + "," + teacher.DepartmentId + "," + teacher.Credit + ")";
            Command.CommandText = query;
            Connection.Open();
            result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result;
        }

        public Teacher GetByTeacher(Teacher teacher)
        {
            Command.CommandText = @"SELECT * FROM Teacher where Email='" + teacher.Email + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Teacher t = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    t = new Teacher();
                    t.Id = (int)reader["Id"];
                    t.Name = reader["Name"].ToString();
                    t.Address = reader["Address"].ToString();
                    t.Email = reader["Email"].ToString();
                    t.ContactNo = reader["ContactNo"].ToString();
                    t.DesignationId =(int)reader["DesignationId"];
                    t.DepartmentId = (int)reader["DepartmentId"];
                    t.Credit = Convert.ToDouble(reader["Credit"]);
                }

            }
            reader.Close();
            Connection.Close();
            return t;
        }

        public List<Teacher> GetTeacherByDepartmentId(Department department)
        {
            Command.CommandText = @"SELECT * FROM Teacher where DepartmentId='"+department.Id+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Teacher> teachers=new List<Teacher>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Teacher t=new Teacher();
                    t.Id = (int)reader["Id"];
                    t.Name = reader["Name"].ToString();
                    t.Address = reader["Address"].ToString();
                    t.Email = reader["Email"].ToString();
                    t.ContactNo = reader["ContactNo"].ToString();
                    t.DesignationId = (int)reader["DesignationId"];
                    t.DepartmentId = (int)reader["DepartmentId"];
                    t.Credit = Convert.ToDouble(reader["Credit"]);
                    teachers.Add(t);
                }

            }
            reader.Close();
            Connection.Close();
            return teachers;
        }

        public Teacher GetTeacherById(Teacher teacher)
        {
            Command.CommandText = @"SELECT * FROM Teacher where Id='" + teacher.Id + "'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            Teacher t = new Teacher();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    t.Id = (int)reader["Id"];
                    t.Name = reader["Name"].ToString();
                    t.Address = reader["Address"].ToString();
                    t.Email = reader["Email"].ToString();
                    t.ContactNo = reader["ContactNo"].ToString();
                    t.DesignationId = (int)reader["DesignationId"];
                    t.DepartmentId = (int)reader["DepartmentId"];
                    t.Credit = Convert.ToDouble(reader["Credit"]);
                    
                }

            }
            reader.Close();
            Connection.Close();
            return t;
        }

        public double GetReaminCredit(Teacher teacher)
        {
            string query1 = @"select ISNULL(SUM(c.Credit),0) AS CreditTaken from Teacher t inner join EnrollCourseTeacher ect on t.Id=ect.TeacherId inner join Course c on ect.CourseId=c.Id where t.ID='" + teacher.Id + "' and ect.Status!='unassigned'";
            string query2 = @"select ISNULL(t.Credit,0) AS MaxCredit from Teacher t where t.Id='" + teacher.Id + "'";
            Command.CommandText = query1;
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            double creditTaken = 0;
            double maxCredit = 0;

            while (reader.Read())
            {
                creditTaken = Convert.ToDouble(reader["CreditTaken"]);

            }
            reader.Close();
            Connection.Close();
            Connection.Open();
            Command.CommandText = query2;
            SqlDataReader reader2 = Command.ExecuteReader();

            while (reader2.Read())
            {
                maxCredit = Convert.ToDouble(reader2["MaxCredit"]);

            }
            reader2.Close();
            Connection.Close();


            return maxCredit - creditTaken;
        }

        public bool EnrollCourseAndTeacher(EnrollCourseTeacher enrollCourseTeacher)
        {
            string query = "INSERT INTO EnrollCourseTeacher Values('" + enrollCourseTeacher.CourseId + "','" + enrollCourseTeacher.TeacherId + "','assigned')";
            Command.CommandText = query;
            Connection.Open();
            bool result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            string query2 = "UPDATE Course set Status='assigned',CourseAssignTo='" + enrollCourseTeacher.TeacherName + "' where Id='" + enrollCourseTeacher.CourseId + "'";
            Command.CommandText = query2;
            Connection.Open();
            bool result2 = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result && result2;
        }
    }
}