namespace ASHWorkersAPI.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Address { get; set; }
    }
}



