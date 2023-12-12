using Microsoft.EntityFrameworkCore;
using QMSL.Models;


namespace QMSL
{
    public class DataContext : DbContext
    {
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Patient>().HasData(new Patient()
        //    {
        //        Id = 847,
        //        Email = "vlad228@gmail.com",
        //        Password = new byte[] {0, 0, 0, 0},
        //        Age = 25,
        //        Sex = "Male",
        //        Name = "Vlad",
        //        Surname = "Atorin",
        //        Fathername = "Evhenovich",
        //        Disease = "Cock Cancer",
        //        PhoneNumber = "+1234567890",
        //    });
        //}
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<GeneralPoll> GeneralPolls { get; set; }
        public DbSet<EditablePoll> EditablePolls { get; set; }
        public DbSet<GeneralQuestion> GeneralQuestions { get; set; }
        public DbSet<EditableQuestion> EditableQuestions { get; set; }
        public DbSet<GeneralAnswer> GeneralAnswers { get; set; }
        public DbSet<EditableAnswer> EditableAnswers { get; set; }
    }
}