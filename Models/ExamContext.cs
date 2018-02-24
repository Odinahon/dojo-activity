using Microsoft.EntityFrameworkCore;
 
namespace Exam.Models
{
    public class ExamContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ExamContext(DbContextOptions<ExamContext> options) : base(options) { }
        public DbSet<User> UserTable {get;set;}
        public DbSet<Activity> ActivityTable {get;set;}
        public DbSet<ActivityUser> ActivityUser {get;set;}
    }
}
