using Moq;
using CashFlow.Payment.API.Controllers;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CashFlow.Payment.API.Application.CashOperation.Commands.Requests;
using CashFlow.Payment.API.Domain.Services.Interfaces;
using CashFlow.Payment.API.Application.CashOperation.Commands.Responses.Common;
using CashFlow.Payment.API.Domain.Entities;

namespace CashFlow.Payment.API.Tests.Controllers
{
    [TestClass]
    public class CashOperationControllerTests
    {
        private CashOperationController _controller;
        private Mock<ILogger<CashOperationController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private Mock<ICashOperationService> _mockService;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<CashOperationController>>();
            _mockMediator = new Mock<IMediator>();
            _mockService = new Mock<ICashOperationService>();
            _controller = new CashOperationController(_mockLogger.Object, _mockMediator.Object, _mockService.Object);
        }

        [TestMethod]
        public async Task Credit_ShouldReturnOkResult()
        {
            // Arrange
            var request = new CreditCashOperationCommandsRequest();
            var expectedResponse = new CashOperationCommandResponse() { Type = 1, Description = "teste", Value = 25.96M };
            _mockMediator.Setup(m => m.Send(request, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Credit(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedResponse, okResult.Value);
        }

        [TestMethod]
        public async Task Debit_ShouldReturnOkResult()
        {
            // Arrange
            var request = new DebitCashOperationCommandsRequest();
            var expectedResponse = new CashOperationCommandResponse() { Type=0, Description="teste", Value=25.96M  }; 
            _mockMediator.Setup(m => m.Send(request, default)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Debit(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedResponse, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOkResultWithListOfOperations()
        {
            // Arrange
            var operations = new List<CashOperationEntity>();
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(operations);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = (result as OkObjectResult).Value as List<CashOperationEntity>;
            Assert.IsNotNull(okResult);
         }
    }
}