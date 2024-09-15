using ApiClienteXp.Context;
using ApiClienteXp.Models;
using ApiClienteXp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
       

        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetClientesPorNomeAsync(string nome)
        {
            var clientes = await GetAllAsync();
            var clientesRecebidos = clientes.Where(c => c.Nome == nome);
            return clientesRecebidos;   

        }

    }
}
