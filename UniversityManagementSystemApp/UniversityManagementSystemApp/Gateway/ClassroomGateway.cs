using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class ClassroomGateway:CommonGateway
    {
        public List<Classroom> GetAllRooms()
        {
            Command.CommandText = "SELECT * FROM Classroom";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Classroom> classrooms = new List<Classroom>();
            while (reader.Read())
            {
                Classroom classroom = new Classroom();
                classroom.Id = (int)reader["Id"];
                classroom.RoomNo = reader["RoomNo"].ToString();
                
                classrooms.Add(classroom);
            }

            reader.Close();
            Connection.Close();
            return classrooms;  
        }


        public bool UnassignAllClassroms()
        {
            bool result = false;
            string query = "Delete from AllocateClassroom;";
            Command.CommandText = query;
            Connection.Open();
            result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result; 
        }
    }
}