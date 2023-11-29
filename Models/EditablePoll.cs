namespace QMSL.Models
{
    public class EditablePoll : GeneralPoll
    {
        public List<EditableQuestion> Questions { get; set; }
        public List<string> Comments { get; set; }
    }
}
