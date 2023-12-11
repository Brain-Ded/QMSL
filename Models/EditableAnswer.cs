using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public class EditableAnswer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int? EditableQuestionId { get; set; }
    }
}
