using Moq;
using CashFlow.Reports.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CashFlow.Payment.API.Domain.Services.Interfaces;
using CashFlow.Reports.API.Domain.DTOs;

namespace CashFlow.Reports.API.Tests.Controllers
{
    [TestClass]
    public class ReportControllerTests
    {
        private ReportController _controller;
        private Mock<ICashOperationService> _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICashOperationService>();
            _controller = new ReportController(_mockService.Object);
        }

        [TestMethod]
        public async Task GetReport_ShouldReturnOkResultWithConsolidatedReport()
        {
            // Arrange
            var reportData = new List<ConsolidateDay>(); // Replace with actual report data type
            _mockService.Setup(s => s.GetConsolidateByDay()).Returns(reportData);

            // Act
            var result = await _controller.GetReport();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(reportData, okResult.Value);
        }
    }
}