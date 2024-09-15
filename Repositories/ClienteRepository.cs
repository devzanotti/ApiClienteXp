using ApiClienteXp.Context;
using ApiClienteXp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _context.Clientes.ToList();
        }

        public Cliente GetCliente(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
        }

        public Cliente Create(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();
            return cliente;
        }
        public Cliente Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return cliente;
        }
    }
}
