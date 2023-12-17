using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using QMSL.Controllers;
using QMSL;
using QMSL.Models;
using QMSL.Services;
using NuGet.Frameworks;
using Microsoft.EntityFrameworkCore;
using QMSL.Dtos;

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
        [TestCase("test", "doctor@gmail.com")]
        [TestCase("ValidPollName", "InvalidDoctorEmail")]
        public void CreatePoll_InvalidNameOrDoctorEmail_ReturnsBadRequest(string name, string doctorEmail)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock.Object);

            var mock1 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Doctors).Returns(mock1.Object);

            PollController_CreateEditDel controller = new PollController_CreateEditDel(testMockDb.Object, null);

            PollDto testPoll = new PollDto() { Name = name, DoctorEmail = doctorEmail };
            var test = controller.CreatePoll(testPoll);

            Assert.That(test.Result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void EditPoll_ValidIdAndEditedPoll_EditsPoll()
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock.Object);
            
            var mock1 = MockService.GetGeneralQuestions().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralQuestions).Returns(mock1.Object);

            var mock2 = MockService.GetGeneralAnswers().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralAnswers).Returns(mock2.Object);

            PollController_CreateEditDel controller = new PollController_CreateEditDel(testMockDb.Object, null);

            var testPoll = MockService.GetGeneralPolls()[0];
            testPoll.Questions.Add(MockService.GetGeneralQuestions().First());
            var test = controller.EditPoll(testPoll);

            Assert.That(test.Result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase(-1)]
        public void EditPoll_InvalidIdOrEditedPollIsNull_FailsThread(int id)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock.Object);

            PollController_CreateEditDel controller = new PollController_CreateEditDel(testMockDb.Object, null);

            var test = controller.EditPoll(new GeneralPoll { Id = id });

            Assert.IsTrue(test.Status == TaskStatus.Faulted);
        }

        [Test]
        [TestCase(1)]
        public void DeletePoll_ValidId_DeletesPoll(int id)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock.Object);

            PollController_CreateEditDel controller = new PollController_CreateEditDel(testMockDb.Object, null);

            var test = controller.DeletePoll(id);

            Assert.That(test.Result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase(-1)]
        public void DeletePoll_InvalidId_FailsThread(int id)
        {
            var testMockDb = new Mock<DataContext>();

            var mock = MockService.GetGeneralPolls().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock.Object);

            PollController_CreateEditDel controller = new PollController_CreateEditDel(testMockDb.Object, null);

            var test = controller.DeletePoll(id);

            Assert.IsTrue(test.Status == TaskStatus.Faulted);
        }
    }
}