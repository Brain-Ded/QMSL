namespace QMSL.Models
{
    public class GeneralPoll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GeneralQuestion> Questions { get; set; }
    }
}
