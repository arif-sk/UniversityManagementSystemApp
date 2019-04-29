using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class GradeGateway:CommonGateway
    {
        public List<Grade> GetAllGrade()
        {
            Command.CommandText = @"select * from Grade";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Grade> grades = new List<Grade>();
            while (reader.Read())
            {
                Grade grade = new Grade();
                grade.Id = (int)reader["Id"];
                grade.Name = reader["Name"].ToString();

                grades.Add(grade);
            }

            reader.Close();
            Connection.Close();
            return grades; 

        }
    }
}