using System.Data.Entity;

namespace WebDashBoardMVC.Models
{
    public class RecordContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

    }
}
