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
        public void GetEditCopy_NegativeTest()
        {
            
            int invalidPollId = -1; 
            Assert.Throws<NotImplementedException>(() =>
            {
                var result = pollInstance.GetEditCopy(invalidPollId);
            });
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

        [Test]
        public void GetEditQuestion_NegativeTest()
        {
            
            int invalidQuestionId = -1; 
            Assert.Throws<NotImplementedException>(() =>
            {
                var result = pollInstance.GetEditQuestion(invalidQuestionId);
            });
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
