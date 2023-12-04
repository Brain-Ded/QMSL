using QMSL.Dtos;

namespace QMSL.Models
{
    public partial class GeneralPoll : Poll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GeneralQuestion> Questions { get; set; }

        
    }

    public partial class GeneralPoll : Poll
    {
        public override Poll getCopy()
        {
            return (Poll)this.MemberwiseClone();
        }
        public EditablePoll getEditCopy()
        {
            return new EditablePoll();
        }
    }
}
