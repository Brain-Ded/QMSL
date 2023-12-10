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
        public Patient AssignPoll(Patient patient, EditablePoll poll) 
        {
            if(patient == null || poll == null)
                throw new ArgumentNullException();

            if (patient.Polls == null)
                patient.Polls = new List<EditablePoll>();

            patient.Polls.Add(poll);
            return patient;
        }
        //Nazar
        public Patient UnassignPoll(Patient patient, int pollId) 
        {
            if (patient == null || patient.Polls == null)
                throw new ArgumentNullException();

            var poll = patient.Polls.FirstOrDefault(p => p.Id == pollId);

            if (poll != null)
            {
                patient.Polls.Remove(poll);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return patient;
        }
        //Nazar
        public EditablePoll CommentPoll(EditablePoll poll, Comment comment) 
        { 
            if(poll == null || comment == null)
                throw new ArgumentNullException();

            if(poll.Comments == null)
                poll.Comments = new List<Comment>();

            poll.Comments.Add(comment);

            return poll;
        }
        //Vlad
        public EditablePoll GetEditCopy(int PollId) { throw new NotImplementedException(); }
        //Vlad
        public EditableQuestion GetEditQuestion(int QuestionId) { throw new NotImplementedException(); }
        //Nazar
        public EditablePoll PassPoll(EditablePoll poll) 
        {
            if (poll == null) throw new ArgumentNullException();

            poll.IsPassed = true;
            return poll;
        }
    }
}
