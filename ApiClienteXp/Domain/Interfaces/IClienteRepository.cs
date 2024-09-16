using ApiClienteXp.Domain.Domain.Models;
using ApiClienteXp.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace ApiClienteXp.Domain.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesPorNomeAsync(string nome);
    }
}
