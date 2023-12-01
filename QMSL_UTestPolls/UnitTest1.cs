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
        public void GetEditCopy_PositiveTest()
        {
            
            int validPollId = 1; 
            Assert.DoesNotThrow(() =>
            {
                var result = pollInstance.GetEditCopy(validPollId);
                Assert.IsNotNull(result);
                
            });
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
        public void GetEditQuestion_PositiveTest()
        {
            
            int validQuestionId = 1; 
            Assert.DoesNotThrow(() =>
            {
                var result = pollInstance.GetEditQuestion(validQuestionId);
                Assert.IsNotNull(result);
                
            });
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
            Assert.DoesNotThrow(() =>
            {
                pollInstance.PassPoll(validPollId);
            });
        }

        [Test]
        public void PassPoll_NegativeTest()
        {
            int invalidPollId = -1; 
            Assert.Throws<NotImplementedException>(() =>
            {
                pollInstance.PassPoll(invalidPollId);
            });
        }
    }
}
