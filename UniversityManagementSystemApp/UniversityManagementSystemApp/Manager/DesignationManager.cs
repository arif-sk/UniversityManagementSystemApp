using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class DesignationManager
    {
        private  DesignationGateway _designationGateway=new DesignationGateway();
        public List<Designation> GetAllDesignation()
        {
            return _designationGateway.GetAllDesignation();
        }
    }
}