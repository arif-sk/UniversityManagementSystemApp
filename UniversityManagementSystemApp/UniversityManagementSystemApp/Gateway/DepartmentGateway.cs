using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class DepartmentGateway:CommonGateway
    {
        public Department GetByDepartment(Department department)
        {
            Command.CommandText = "SELECT * FROM Department where Code='" + department.Code + "' OR Name='"+department.Name+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Department d = null;
            
            if(reader.HasRows){
                while (reader.Read())
                {
                    d = new Department();
                    d.Id = (int)reader["Id"];
                    d.Code = reader["Code"].ToString();
                    d.Name = reader["Name"].ToString();
                }

            }
            reader.Close();
            Connection.Close();
            return d;
            
        }

        public int Save(Department department)
        {
            string query = "INSERT INTO Department Values('" + department.Code + "','" + department.Name + "')";
            Command.CommandText = query;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<Department> GetAllDepartments()
        {
            Command.CommandText = "SELECT * FROM Department  order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Department> departments = new List<Department>();
            while (reader.Read())
            {
                Department department = new Department();
                department.Id = (int) reader["Id"];
                department.Code = reader["Code"].ToString();
                department.Name = reader["Name"].ToString();
                departments.Add(department);
            }

            reader.Close();
            Connection.Close();
            return departments;
        }

        public Department GetDepartmentCodeById(Student student)
        {
            Command.CommandText = "SELECT * FROM Department where Id='" + student.DepartmentId + "' order by Name";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            Department d = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    d = new Department();
                    d.Id = (int)reader["Id"];
                    d.Code = reader["Code"].ToString();
                    d.Name = reader["Name"].ToString();
                }

            }
            reader.Close();
            Connection.Close();
            return d;
        }
    }
}