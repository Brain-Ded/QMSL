using System.ComponentModel.DataAnnotations;

namespace QMSL.Models
{
    public class GeneralQuestion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Answers { get; set; }
        public int GeneralPollId { get; set; }
        public GeneralPoll GeneralPoll { get; set; } = null!;
    }
}
