using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public abstract class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Fathername { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }

    }
}
