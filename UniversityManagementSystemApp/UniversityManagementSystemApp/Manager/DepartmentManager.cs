using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class DepartmentManager
    {
        public string Message { get; set; }
        DepartmentGateway departmentGateway=new DepartmentGateway();
        public bool Save(Department department)
        {

            return departmentGateway.Save(department)>0;
        }

        public void ValidateDepartment(Department department)
        {
            if (department.Code.Length<2||department.Code.Length>7)
            {
                Message = "code must be two (2) to seven (7) characters long.";
            }
           
        }

        public void IsDepartmentAvailable(Department department)
        {
            Department dep = GetByDepartment(department);
            if (dep!=null)
            {
                Message = "Code and Name must be unique.";
            }
        }

        public Department GetByDepartment(Department department)
        {
            return departmentGateway.GetByDepartment(department);
        }

        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }

        public Department GetDepartmentCodeById(Student student)
        {
            return departmentGateway.GetDepartmentCodeById(student);
        }
    }
}