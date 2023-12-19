
using ASHWorkersAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ASHWorkersAPI.Models;
namespace ASHWorkersAPI.Data;
using Moq;
using ASHWorkersAPI.Controllers.Requests;
using ASHWorkersAPI.Controllers.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkerController.Unit.Tests
{
    [TestClass]
    public class WorkerControllerTests
    {


        private WorkerController wcontroller;
        private Mock<ApiDbContext> apiDbContext;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {

            apiDbContext = new Mock<ApiDbContext>();
            logger = new Mock<ILogger>();
            wcontroller = new WorkerController(apiContext, logger);
        }


        [TestMethod]
        public void GetAllWorkers()
        {
            // Arrange
            // Todo: create workers data type (e.g workers + employee) 
            var workerList = new List<WorkerResponse>
            {
                new WorkerResponse
                {
                    WorkerId = 1,
                    FirstName = "Matt",
                    LastName = "Henry",
                    Address = "Morristown",
                    AnnualSalary = 100000,
                    MaxExpenseAmount = 1000
                },
                new WorkerResponse
                {
                    WorkerId = 2,
                    FirstName = "Trent",
                    LastName = "Williams",
                    Address = "San Diego",
                    AnnualSalary = 70000

                },
                new WorkerResponse
                {
                    WorkerId = 3,
                    FirstName = "Kane",
                    LastName = "Dasher",
                    Address = "Test Town",
                    PayPerHour = 60

                }
            };

            apiContext.Setup(p => p.Managers()).Returns(new WorkerResponse {
                    FirstName = "Matt",
                    LastName = "Henry",
                    Address = "Morristown",
                    AnnualSalary = 100000,
                    MaxExpenseAmount = 1000});
            apiContext.Setup(p => p.Supervisors()).Returns(new WorkerResponse
            {
                FirstName = "Trent",
                LastName = "Williams",
                Address = "San Diego",
                AnnualSalary = 70000
            });
            apiContext.Setup(p => p.Employee()).Returns(new WorkerResponse
            {
                FirstName = "Kane",
                LastName = "Dasher",
                Address = "Test Town",
                PayPerHour = 60
            });
            var expectedResult = new JsonResult(workerList);
            //Act

            

            var result = wcontroller.GetAllWorkers();

            //Assert

            Assert.Equals(result == expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Some Exception.")]
        public void GetAllWorkersThrowsException()
        {
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());

            // Act
            var result = wcontroller.GetAllWorkers();

        }


        [TestMethod]
        public void AddManagerWorker()
        {
            var worker = new WorkerRequest
            {
                FirstName= "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "manager", maxExpenseAllowed=5000.50, annualSalary=50000);

            //Assert
            Assert.Equals(result, 1);

        }

        [TestMethod]
        public void AddSupervisorWorker()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "supervisor", annualSalary = 50000);

            //Assert
            Assert.Equals(result, 1);

        }

        [TestMethod]
        public void AddEmployeeWorker()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "employee", payPerHour = 50.50);

            //Assert
            Assert.Equals(result, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Incorrect arguments")]
        public void AddManagerWorkerThrowsException()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "manager", annualSalary = 50000);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Incorrect arguments")]
        public void AddSupervisorWorkerThrowsException()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "supervisor", payPerHour = 50);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Incorrect arguments")]
        public void AddEmployeeWorkerThrowsException()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Managers()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns(null);

            // Act
            var result = wcontroller.AddWorker(worker, "employee", annualSalary = 50000);

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Incorrect arguments")]
        public void AddWorkerThrowsExceptionWhileSaving()
        {
            var worker = new WorkerRequest
            {
                FirstName = "Adam",
                LastName = "Black",
                Address = "1342 Pine Court, Los Angeles, CA"
            };
            // Arrange
            apiDbContext.Setup(p => p.Manager()).Returns(new Exception());
            apiDbContext.SetupSequence(x => x.AddObject())
            .Returns(1)
            .Returns( new Exception());

            // Act
            var result = wcontroller.AddWorker(worker, "employee", payPerHour = 50000);

        }
    }
}
