using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class CourseScheduleViewModel
    {
        public string Code { get; set; }
        public string CourseName { get; set; }
        public string RoomNo { get; set; }
        public int FromTime { get; set; }
        public int ToTime { get; set; }
        public string DayName { get; set; }
        public string DeptCode { get; set; }
    }

    public class StructredCourseScheduleViewModel
    {
        public StructredCourseScheduleViewModel()
        {
            RoomAndTimes = new List<RoomAndTime>();
        }
        public string Code { get; set; }
        public string CourseName { get; set; }
        public List<RoomAndTime> RoomAndTimes { get; set; }


    }

    public class RoomAndTime
    {
        public string RoomNo { get; set; }
        public int FromTime { get; set; }
        public int ToTime { get; set; }
        public string DayName { get; set; }
        public string DeptCode { get; set; }

        public string FromTimeString
        {
            set { }
            get
            {
                var hh = FromTime / 60;
                var mm = (FromTime % 60);
                if (hh >= 13)
                {
                    hh = hh - 12;
                    return hh.ToString() + ":" + mm.ToString() + "  PM";
                }
                if (hh >= 12 && hh < 13)
                {
                    return hh.ToString() + ":" + mm.ToString() + "  PM";
                }

                return hh.ToString() + ":" + mm.ToString() + "  AM";
            }
        }

        public string ToTimeString
        {
            set { }
            get
            {
                var hh = ToTime / 60;
                var mm = ToTime % 60;
                if (hh >= 13)
                {
                    hh = hh - 12;
                    return hh.ToString() + ":" + mm.ToString() + "  PM";
                }
                if (hh >= 12 && hh < 13)
                {
                    return hh.ToString() + ":" + mm.ToString() + "  PM";
                }
                return hh.ToString() + ":" + mm.ToString() + "  AM";
            }
        }
    }
}