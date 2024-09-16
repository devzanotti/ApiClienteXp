using ApiClienteXp.Domain.Domain.Models;
using System.Runtime.InteropServices;

namespace ApiClienteXp.Infraestructure.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesPorNomeAsync(string nome);
    }
}
