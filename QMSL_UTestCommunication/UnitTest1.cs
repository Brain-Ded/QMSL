using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using QMSL;
using QMSL.Controllers;
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
        [TestCase("InvalidEmail", "test")]
        [TestCase("patient@gmail.com", "InvalidPollName")]
        public void AssignPoll_InvalidPatientEmailOrPollName_ReturnsBadRequest(string patientEmail, string pollName)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock1 = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock1.Object);

            PollController_AssUnassCommPass controller = new PollController_AssUnassCommPass(testMockDb.Object, null);

            var test = controller.AssignPoll(pollName, patientEmail);

            Assert.That(test.Result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void UnassignPoll_ValidPatientIdAndPollId_RemovesPollFromList()
        {
            _patient?.Polls?.Add(_editablePoll);
            _pollsService.UnassignPoll(_patient, _editablePoll.Id);
            Assert.That(_patient.Polls, Is.Not.Contains(_editablePoll));
        }

        [Test]
        [TestCase("InvalidPatientEmail", "test")]
        [TestCase("patient@gmail.com", "InvalidPollName")]
        public void UnassignPoll_InvalidPatientEmailOrPollName_ThrowsArgumentOutOfRangeException(string patientEmail, string pollName)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock1 = MockService.GetEditablePolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock1.Object);

            PollController_AssUnassCommPass controller = new PollController_AssUnassCommPass(testMockDb.Object, null);

            var test = controller.UnassignPoll(pollName, patientEmail);

            Assert.That(test.Result.Result, Is.TypeOf<BadRequestObjectResult>());
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
        public void CommentPoll_InvalidPollId_ReturnsBadRequest(int pollId)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetEditablePolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock.Object);

            PollController_AssUnassCommPass controller = new PollController_AssUnassCommPass(testMockDb.Object, null);

            var test = controller.CommentPoll(pollId, new QMSL.Dtos.CommentDto());

            Assert.IsTrue(test.Result.Result is BadRequestObjectResult);
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