namespace QMSL.Models
{
    public class Doctor : User
    {
        public List<Patient>? Patients { get; set; }
        public List<GeneralPoll>? Polls { get; set; }
    }
}
