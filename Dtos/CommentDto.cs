using QMSL.Enums;

namespace QMSL.Dtos
{
    public class CommentDto
    {
        public string Text { get; set; }
        public int DoctorId { get; set; }
        public CommentTypes Type { get; set; }
    }
}
