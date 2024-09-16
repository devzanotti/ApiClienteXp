using ApiClienteXp.Domain.Domain.Models;
using ApiClienteXp.Infraestructure.Context;
using ApiClienteXp.Infraestructure.Repositories.Interfaces;

namespace ApiClienteXp.Infraestructure.Repositories
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
