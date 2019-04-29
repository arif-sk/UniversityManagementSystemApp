using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class CourseController : Controller
    {
        private CourseManager _courseManager = new CourseManager();
        private SemesterManager _semesterManager = new SemesterManager();
        private DepartmentManager _departmentManager = new DepartmentManager();

        [HttpGet]
        public ActionResult Save()
        {

            ViewBag.Semester = _semesterManager.GetAllSemester();
            ViewBag.Department = _departmentManager.GetAllDepartments();

            return View();
        }

        [HttpPost]
        public ActionResult Save(Course course)
        {
            
            _courseManager.ValidateCourse(course);
            _courseManager.IsCourseAvailable(course);
            if (_courseManager.Message == null)
            {
                
                bool rowAffected = _courseManager.Save(course);
                if (rowAffected)
                {
                    ViewBag.SuccessMessage = "Course Saved Successfully";
                }
               
            }
            ViewBag.ErrorMessage = _courseManager.Message;
            ViewBag.Semester = _semesterManager.GetAllSemester();
            ViewBag.Department = _departmentManager.GetAllDepartments();

            return View();
        }

        public JsonResult GetCourseByCourseCode(Course course)
        {
            Course c = _courseManager.GetCourseByCourseId(course);
            return Json(c,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCourses(CourseTeacherViewModel courseTeacherViewModel )
        {
            List<CourseTeacherViewModel> courseTeacherViewModels = _courseManager.GetAllCourses(courseTeacherViewModel);
            return Json(courseTeacherViewModels,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowAllCourses()
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();
            return View();
        }

        public ActionResult UnassignAllCourses()
        {
            return View();
        }
        [HttpPost]
        [ActionName("UnassignAllCourses")]
        public ActionResult Unassign()
        {
            bool rowAffected = _courseManager.UnassignAllCourses();
            if (rowAffected)
            {
                ViewBag.SuccessMessage = "Course Unassign Successfull";
            }
            else
            {
                ViewBag.ErrorMessage = "No course Availeable for Unassign";
            }
            return View();
        }
    }
}