using ApiClienteXp.Context;
using ApiClienteXp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController :ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clientes =_context.Clientes.ToList();
            if(clientes is null)
            {
                return NotFound("Clientes nao encontrados");
            }
            return clientes;
        }

        [HttpGet("{id}", Name ="ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c=> c.ClienteId == id);
            if(cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            if(cliente == null)
            {
                return BadRequest();
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            
            return new CreatedAtRouteResult("ObterCliente",
                new { id = cliente.ClienteId }, cliente);
        }




        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            if(id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(cliente);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        { 
            var cliente = _context.Clientes.FirstOrDefault( c=> c.ClienteId == id);  
            if(cliente == null)
            { 
                return NotFound("Cliente nao localizado");
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }


    }
}
