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

        public IEnumerable<Cliente> GetClientesPorNome(string nome)
        {
            return GetAll().Where(c => c.Nome == nome);
        }
    }
}
