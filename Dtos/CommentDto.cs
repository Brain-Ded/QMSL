using QMSL.Enums;

namespace QMSL.Dtos
{
    public class CommentDto
    {
        public int DoctorId { get; set; }
        public string Text { get; set; }
        public CommentTypes type { get; set; }
        public DateTime CommentedAt { get; set; }
    }
}
