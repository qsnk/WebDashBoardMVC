namespace WebDashBoardMVC.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string? OfficeName { get; set; }
        public string? EmployeName { get; set; }
        public DateTime Date { get; set; }
        public int ClientsNumber { get; set; }
        public int ClientsCalls { get; set; }
        public int ClientsMeets { get; set; }
    }
}
