using System;
namespace ASHWorkersAPI.Controllers.Requests
{
	public class WorkerRequest
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Address { get; set; }
        public double WorkerType { get; set; }
        public double? AnnualSalary { get; set; }
		public double? PayPerHour { get; set; }
		public double? MaxExpenseAmount { get; set; }
	}
}

