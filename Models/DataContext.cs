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
        public DataContext() : base() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<GeneralPoll> GeneralPolls { get; set; }
        public virtual DbSet<EditablePoll> EditablePolls { get; set; }
        public virtual DbSet<GeneralQuestion> GeneralQuestions { get; set; }
        public virtual DbSet<EditableQuestion> EditableQuestions { get; set; }
        public virtual DbSet<GeneralAnswer> GeneralAnswers { get; set; }
        public virtual DbSet<EditableAnswer> EditableAnswers { get; set; }
    }
}