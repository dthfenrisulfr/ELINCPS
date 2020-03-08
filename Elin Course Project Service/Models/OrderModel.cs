using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.Models
{
	public class OrderModel
	{
		public int OrderID { get; set; }
		public int CustomerID { get; set; }
		public DateTime Date { get; set; }
		public string Condition { get; set; }
		public int StaffID { get; set; }
	}
}
