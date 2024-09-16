using ApiClienteXp.API.Controllers;
using ApiClienteXp.Domain.Domain.Models;
using ApiClienteXp.Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiClienteXpxUnitTests.UnitTests
{
    public class ClientesUnitTestController
    {
        private readonly Mock<IClienteRepository> _mockRepo;
        private readonly ClientesController _controller;

        public ClientesUnitTestController()
        {
            _mockRepo = new Mock<IClienteRepository>();
            _controller = new ClientesController(_mockRepo.Object);
        }

        //################################## GET METHODS #######################################

        [Fact]
        public async Task Get_ReturnsOkResult_WhenClienteExists_ById()
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
        public async Task Get_ReturnsOkResult_WhenClienteExists_ByName()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" }
            };

            _mockRepo.Setup(repo => repo.GetClientesPorNomeAsync("Joao")).ReturnsAsync(clientes);

            // Act
            var result = await _controller.GetClientesNome("Joao");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Cliente>>(okResult.Value);
            Assert.Equal("Joao", returnValue.First().Nome);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenClienteNotExists_ByName()
        {
            // Arrange
            var clientes = new List<Cliente>();

            _mockRepo.Setup(repo => repo.GetClientesPorNomeAsync("Joao")).ReturnsAsync(clientes);

            // Act
            var result = await _controller.GetClientesNome("Joao");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenClienteDoesNotExist_ById()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Get(c => c.ClienteId == 1)).ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        //############################### DELETE METHODS ####################################


    }
}
