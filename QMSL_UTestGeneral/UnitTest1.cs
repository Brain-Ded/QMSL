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
        [Test]
        [TestCase(1)]
        public void GetPatientPollStatistic_ValidId(int id)
        {
            var testMockDb = new Mock<DataContext>();
            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x=>x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetEditablePollsForPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock2.Object);

            testMockDb.Object.Patients.First(x => x.Id == id).Polls = testMockDb.Object.EditablePolls.ToList();

            StatisticController controller = new StatisticController(testMockDb.Object, null);

            var stats = controller.GetPatientPollStatistic(id);
;
            Assert.IsTrue(stats.Result.Result is OkObjectResult);

        }
        [Test]
        [TestCase(1, "09.08.2022", "10.10.2024")]
        public void GetPatientPollStatisticFromTo_ValidId(int id, DateTime from, DateTime to)
        {
            var testMockDb = new Mock<DataContext>();
            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetEditablePollsForPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock2.Object);

            testMockDb.Object.Patients.First(x => x.Id == id).Polls = testMockDb.Object.EditablePolls.ToList();

            StatisticController controller = new StatisticController(testMockDb.Object, null);

            var stats = controller.GetPatientPollStatisticAt(id, from, to);
            ;
            Assert.IsTrue(stats.Result.Result is OkObjectResult);

        }
        [Test]
        [TestCase(1)]
        public void GetDoctorStatistic_ValidId(int id)
        {
            var testMockDb = new Mock<DataContext>();
            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetEditablePollsForPatients2().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock2.Object);

            var mock3 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Doctors).Returns(mock3.Object);

            var mock4 = MockService.GetGeneralPollsForDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock4.Object);

            testMockDb.Object.Patients.First(x => x.Id == 1).Polls = testMockDb.Object.EditablePolls.Where(y => y.Id == 1).ToList();
            testMockDb.Object.Patients.First(x => x.Id == 2).Polls = testMockDb.Object.EditablePolls.Where(y => y.Id == 2).ToList();

            testMockDb.Object.Doctors.First(x => x.Id == id).Patients = testMockDb.Object.Patients.Where(y => y.Id == 1).ToList();
            testMockDb.Object.Doctors.First(x => x.Id == id).Polls = testMockDb.Object.GeneralPolls.ToList();

            StatisticController controller = new StatisticController(testMockDb.Object, null);

            var stats = controller.GetDoctorStatistic(id);
            ;
            Assert.IsTrue(stats.Result.Result is OkObjectResult);

        }
        [Test]
        [TestCase(1, "09.08.2022", "10.10.2024")]
        public void GetDoctorStatisticAt_Valid(int id, DateTime from, DateTime to)
        {
            var testMockDb = new Mock<DataContext>();
            var mock = MockService.GetMockPatients().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Patients).Returns(mock.Object);

            var mock2 = MockService.GetEditablePollsForPatients2().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.EditablePolls).Returns(mock2.Object);

            var mock3 = MockService.GetMockDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.Doctors).Returns(mock3.Object);

            var mock4 = MockService.GetGeneralPollsForDoctors().BuildMock().BuildMockDbSet();
            testMockDb.Setup(x => x.GeneralPolls).Returns(mock4.Object);

            testMockDb.Object.Patients.First(x => x.Id == 1).Polls = testMockDb.Object.EditablePolls.Where(y => y.Id == 1).ToList();
            testMockDb.Object.Patients.First(x => x.Id == 2).Polls = testMockDb.Object.EditablePolls.Where(y => y.Id == 2).ToList();

            testMockDb.Object.Doctors.First(x => x.Id == id).Patients = testMockDb.Object.Patients.Where(y => y.Id == 1).ToList();
            testMockDb.Object.Doctors.First(x => x.Id == id).Polls = testMockDb.Object.GeneralPolls.ToList();

            StatisticController controller = new StatisticController(testMockDb.Object, null);

            var stats = controller.GetDoctorStatisticAt(id, from, to);
            ;
            Assert.IsTrue(stats.Result.Result is OkObjectResult);

        }
    }
}