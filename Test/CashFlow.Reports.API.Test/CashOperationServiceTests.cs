using Moq;
using Microsoft.Extensions.Logging;
using CashFlow.Payment.API.Domain.Services;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Interfaces;
using CashFlow.Reports.API.Domain.DTOs;
using MongoDB.Driver.Linq;
using System.Xml;

namespace CashFlow.Payment.API.Tests.Domain.Services
{
    [TestClass]
    public class CashOperationServiceTests
    {
        private CashOperationService _service;
        private Mock<ICashOperationRepository> _mockRepository;
        private Mock<ILogger<CashOperationService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<ICashOperationRepository>();
            _mockLogger = new Mock<ILogger<CashOperationService>>();
            _service = new CashOperationService(_mockRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void GetConsolidateByDay_ShouldReturnListOfConsolidateDay()
        {
            // Arrange
            var consolidateDays = new List<ConsolidateDay>
            {
                new ConsolidateDay { Id = Guid.NewGuid().ToString(), Date = DateTime.UtcNow, Value = 100 },
                new ConsolidateDay { Id = Guid.NewGuid().ToString(), Date = DateTime.UtcNow.AddDays(-1), Value = 200 }
            };
            _mockRepository.Setup(r => r.Get()).Returns(consolidateDays.AsQueryable());

            // Act
            var result = _service.GetConsolidateByDay();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(100, result[0].Value);
        }

        [TestMethod]
        public async Task SetOperation_ShouldUpdateOperation_WhenOperationExistsForSameDay()
        {
            // Arrange
            var operation = new CashOperation { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.UtcNow, Value = 50, Type = 1 };
            var existingDay = new ConsolidateDay { Id = Guid.NewGuid().ToString(), Date = DateTime.UtcNow, Value = 100 };

            _mockRepository.Setup(r => r.Get()).Returns(new List<ConsolidateDay> { existingDay }.AsQueryable());
            _mockRepository.Setup(r => r.UpdateAsync(existingDay.Id, It.IsAny<ConsolidateDay>())).ReturnsAsync(1);

            // Act
            var result = await _service.SetOperation(operation);

            // Assert
            Assert.AreEqual(1, result);
            _mockRepository.Verify(r => r.UpdateAsync(existingDay.Id, It.Is<ConsolidateDay>(d => d.Value == 150)), Times.Once);
        }

        [TestMethod]
        public async Task SetOperation_ShouldInsertNewOperation_WhenNoExistingOperationForSameDay()
        {
            // Arrange
            var operation = new CashOperation { Id = Guid.NewGuid().ToString(), CreatedAt = DateTime.UtcNow, Value = 50, Type = 1 };

            _mockRepository.Setup(r => r.Get()).Returns(new List<ConsolidateDay>().AsQueryable());
            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<ConsolidateDay>())).ReturnsAsync(1);

            // Act
            var result = await _service.SetOperation(operation);

            // Assert
            Assert.AreEqual(1, result);
            _mockRepository.Verify(r => r.InsertAsync(It.Is<ConsolidateDay>(d => d.Value == 50 && d.Date == operation.CreatedAt)), Times.Once);
        }
    }
}
