using QMSL.Models;

namespace QMSL.Dtos
{
    public class EditableQuestionDto
    {
        public string Name { get; set; }
        public List<EditableAnswerDto> EditableAnswers { get; set; }
        public int? ChoosenAnswer { get; set; }
    }
}
