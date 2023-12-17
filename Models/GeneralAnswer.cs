using QMSL.Models;
using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public partial class GeneralAnswer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int? GeneralQuestionId { get; set; }
        //public GeneralQuestion? GeneralQuestion { get; set; } = null!;
        // public EditableQuestion? EditableQuestion { get; set; } = null!;

    }

    public partial class GeneralAnswer
    {
        public EditableAnswer getEditCopy()
        {
            return new EditableAnswer()
            {
                Text = Text
            };
        }
    }
}