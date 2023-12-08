using Microsoft.EntityFrameworkCore;
using QMSL.Models;


namespace QMSL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<GeneralPoll> GeneralPolls { get; set; }
        public DbSet<EditablePoll> EditablePolls { get; set; }
        public DbSet<GeneralQuestion> GeneralQuestions { get; set; }
        public DbSet<EditableQuestion> EditableQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}