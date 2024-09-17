using ApiClienteXp.API.Controllers;
using ApiClienteXp.Domain.Domain.Models;
using ApiClienteXp.Infraestructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

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
        public async Task Get_ReturnsOkResult_QuandoClienteExiste_PorId()
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
        public async Task Get_ReturnsOkResult_QuandoClienteExiste_PorNome()
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
        public async Task Get_ReturnsOkResult_QuandoClienteNaoExiste_PorNome()
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
        public async Task Get_ReturnsNotFound_QuandoClienteNaoExiste_PorId()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Get(c => c.ClienteId == 1)).ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_ComListaDeClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" },
                new Cliente { ClienteId = 2, Nome = "Maria", Cpf = "09876543210", Email = "maria@exemplo.com" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clientes);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Cliente>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        //############################### POST METHODS ####################################

        [Fact]
        public async Task Post_ReturnsBadRequest_QuandoCliente_IsNull()
        {
            // Arrange
            Cliente cliente = null;

            // Act
            var result = await _controller.Post(cliente);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Dados Invalidos", badRequestResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtRoute_QuandoCliente_IsCreated()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" };

            _mockRepo.Setup(repo => repo.Create(cliente)).Returns(cliente);

            // Act
            var result = await _controller.Post(cliente);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("ObterCliente", createdAtRouteResult.RouteName);
            Assert.Equal(1, ((Cliente)createdAtRouteResult.Value).ClienteId);
        }

        //############################### PUT METHODS ####################################

        [Fact]
        public async Task Put_ReturnsBadRequest_QuandoIdDiferene()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" };
            int idDiferente = 2;

            // Act
            var result = await _controller.Put(idDiferente, cliente);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dados Invalidos", badRequestResult.Value);
        }

        [Fact]
        public async Task Put_ReturnsOk_QuandoCliente_Ok()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" };

            _mockRepo.Setup(repo => repo.Update(cliente)).Returns(cliente);

            // Act
            var result = await _controller.Put(cliente.ClienteId, cliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedCliente = Assert.IsType<Cliente>(okResult.Value);
            Assert.Equal(cliente.ClienteId, updatedCliente.ClienteId);
        }

        //############################### DELETE METHODS ####################################

        [Fact]
        public async Task Delete_ReturnsNotFound_QuandoClienteNaoExiste()
        {
            // Arrange
            int clienteId = 1;
            _mockRepo.Setup(repo => repo.Get(c => c.ClienteId == clienteId)).ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.Delete(clienteId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Cliente com id= {clienteId} nao localizado", notFoundResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_QuandoCliente_IsDeleted()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Nome = "Joao", Cpf = "12345678910", Email = "joao@exemplo.com" };

            _mockRepo.Setup(repo => repo.Get(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(cliente);
            _mockRepo.Setup(repo => repo.Delete(It.IsAny<Cliente>())).Returns(cliente);

            // Act
            var result = await _controller.Delete(cliente.ClienteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedCliente = Assert.IsType<Cliente>(okResult.Value);
            Assert.Equal(cliente.ClienteId, deletedCliente.ClienteId);
        }
    }
}
