using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.Models
{
    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; } 
        public string Address { get; set; }
        public string Organization { get; set; }
    }
}
