namespace ASHWorkersAPI.Models
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }
        public double AnnualSalary { get; set; }
        public Worker Worker { get; set; }
    }
}

