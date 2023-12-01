using QMSL.Models;
using QMSL.Services;

namespace QMSL_UTestGeneral
{
    public class GeneralTests
    {
        PollsService _PollsService;
        GeneralQuestion _GeneralQuestion;
        GeneralPoll _GeneralPoll;

        [SetUp]
        public void Setup()
        {
            _PollsService = new PollsService();
            _GeneralQuestion = new GeneralQuestion() { Id = 1 };
            _GeneralPoll = new GeneralPoll() { Id = 1 };
        }

        [Test]
        public void CreatePoll_ValidNameAndQuestionsList_CreatesPoll()
        {
            var poll = _PollsService.CreatePoll("TestName", new List<GeneralQuestion>() { new GeneralQuestion(), new GeneralQuestion() });
            Assert.That(poll, Is.Not.Null);
        }

        [Test]
        [TestCase(null)]
        [TestCase("TestName")]
        public void CreatePoll_InvalidNameOrQuestionListIsEmptyOrNull_ThrowsArgumentNullException(string name)
        {
            List<GeneralQuestion> questions = null;
            if(name == null)
                questions = new List<GeneralQuestion>() { new GeneralQuestion() };

            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Property("Questions").Not.Contain(_GeneralQuestion),
                () => _PollsService.CreatePoll(name, questions));
        }

        [Test]
        public void EditPoll_ValidIdAndEditedPoll_EditsPoll()
        {
            GeneralPoll poll = new GeneralPoll() { Id = 2, Name = "TEST" };
            var result = _PollsService.EditPoll(_GeneralPoll.Id, poll);
            Assert.That(result, Is.Not.Null.And.Property("Name").Not.Contain(_GeneralPoll)
                .Or.Property("Questions").Not.Contain(_GeneralPoll));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        public void EditPoll_InvalidIdOrEditedPollIsNull_ThrowsArgumentNullException(int id)
        {
            GeneralPoll poll = null;
            if (id == -1)
                poll = new GeneralPoll();

            Assert.Throws(Is.TypeOf<ArgumentNullException>(),
                () => _PollsService.EditPoll(id, poll));
        }

        [Test]
        public void DeletePoll_ValidId_DeletesPoll()
        {
            int id = 1;
            GeneralPoll poll = new GeneralPoll() { Id = id };

            _PollsService.DeletePoll(id);
            Assert.That(poll.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(-1)]
        public void DeletePoll_InvalidId_ThrowsArgumentException(int id)
        {
            _PollsService.DeletePoll(id);
            Assert.Throws(Is.TypeOf<ArgumentException>(),
                () => _PollsService.DeletePoll(id));
        }
    }
}