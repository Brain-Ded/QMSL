using QMSL.Dtos;

namespace QMSL.Models
{
    public partial class EditablePoll : GeneralPoll
    {
        public List<EditableQuestion> Questions { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public partial class EditablePoll
    {
        public override Poll getCopy()
        {
            return (Poll)this.MemberwiseClone();
        }
    }
}
