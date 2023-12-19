namespace ASHWorkersAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public double PayPerHour { get; set; }
        public Worker Worker { get; set; }
    }
}



