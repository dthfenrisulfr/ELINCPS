using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.Models
{
    public class CustomersModel
    {

        public int CustomerID { get; set; }
        public string PaymentAccount { get; set; }
        public DateTime DateOfContractCompletion { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Organization { get; set; }
        public string Address { get; set; }

    }
}
