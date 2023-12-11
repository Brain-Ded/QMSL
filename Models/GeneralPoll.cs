using QMSL.Dtos;
using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public partial class GeneralPoll : Poll
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GeneralQuestion> Questions { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }

    public partial class GeneralPoll : Poll
    {
        public override Poll getCopy()
        {
            return (Poll)this.MemberwiseClone();
        }
        public EditablePoll getEditCopy()
        {
            return new EditablePoll()
            {
                Name = Name,
                Questions = new List<EditableQuestion>(),
                IsPassed = false
            };
        }
    }
}
