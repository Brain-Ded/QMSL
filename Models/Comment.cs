using QMSL.Enums;

namespace QMSL.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Text { get; set; }
        public CommentTypes type { get; set; }
        public DateTime CommentedAt { get; set; }
    }

    public partial class Comment
    {
        public void lol()
        {
           
        }
    }
}
