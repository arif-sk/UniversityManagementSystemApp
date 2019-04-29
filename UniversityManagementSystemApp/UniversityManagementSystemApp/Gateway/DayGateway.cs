using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class DayGateway:CommonGateway
    {
        public List<Day> GetAllDays()
        {
            Command.CommandText = @"SELECT * FROM Day";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Day> days = new List<Day>();
            while (reader.Read())
            {
                Day day = new Day();
                day.Id = (int)reader["Id"];
                day.Name = reader["Name"].ToString();

                days.Add(day);
            }

            reader.Close();
            Connection.Close();
            return days; 
        }
    }
}