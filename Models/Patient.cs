using System.Text.Json.Serialization;

namespace QMSL.Models
{
    public class Patient : User
    {
        [JsonIgnore]
        public List<Doctor>? Doctors { get; set; }
        public List<EditablePoll>? Polls { get; set; }
    }
}
