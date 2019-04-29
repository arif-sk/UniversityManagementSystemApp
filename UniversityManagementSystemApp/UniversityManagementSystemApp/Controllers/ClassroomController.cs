using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Controllers
{
    public class ClassroomController : Controller
    {
        private DepartmentManager _departmentManager = new DepartmentManager();
        private ClassroomManager _classroomManager = new ClassroomManager();
        private DayManager _dayManager = new DayManager();
        private AllocateClassRoomManager _allocateClassRoomManager= new AllocateClassRoomManager();
        public ActionResult AllocateClassroom()
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();
            ViewBag.Day = _dayManager.GetAllDays();
            ViewBag.Classroom = _classroomManager.GetAllRooms();
            return View();
        }

        [HttpPost]
        public ActionResult AllocateClassroom(AllocateClassroom allocateClassroom)
        {
            _allocateClassRoomManager.IsRoomAllocated(allocateClassroom);

            if (_allocateClassRoomManager.Message=="")
            {
                bool result = _allocateClassRoomManager.Save(allocateClassroom);
                if (result)
                {
                    ViewBag.SuccessMessage = "Class Room Allocated Successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Class Room Allocation Failed";
                }
            }
            else
            {
                ViewBag.ErrorMessage = _allocateClassRoomManager.Message;
            }
           

          

            ViewBag.Department = _departmentManager.GetAllDepartments();
            ViewBag.Classroom = _classroomManager.GetAllRooms();
            ViewBag.Day = _dayManager.GetAllDays();
            return View();
        }

        public ActionResult ShowAllCourseAllocation()
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();
            return View();
        }
        public JsonResult GetAllocatedRoomsByDept(string dept)
        {

            var data = _allocateClassRoomManager.GetCourseSheduleByDept(dept);

            //foreach (var v in data)
            //{
            //    Response.Write(v.Code+ ""+v.CourseName);
            //    foreach (var r in v.RoomAndTimes)
            //    {
            //        Response.Write(" Room No      " + r.RoomNo+ r.DayName+r.FromTimeString+" - "+r.ToTimeString);
            //    }

            //}
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnallocateAllClassroom()
        {

            return View();
        }

        [HttpPost]
        [ActionName("UnallocateAllClassroom")]
        public ActionResult Unallocate()
        {
            bool rowAffected = _classroomManager.UnassignAllClassroms();
            if (rowAffected)
            {
                ViewBag.SuccessMessage = "Classroom Unallocation Successfull";
            }
            else
            {
                ViewBag.ErrorMessage = "No Classroom Allocation Availeable for Unallocation";
            }
            return View();
            
        }
    }
}