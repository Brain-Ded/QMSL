using System.Text.Json.Serialization;

namespace QMSL.Models
{
    public class Doctor : User
    {
        [JsonIgnore]
        public List<Patient>? Patients { get; set; }
        public List<GeneralPoll>? Polls { get; set; }
    }
}
