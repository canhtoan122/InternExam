using InternExam.Models;
using Microsoft.EntityFrameworkCore;

namespace InternExam.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
    }
}
