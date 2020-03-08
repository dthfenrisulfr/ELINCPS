using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.Converter
{
    public static class StaffConverter
    {
        public static CPS.Staff ToStaff(Models.StaffModel staffModel)
        {
            return new CPS.Staff
            {
                BDate = staffModel.BDate.Ticks,
                DateOfReceipt = staffModel.DateOfReceipt.Ticks,
                Department = staffModel.DepartmentID.ToString(),
                Experience = staffModel.Experience,
                Gender = staffModel.Gender,
                MiddleName = staffModel.MiddleName,
                Name = staffModel.Name,
                Passport = staffModel.Passport,
                Position = staffModel.PositionID.ToString(),
                SecondName = staffModel.SecondName
            };
        }
        public static Models.StaffModel ToStaffModel(CPS.Staff staff)
        {
            return new Models.StaffModel
            {
                BDate = new DateTime(staff.BDate),
                DateOfReceipt = new DateTime(staff.DateOfReceipt),
                DepartmentID = Convert.ToInt32(staff.Department),
                Experience = staff.Experience, Gender = staff.Gender,
                MiddleName = staff.MiddleName, Name = staff.Name,
                Passport = staff.Passport, PositionID = Convert.ToInt32(staff.Position), 
                SecondName = staff.SecondName
            };
        }
    }
}
