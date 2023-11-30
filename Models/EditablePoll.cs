namespace QMSL.Models
{
    public class EditablePoll : GeneralPoll
    {
        public List<EditableQuestion> Questions { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
