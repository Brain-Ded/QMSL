using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMSL.Models
{
    public partial class GeneralQuestion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<GeneralAnswer> GeneralAnswers { get; set; } = new List<GeneralAnswer>();
        public int GeneralPollId { get; set; }
        //public GeneralPoll GeneralPoll { get; set; } = null!;
    }

    public partial class GeneralQuestion
    {
        public EditableQuestion getEditCopy()
        {
            return new EditableQuestion()
            {
                Name = Name,
                EditableAnswers = new List<EditableAnswer>(GeneralAnswers.Select(x => x.getEditCopy())),
            };
        }
    }
}
