using Microsoft.EntityFrameworkCore;
using QMSL.Enums;
using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Text { get; set; }
        public CommentTypes type { get; set; }
        public DateTime CommentedAt { get; set; }
        public int EditablePollId { get; set; }
        public EditablePoll EditablePoll { get; set; } = null!;
    }

    public partial class Comment
    {
        public void lol()
        {
           
        }
    }
}
