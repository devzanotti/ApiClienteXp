using ApiClienteXp.Models;
using System.Runtime.InteropServices;

namespace ApiClienteXp.Repositories
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetClientes();
        Cliente GetCliente(int id);
        Cliente Create(Cliente cliente);
        Cliente Update(Cliente cliente);
        Cliente Delete(int id);
    }
}
