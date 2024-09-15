using ApiClienteXp.Context;
using ApiClienteXp.Controllers;
using ApiClienteXp.Repositories;
using ApiClienteXp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClienteXpxUnitTests.UnitTests
{
    public class ClientesUnitTestController
    {
        public IClienteRepository repository;
        public ILogger _logger;
        public static DbContextOptions<AppDbContext> DbContextOptions { get; }

        public static string connectionString = "Server=localhost;DataBase=apiclientedb;UId=root;Pwd=root";

        static ClientesUnitTestController()
        {
            DbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public ClientesUnitTestController()
        {
            var context = new AppDbContext(DbContextOptions);
            repository=new ClienteRepository(context);
        }


    }
}
