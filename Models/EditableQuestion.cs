using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public class EditableQuestion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EditableAnswer> EditableAnswers { get; set; }
        public int? ChoosenAnswer { get; set; }
        public int EditablePollId { get; set; }
        //public EditablePoll EditablePoll { get; set; } = null!;
    }
}
