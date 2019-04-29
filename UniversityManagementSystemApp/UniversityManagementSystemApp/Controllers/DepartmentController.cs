using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        public ActionResult Save()
        {
            List<Department> departments = departmentManager.GetAllDepartments();
            ViewBag.Department = departments;
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department department)
        {

            if (department.Code != null && department.Name != null)
            {
                departmentManager.ValidateDepartment(department);
                departmentManager.IsDepartmentAvailable(department);
                if (departmentManager.Message == null)
                {
                    bool rowAffected = departmentManager.Save(department);
                    if (rowAffected)
                    {
                        ViewBag.SuccessMessage = "Department Saved Successfully";
                    }
                }
                ViewBag.ErrorMessage = departmentManager.Message;
                
            }
            return View();
        }

        public ActionResult ViewAllDepartment()
        {
            List<Department> departments = departmentManager.GetAllDepartments();
            ViewBag.Department = departments;
            return View();
        }
	}
}