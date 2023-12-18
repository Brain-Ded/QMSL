using QMSL.Models;
using QMSL.Services;

namespace QMSL_UTestPolls
{
    public class Tests
    {
        private PollsService pollInstance;
        [SetUp]
        public void Setup()
        {
            pollInstance = new PollsService();
        }

        [Test]
        [TestCase(1)]
        public void GetEditCopy_PositiveTest(int id)
        {
            GeneralPoll poll = new GeneralPoll()
            {
                Id = id,
                Name = "Test",
            };
            var test = poll.getEditCopy();
            Assert.IsNotNull(test);
        }
        [Test]
        [TestCase(1)]
        public void GetEditQuestion_PositiveTest(int id)
        {
            GeneralQuestion question = new GeneralQuestion()
            {
                Id = id,
                GeneralAnswers = new List<GeneralAnswer>(),
                Name = "Test",
            };
            var test = question.getEditCopy();
            Assert.IsNotNull(test);
        }
        public void PassPoll_PositiveTest()
        {
            int validPollId = 1;
            EditablePoll poll = new EditablePoll() { Id = validPollId };
            Assert.DoesNotThrow(() =>
            {
                pollInstance.PassPoll(poll);
            });
        }

        [Test]
        public void PassPoll()
        {
            EditablePoll editablePoll = new EditablePoll();
            pollInstance.PassPoll(editablePoll);

            Assert.IsTrue(editablePoll.IsPassed);
        }
    }
}
