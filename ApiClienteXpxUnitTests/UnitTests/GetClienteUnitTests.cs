using ApiClienteXp.Controllers;
using ApiClienteXp.Repositories.Interfaces;
using ApiClienteXp.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace ApiClienteXpxUnitTests.UnitTests
{
    public class GetClienteUnitTests : IClassFixture<ClientesUnitTestController>
    {
        private readonly ClientesController _controller;
        public GetClienteUnitTests(ClientesUnitTestController controller)
        {
            ILogger<ClientesController> logger = NullLogger<ClientesController>.Instance;
            _controller = new ClientesController(controller.repository, logger);
        }

        [Fact]
        public async Task GetClienteById_OkResult()
        {
            //Arrange
            var clienteId = 1;

            //Act
            var data = await _controller.Get(clienteId);

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);
        }


    }
}
