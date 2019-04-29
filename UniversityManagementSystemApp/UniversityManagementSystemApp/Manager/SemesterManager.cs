using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class SemesterManager
    {
        private SemesterGateway _semesterGateway=new SemesterGateway();
        public List<Semester> GetAllSemester()
        {
            return _semesterGateway.GetAllSemester();
        }
    }
}