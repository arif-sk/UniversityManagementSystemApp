using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class HomeController : Controller
    {
        DepartmentManager departmentManager=new DepartmentManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Department()
        {
            List<Department> departments = departmentManager.GetAllDepartments();
            ViewBag.Department = departments;
            return View();
        }
        [HttpPost]
        public ActionResult Department(Department department)
        {
           
            if (department.Code != null && department.Name != null)
            {
                departmentManager.ValidateDepartment(department);
                departmentManager.IsDepartmentAvailable(department);
                if (departmentManager.Message==null)
                {
                    bool rowAffected = departmentManager.Save(department);
                    if (rowAffected)
                    {
                        ViewBag.SuccessMessage = "Department Saved Successfully";
                    }
                }
                ViewBag.ErrorMessage = departmentManager.Message;
                List<Department> departments = departmentManager.GetAllDepartments();
                ViewBag.Department = departments;
            }
            return View();
        }
    }
}