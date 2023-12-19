using System;
namespace ASHWorkersAPI.Controllers.Response
{
	public class WorkerResponse
	{
		public int WorkerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public double? AnnualSalary { get; set; }
        public double? PayPerHour { get; set; }
        public double? MaxExpenseAmount { get; set; }
    }
}

