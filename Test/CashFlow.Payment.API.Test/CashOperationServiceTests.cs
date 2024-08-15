using CashFlow.Payment.API.Domain.Entities;
using CashFlow.Payment.API.Domain.Services;
using CashFlow.Payment.API.Infrastructure.Database.Repositories.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace CashFlow.Payment.API.Tests.Domain.Services
{
    [TestClass]
    public class CashOperationServiceTests
    {
        private CashOperationService _service;
        private Mock<ICashOperationRepository> _mockRepository;
        private Mock<ICashOperationReportPublisher> _mockPublisher;
        private Mock<ILogger<CashOperationService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ICashOperationRepository>();
            _mockPublisher = new Mock<ICashOperationReportPublisher>();
            _mockLogger = new Mock<ILogger<CashOperationService>>();
            _service = new CashOperationService(_mockRepository.Object, _mockPublisher.Object, _mockLogger.Object);

            // Mocking EnsureCreated method in publisher
            _mockPublisher.Setup(p => p.EnsureCreated()).Verifiable();
        }

        [TestMethod]
        public async Task AddCredit_ShouldReturnCashOperationEntity_WhenSuccess()
        {
            // Arrange
            var cashOperation = new CashOperationEntity();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<CashOperationEntity>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddCredit(cashOperation);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Type);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<CashOperationEntity>()), Times.Once);
            _mockPublisher.Verify(p => p.SendMessage(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddCredit_ShouldReturnNull_WhenExceptionThrown()
        {
            // Arrange
            var cashOperation = new CashOperationEntity();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<CashOperationEntity>()))
                           .Throws(new Exception("Repository error"));

            // Act
            var result = await _service.AddCredit(cashOperation);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddDebit_ShouldReturnCashOperationEntity_WhenSuccess()
        {
            // Arrange
            var cashOperation = new CashOperationEntity();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<CashOperationEntity>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddDebit(cashOperation);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Type);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<CashOperationEntity>()), Times.Once);
            _mockPublisher.Verify(p => p.SendMessage(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddDebit_ShouldReturnNull_WhenExceptionThrown()
        {
            // Arrange
            var cashOperation = new CashOperationEntity();
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<CashOperationEntity>()))
                           .Throws(new Exception("Repository error"));

            // Act
            var result = await _service.AddDebit(cashOperation);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnListOfCashOperationEntities()
        {
            // Arrange
            var operations = new List<CashOperationEntity> { new CashOperationEntity(), new CashOperationEntity() };
            _mockRepository.Setup(r => r.GetAllAsync())
                           .ReturnsAsync(operations);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
