namespace ASHWorkersAPI.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public double AnnualSalary { get; set; }
        public double MaxExpenseAmount { get; set; }
        public Worker Worker { get; set; }
    }
}




