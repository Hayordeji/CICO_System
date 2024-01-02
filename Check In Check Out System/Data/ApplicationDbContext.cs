using Check_In_Check_Out_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Check_In_Check_Out_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Record> Records { get; set; }
 
    }
}