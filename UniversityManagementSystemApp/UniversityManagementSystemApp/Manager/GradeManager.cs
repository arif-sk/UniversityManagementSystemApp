using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class GradeManager
    {
        private GradeGateway _gradeGateway=new GradeGateway();
        public List<Grade> GetAllGrade()
        {
            return _gradeGateway.GetAllGrade();
        }
    }
}