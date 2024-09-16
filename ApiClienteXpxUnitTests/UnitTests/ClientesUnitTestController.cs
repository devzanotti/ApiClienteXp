using ApiClienteXp.API.Controllers;
using ApiClienteXp.Context;
using ApiClienteXp.Domain.Interfaces;
using ApiClienteXp.Domain.Models;
using ApiClienteXp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClienteXpxUnitTests.UnitTests
{
    public class ClientesUnitTestController
    {
        private readonly Mock<IClienteRepository> _mockRepo;
        private readonly Mock<ILogger<ClientesController>> _mockLogger;
        private readonly ClientesController _controller;

        public ClientesUnitTestController()
        {
            _mockRepo = new Mock<IClienteRepository>();
            _mockLogger = new Mock<ILogger<ClientesController>>();
            _controller = new ClientesController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenClienteExists()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Nome = "João", Cpf = "12345678910", Email = "joao@exemplo.com" };
            _mockRepo.Setup(repo => repo.Get(c => c.ClienteId == 1)).ReturnsAsync(cliente);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Cliente>(okResult.Value);
            Assert.Equal(1, returnValue.ClienteId);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Get(c => c.ClienteId == 1)).ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
