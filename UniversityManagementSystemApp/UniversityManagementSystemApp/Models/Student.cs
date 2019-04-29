using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Please provide vaild email")]
        public string Email { get; set; }
        public string RegistrationNo { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
       
        public DateTime CreatedAt { get; set; }
        [Required]
        public int DepartmentId { get; set; }


    }
}