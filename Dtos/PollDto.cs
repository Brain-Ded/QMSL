using QMSL.Models;

namespace QMSL.Dtos
{
    public class PollDto
    {
        public string Name { get; set; }
        public string DoctorEmail { get; set; }
        public List<GeneralQuestionDto> Questions { get; set; }
    }
}
