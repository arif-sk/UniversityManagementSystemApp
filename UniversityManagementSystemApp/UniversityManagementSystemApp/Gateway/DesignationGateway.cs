using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class DesignationGateway : CommonGateway
    {
        public List<Designation> GetAllDesignation()
        {

            Command.CommandText = "SELECT * FROM Designation";
            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            List<Designation> designations = new List<Designation>();

            while (reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = (int)reader["Id"];
                designation.Name = reader["Name"].ToString();

                designations.Add(designation);
            }

            reader.Close();
            Connection.Close();

            return designations;
        }
    }
}