using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public class GeneralQuestion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public int GeneralPollId { get; set; }
        //public GeneralPoll GeneralPoll { get; set; } = null!;
    }
}
