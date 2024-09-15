using ApiClienteXp.Models;
using System.Runtime.InteropServices;

namespace ApiClienteXp.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> GetClientesPorNome(string nome);
    }
}
