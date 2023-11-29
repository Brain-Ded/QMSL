namespace QMSL.Models
{
    public class Patient : User
    {
        public List<Doctor>? Doctors { get; set; }
        public List<EditablePoll>? Polls { get; set; }
    }
}
