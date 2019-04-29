using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Models;

namespace UniversityManagementSystemApp.Gateway
{
    public class AllocateClassRoomGateway : CommonGateway
    {
        public bool Save(AllocateClassroom allocateClassroom)
        {

            bool result = false;


            string query = "INSERT INTO AllocateClassroom(DepartmentId,CourseId,ClassroomId,DayId,FromTime,ToTime) " +
                           " Values(" + allocateClassroom.DepartmentId + ", " + allocateClassroom.CourseId + "," + allocateClassroom.ClassroomId + "," + allocateClassroom.DayId + ",'" + allocateClassroom.FromTime + "','" + allocateClassroom.ToTime + "')";
            Command.CommandText = query;

            Connection.Open();
            result = Command.ExecuteNonQuery() > 0;
            Connection.Close();

            return result;
        }
        public List<AllocateClassroom> GetAllAllocationList(AllocateClassroom allocateClassroom)
        {
            Command.CommandText = "SELECT * FROM AllocateClassroom where ClassroomId='"+allocateClassroom.ClassroomId+"' AND DayId='"+allocateClassroom.DayId+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<AllocateClassroom> allocateClassrooms = new List<AllocateClassroom>();
            while (reader.Read())
            {
                AllocateClassroom ac = new AllocateClassroom();
                ac.Id = (int)reader["Id"];
                ac.DepartmentId = (int)reader["DepartmentId"];
                ac.CourseId = (int)reader["CourseId"];
                ac.ClassroomId = (int)reader["ClassroomId"];
                ac.DayId = (int)reader["DayId"];
                ac.ClassroomId = (int)reader["ClassroomId"];
                ac.FromTime = (int)reader["FromTime"];
                ac.ToTime = (int)reader["ToTime"];

                allocateClassrooms.Add(ac);
            }

            reader.Close();
            Connection.Close();
            return allocateClassrooms;

        }

        public List<StructredCourseScheduleViewModel> GetCourseScheduleByDeptCode(string deptcode)
        {
            Command.CommandText = "SELECT * FROM dbo.vw_CourseSchedual WHERE DeptCode = '"+deptcode+"'";
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<StructredCourseScheduleViewModel> result = new List<StructredCourseScheduleViewModel>();
            while (reader.Read())
            {
                CourseScheduleViewModel temp = new CourseScheduleViewModel();
                temp.Code = reader["Code"]!= null ? reader["Code"].ToString(): "-1";
                temp.CourseName = reader["CourseName"]!= null ? reader["CourseName"].ToString(): "-1";
                temp.DayName = reader["DayName"] != null ? reader["DayName"].ToString() : "-1";
                temp.DeptCode = reader["DeptCode"] != DBNull.Value ? reader["DeptCode"].ToString() : "-1";
                temp.FromTime = reader["FromTime"] != DBNull.Value ? (int)reader["FromTime"] : 0;
                temp.ToTime = reader["ToTime"] != DBNull.Value ? (int)reader["ToTime"] : 0;
                temp.RoomNo = reader["RoomNo"] != DBNull.Value ? reader["RoomNo"].ToString() : "-1";
                if(temp.Code.Equals("-1"))
                    continue;
                RoomAndTime rt = new RoomAndTime();
                rt.RoomNo = temp.RoomNo.Equals("-1") ? "Not Scheduled Yet" : temp.RoomNo;
                rt.DayName = temp.DayName.Equals("-1") ? string.Empty : temp.DayName;
                rt.ToTime = temp.ToTime;
                rt.FromTime = temp.FromTime;

                if( result.FirstOrDefault(x=>x.Code.Equals(temp.Code)) != null)
                {
                    // existing
                    var t = result.FirstOrDefault(x => x.Code.Equals(temp.Code));
                    t.RoomAndTimes.Add(rt);
                }
                else
                {
                    var t = new StructredCourseScheduleViewModel();
                    t.Code = temp.Code;
                    t.CourseName = temp.CourseName;

                    t.RoomAndTimes.Add(rt);
                    result.Add(t);
                    // new 
                }
            }

            reader.Close();
            Connection.Close();
            return result;
        }
    }
}