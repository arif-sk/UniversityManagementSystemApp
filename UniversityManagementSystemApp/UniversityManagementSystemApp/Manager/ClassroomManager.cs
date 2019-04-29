using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class ClassroomManager
    {
        private ClassroomGateway _classroomGateway=new ClassroomGateway();
        public List<Classroom> GetAllRooms()
        {
            return _classroomGateway.GetAllRooms();
        }


        public bool UnassignAllClassroms()
        {
            return _classroomGateway.UnassignAllClassroms();
        }
    }
}