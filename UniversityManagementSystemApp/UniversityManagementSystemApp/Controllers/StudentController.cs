using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemApp.Manager;
using UniversityManagementSystemApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Font = System.Drawing.Font;

namespace UniversityManagementSystemApp.Controllers
{
    public class StudentController : Controller
    {
        private DepartmentManager _departmentManager = new DepartmentManager();
        private StudentManager _studentManager = new StudentManager();
        private CourseManager _courseManager = new CourseManager();
        private StudentEnrollCoursesManager _studentEnrollCoursesManager = new StudentEnrollCoursesManager();
        private GradeManager _gradeManager = new GradeManager();
        //
        // GET: /Student/
        public ActionResult Save()
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Student student)
        {
            ViewBag.Department = _departmentManager.GetAllDepartments();

            _studentManager.ValidateEmailAddress(student);
            if (_studentManager.Message == null)
            {
                Department department = _departmentManager.GetDepartmentCodeById(student);
                string regNo = _studentManager.CreateRegNo(student, department);
                student.RegistrationNo = regNo;
                bool rowAffected = _studentManager.Save(student);
                if (rowAffected)
                {
                    ViewBag.SuccessMessage = "Student Registered Successfully";
                    ViewBag.StudentInfo = student;
                    ViewBag.StudentDept = department.Name;
                }
                else
                {
                    ViewBag.ErrorMessage = "Student Registered Unuccessfully";
                }
            }
            else
            {
                ViewBag.ErrorMessage = _studentManager.Message;
            }
            return View();
        }

        public ActionResult EnrollCourse()
        {
            ViewBag.Student = _studentManager.GetAllStudent();
            return View();
        }




        [HttpPost]
        public ActionResult EnrollCourse(StudentEnrollCourses studentEnrollCourses)
        {
            bool result = false;
            _studentManager.ValidateStudent(studentEnrollCourses);




            if (ModelState.IsValid)
            {
                if (_studentManager.Message == null)
                {
                    result = _studentEnrollCoursesManager.Save(studentEnrollCourses);
                    if (result)
                    {
                        ViewBag.SuccessMessage = "Student Enrolled Successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Student Enrolled Failed";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = _studentManager.Message;
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Student Enrolled Failed";
            }

            ViewBag.Student = _studentManager.GetAllStudent();
            return View();
        }



        public JsonResult GetStudentById(Student student)
        {
            StudentDepartmentViewModel st = _studentManager.GetStudentById(student);
            return Json(st, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByStudentId(Student student)
        {
            List<Course> courses = _courseManager.GetCourseByStudentId(student);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEnrolledCourseByStudentId(Student student)
        {
            List<Course> courses = _studentEnrollCoursesManager.GetEnrolledCourseByStudentId(student);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public ViewResult SaveStudentResult()
        {
            ViewBag.Student = _studentManager.GetAllStudent();
            ViewBag.Grade = _gradeManager.GetAllGrade();
            return View();
        }

        [HttpPost]
        public ActionResult SaveStudentResult(StudentResult studentResult)
        {
            bool rowAffected = _studentManager.SaveStudentResult(studentResult);
            if (rowAffected)
            {
                ViewBag.SuccessMessage = "Student Result Saved Successfully";
            }
            else
            {
                ViewBag.ErrotrMessage = "Student Result Saved Failed";
            }
            ViewBag.Student = _studentManager.GetAllStudent();
            ViewBag.Grade = _gradeManager.GetAllGrade();
            return View();
        }

        public JsonResult GetStudentResult(StudentResult studentResult)
        {
            List<StudentResultViewModel> studentResultViewModels = _studentManager.GetStudentResult(studentResult);
            return Json(studentResultViewModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewStudentResult()
        {
            ViewBag.Student = _studentManager.GetAllStudent();
            return View();
        }

        [HttpPost]
        public ActionResult MakePDF(StudentResult studentResult)
        {
            StudentDepartmentViewModel st = _studentManager.GetStudentInfoById(studentResult);

            string regNo = st.RegistrationNo;
            string name = st.StudentName;
            string email = st.StudentEmail;
            string department = st.DepartmentName;

            using (MemoryStream ms = new MemoryStream())
            {
                List<StudentResultViewModel> studentResultViewModels = _studentManager.GetStudentResult(studentResult);
                PdfPTable table = new PdfPTable(3);

                table.WidthPercentage = 100.0f;
                table.AddCell("Course Code");
                table.AddCell("Name");
                table.AddCell("Grade");
                foreach (var viewResult in studentResultViewModels)
                {
                    table.AddCell(viewResult.CourseCode);
                    table.AddCell(viewResult.CourseName);
                    table.AddCell(viewResult.GradeName);

                }




                Document doc = new Document(PageSize.A4, 30f, 30f, 10f, 10f);
                iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 20, BaseColor.BLUE);
                iTextSharp.text.Font HeaderFont = FontFactory.GetFont("tahoma", 23, BaseColor.ORANGE);
                iTextSharp.text.Font InfoFont = FontFactory.GetFont("myriadPro", 14, BaseColor.GRAY);
                iTextSharp.text.Font tableHeader = FontFactory.GetFont("arial", 16, BaseColor.BLACK);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("University Course and Result Management System",HeaderFont));
                doc.Add(new Paragraph("                               Student Result", HeaderFont));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Student Information",NormalFont));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Name : " + name, InfoFont));
                doc.Add(new Paragraph("Registration No : " + regNo, InfoFont));
                doc.Add(new Paragraph("Department : " + department, InfoFont));
                doc.Add(new Paragraph("Email : " + email, InfoFont));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("Result of all Courses",tableHeader));
                
                doc.Add(new Paragraph(" "));
                doc.Add(table);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("                                                 Prepared By DotNet Learners Team"));

                doc.Close();
                byte[] bytes = ms.ToArray();
                ms.Close();

                //Response.Clear();
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                //Response.ContentType = "application/pdf";
                //Response.Buffer = true;
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(bytes);
                //Response.End();
                //Response.Close();

                return File(bytes, "application/pdf", "'" + name + "'.pdf");
            }
        }

    }
}