using QMSL.Enums;
using QMSL.Models;
using QMSL.Services;

namespace QMSL_UTestCommunication
{
    public class CommunicationTests
    {
        PollsService _pollsService;
        Patient _patient;
        EditablePoll _editablePoll;

        private Comment GetComment()
        {
            return new Comment() { Id = 1, DoctorId = 1, Text = "test", type = CommentTypes.Important };
        }

        public static IEnumerable<Comment> GetCommentsWithInvalidId
        {
            get
            {
                yield return new Comment() { Id = -1, DoctorId = 1, Text = "test", type = CommentTypes.Urgent };
                yield return new Comment() { Id = 1, DoctorId = -1, Text = "test", type = CommentTypes.Urgent };
            }
        }

        public static IEnumerable<Comment> GetCommentsWithEmptyMessage
        {
            get
            {
                yield return new Comment() { Id = 1, DoctorId = 1, Text = "", type = CommentTypes.Urgent };
                yield return new Comment() { Id = 1, DoctorId = 1, Text = null, type = CommentTypes.Urgent };
            }
        }

        [SetUp]
        public void Setup()
        {
            _patient = new Patient() { Id = 1, Polls = new List<EditablePoll>()};
            _editablePoll = new EditablePoll() { Id = 1 };
            _pollsService = new PollsService();

        }

        [Test]
        public void AssignPoll_ValidPatientIdAndPollId_AddsPollToList()
        {
            _pollsService.AssignPoll(_patient, _editablePoll);
            Assert.That(_patient.Polls, Is.Not.Empty.And.Contains(_editablePoll));
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void AssignPoll_InvalidPatientIdOrPollId_ThrowsArgumentOutOfRangeException(int patientId, int pollId)
        {
            Patient patient = new Patient() { Id = patientId };
            EditablePoll poll = new EditablePoll() { Id = pollId };

            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>()
                .And.Property("Polls").Not.Contain(_editablePoll),
            () => _pollsService.AssignPoll(patient, poll));
        }

        [Test]
        public void UnassignPoll_ValidPatientIdAndPollId_RemovesPollFromList()
        {
            _patient?.Polls?.Add(_editablePoll);
            _pollsService.UnassignPoll(_patient, _editablePoll.Id);
            Assert.That(_patient.Polls, Is.Not.Contains(_editablePoll));
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void UnassignPoll_InvalidPatientIdOrPollId_ThrowsArgumentOutOfRangeException(int patientId, int pollId)
        {
            Patient patient = new Patient() { Id = patientId };

            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
            () => _pollsService.UnassignPoll(patient, pollId));
        }

        [Test]
        public void CommentPoll_ValidData_AddsCommentToPoll()
        {
            var _comment = GetComment();
            _pollsService.CommentPoll(_editablePoll, _comment);
            Assert.That(_editablePoll.Comments, Is.Not.Empty.And.Contain(_comment));
            Assert.That(_comment.CommentedAt, Is.EqualTo(DateTime.Now).Within(1).Minutes);
        }

        [Test]
        [TestCase(-1)]
        public void CommentPoll_InvalidPollId_ThrowsArgumentOutOfRangeException(int pollId)
        {
            EditablePoll poll = new EditablePoll() { Id = pollId };

            var _comment = GetComment();
            Assert.Throws(Is.TypeOf<ArgumentOutOfRangeException>(),
            () => _pollsService.CommentPoll(poll, _comment));
        }

        [Test]
        [TestCaseSource(nameof(GetCommentsWithEmptyMessage))]
        public void CommentPoll_EmptyCommentMessage_ThrowsArgumentException(Comment comment)
        {
            var _comment = GetComment();
            Assert.Throws(Is.TypeOf<ArgumentException>(),
            () => _pollsService.CommentPoll(_editablePoll, comment));
        }

    }
}