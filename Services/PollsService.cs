using QMSL.Models;

namespace QMSL.Services
{
    public class PollsService
    {
        //Vlad
        public GeneralPoll CreatePoll(string name, List<GeneralQuestion> questions)
        {
            if (!string.IsNullOrEmpty(name) && questions?.Count > 0)
                return new GeneralPoll() { Name = name, Questions = questions };

            throw new ArgumentNullException();
        }
        //Vlad
        public GeneralPoll EditPoll(int id, GeneralPoll edited)
        {
            throw new NotImplementedException();
        }
        //Vlad
        public void DeletePoll(int id) { throw new NotImplementedException(); }
        //Nazar
        public void AssignPoll(int PatientId, int PollId) { throw new NotImplementedException(); }
        //Nazar
        public void UnassignPoll(int PatientId, int PollId) { throw new NotImplementedException(); }
        //Nazar
        public void CommentPoll(int PollId, Comment comment) { throw new NotImplementedException(); }
        //Vlad
        public EditablePoll GetEditCopy(int PollId) { throw new NotImplementedException(); }
        //Vlad
        public EditableQuestion GetEditQuestion(int QuestionId) { throw new NotImplementedException(); }
        //Nazar
        public void PassPoll(int PollId) { throw new NotImplementedException(); }
    }
}
