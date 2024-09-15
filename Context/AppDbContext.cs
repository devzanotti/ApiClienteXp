using ApiClienteXp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options )
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
