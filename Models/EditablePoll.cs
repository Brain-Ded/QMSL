using QMSL.Dtos;
using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public partial class EditablePoll : Poll
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditableQuestion> Questions { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsPassed { get; set; }
        public int PatientId { get; set; }
        public Patient Patients { get; set; } = null!;
    }

    public partial class EditablePoll
    {
        public override Poll getCopy()
        {
            return (Poll)this.MemberwiseClone();
        }
    }
}
