using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Models
{
    public class QuizDbContext:DbContext
    {
        public QuizDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Question> questions { get; set; }
        public DbSet<Participant> participants { get; set; }
    }
}
