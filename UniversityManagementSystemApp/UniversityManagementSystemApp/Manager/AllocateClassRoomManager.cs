using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Gateway;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Manager
{
    public class AllocateClassRoomManager
    {
        public string Message { get; set; }
        private AllocateClassRoomGateway _allocateClassRoomGateway = new AllocateClassRoomGateway();
        public bool Save(AllocateClassroom allocateClassroom)
        {
            return _allocateClassRoomGateway.Save(allocateClassroom);
        }

        public List<AllocateClassroom> GetAllAllocationList(AllocateClassroom allocateClassroom)
        {
            return _allocateClassRoomGateway.GetAllAllocationList(allocateClassroom);
        }

        public List<StructredCourseScheduleViewModel> GetCourseSheduleByDept(string dept)
        {
            return _allocateClassRoomGateway.GetCourseScheduleByDeptCode(dept);
        }

        public void IsRoomAllocated(AllocateClassroom allocateClassroom)
        {
            var allcocationList = GetAllAllocationList(allocateClassroom);

            string msg;

            if (allcocationList.Count == 0)
            {
                Message = "";

            }
            else
            {
                bool status = false;
                foreach (var a in allcocationList)
                {

                    if ((allocateClassroom.FromTime >= a.FromTime && allocateClassroom.FromTime < a.ToTime)
                         || (allocateClassroom.ToTime > a.FromTime && allocateClassroom.ToTime <= a.ToTime))
                    {
                        Message = "Time Clash for room alocation. Please Try another time.";
                       
                        status = true;
                    }

                }

                if (status == false)
                {
                    Message = "";


                }
                else
                {
                    Message = "Time Clash for room alocation. Please Try another time.";
                }
            }
        }

    }
}