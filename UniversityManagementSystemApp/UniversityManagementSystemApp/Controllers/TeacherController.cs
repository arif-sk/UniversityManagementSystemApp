using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class TeacherController : Controller
    {
        private DesignationManager _designationManager = new DesignationManager();
        private DepartmentManager _departmentManager = new DepartmentManager();
        private TeacherManager _teacherManager = new TeacherManager();
        private  CourseManager _courseManager=new CourseManager();
        
        [HttpGet]
        public ActionResult Save()
        {

            ViewBag.Designation = _designationManager.GetAllDesignation();
            ViewBag.Deartment = _departmentManager.GetAllDepartments();


            return View();
        }

        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            ViewBag.Designation = _designationManager.GetAllDesignation();
            ViewBag.Deartment = _departmentManager.GetAllDepartments();
            _teacherManager.ValidateTeacher(teacher);
            _teacherManager.IsTeacherAvailable(teacher);
            if (_teacherManager.Message == null)
            {
                bool rowAffected = _teacherManager.Save(teacher);
                if (rowAffected)
                {
                    ViewBag.SuccessMessage = "Teacher Saved Successfully";
                }

            }
            ViewBag.ErrorMessage = _teacherManager.Message;
            return View();
        }

        public ActionResult AssignCourse()
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();
            
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourse(EnrollCourseTeacher enrollCourseTeacher)
        {
            CourseTeacherViewModel ctvm = _courseManager.IsCourseAssigned(enrollCourseTeacher);
            if (ctvm==null)
            {
                bool rowAffected = _teacherManager.EnrollCourseAndTeacher(enrollCourseTeacher);
                if (rowAffected)
                {
                    ViewBag.SuccessMessage="Course and Teacher Enroll Successfullly";
                }
                else
                {
                    ViewBag.ErrorMessage="Course and Teacher Enroll Failed";
                }
            }
            else
            {
                ViewBag.ErrorMessage ="Assign Failed! "+ ctvm.CourseName+" already enrolled to "+ctvm.TeacherName;
            }
            ViewBag.Department = _departmentManager.GetAllDepartments();
            return View();
        }

        public JsonResult GetTeacherByDepartmentId(Department department)
        {
            List<Teacher> teachers = _teacherManager.GetTeacherByDepartmentId(department);
            return Json(teachers,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseByDepartmentId(Department department)
        {
            
            List<Course> courses = _courseManager.GetCourseByDepartmentId(department);
            return Json(courses,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeacherById(Teacher teacher)
        {
            Teacher t = _teacherManager.GetTeacherById(teacher);
            double remainingCredit = _teacherManager.GetReaminCredit(teacher);
            t.RemainingCredit = remainingCredit;
            return Json(t, JsonRequestBehavior.AllowGet);
        }


    }
}