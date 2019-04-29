using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class DayManager
    {
        private DayGateway _dayGateway=new DayGateway();
        public List<Day> GetAllDays()
        {
            return _dayGateway.GetAllDays();
        }
    }
}