using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class SemesterGateway : CommonGateway
    {
        public List<Semester> GetAllSemester()
        {

            Command.CommandText = "SELECT * FROM Semester";
            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();
            List<Semester> semesters = new List<Semester>();

            while (reader.Read())
            {
                Semester semester = new Semester();
                semester.Id = (int)reader["Id"];
                semester.Name = reader["Name"].ToString();
                semesters.Add(semester);
            }

            reader.Close();
            Connection.Close();

            return semesters;
        }
    }
}