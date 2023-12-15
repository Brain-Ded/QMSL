using QMSL.Models;

namespace QMSL.Dtos
{
    public class EditablePollDto
    {
        public string Name { get; set; }
        public List<EditableQuestionDto> Questions { get; set; }
        public List<CommentDto> Comments { get; set; }
        public bool IsPassed { get; set; }
        public string patientEmail { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime PassedAt { get; set; }
    }
}
