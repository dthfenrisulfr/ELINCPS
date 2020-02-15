using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.Models
{
    public class StaffModel
    {
        public int Passport { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BDate { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public string Experience { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
    }
}
