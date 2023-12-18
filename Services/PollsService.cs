using Microsoft.IdentityModel.Tokens;
using QMSL.Models;

namespace QMSL.Services
{
    public class PollsService
    {
        public GeneralPoll CreatePoll(string name, List<GeneralQuestion> questions)
        {
            if (!string.IsNullOrEmpty(name) && questions?.Count > 0)
                return new GeneralPoll() { Name = name, Questions = questions };

            throw new ArgumentNullException();
        }
        public GeneralPoll EditPoll(int id, GeneralPoll edited)
        {
            throw new NotImplementedException();
        }
        public Patient AssignPoll(Patient patient, EditablePoll poll) 
        {
            if(patient == null || poll == null)
                throw new ArgumentNullException();

            poll.AssignedAt = DateTime.Now;

            poll.PatientId = patient.Id;
            patient.Polls.Add(poll);
            return patient;
        }
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
        public EditablePoll CommentPoll(EditablePoll poll, Comment comment) 
        { 
            if(poll == null || comment == null)
                throw new ArgumentNullException();

            if (comment.Text.IsNullOrEmpty())
                throw new ArgumentException();

            if(poll.Comments == null)
                poll.Comments = new List<Comment>();

            poll.Comments.Add(comment);
            comment.EditablePollId = poll.Id;
            comment.CommentedAt = DateTime.Now;

            return poll;
        }  
        public EditablePoll PassPoll(EditablePoll poll) 
        {
            if (poll == null) throw new ArgumentNullException();

            poll.PassedAt = DateTime.Now;
            poll.IsPassed = true;
            return poll;
        }
    }
}
