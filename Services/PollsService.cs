using QMSL.Models;

namespace QMSL.Services
{
    //Katya
    //Nazar
    //Maxim
    public class PollsService
    {
        //Nazar
        public GeneralPoll CreatePoll(string name, List<GeneralQuestion> questions)
        {
            if (!string.IsNullOrEmpty(name) && questions?.Count > 0)
                return new GeneralPoll() { Name = name, Questions = questions };

            throw new ArgumentNullException();
        }
        //Nazar
        public GeneralPoll EditPoll(int id, GeneralPoll edited)
        {
            throw new NotImplementedException();
        }
        //Nazar
        public void DeletePoll(int id) { throw new NotImplementedException(); }
        //Katya
        public void AssignPoll(int PatientId, int PollId) { throw new NotImplementedException(); }
        //Katya
        public void UnassignPoll(int PatientId, int PollId) { throw new NotImplementedException(); }
        //Katya
        public void CommentPoll(int PollId, Comment comment) { throw new NotImplementedException(); }
        //Maxim
        public EditablePoll GetEditCopy(int PollId) { throw new NotImplementedException(); }
        //Maxim
        public EditableQuestion GetEditQuestion(int QuestionId) { throw new NotImplementedException(); }
        //Maxim
        public void PassPoll(int PollId) { throw new NotImplementedException(); }
    }
}
