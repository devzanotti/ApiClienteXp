using ApiClienteXp.Context;
using ApiClienteXp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Controllers
{
    //Metodos retornando todos os registros sem filtros por ser um Case com poucos registros
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
            try
            {
                var clientes = _context.Clientes.AsNoTracking().ToList();
                if (clientes is null)
                {
                    return NotFound("Clientes nao encontrados");
                }
                return clientes;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitacao.");
            }

        }

        [HttpGet("{id}", Name ="ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c=> c.ClienteId == id);
            if(cliente == null)
            {
                return NotFound($"Cliente com id= {id} nao localizado");
            }
            return cliente;
        }

        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            if(cliente == null)
            {
                return BadRequest("Dados Invalidos");
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
                return BadRequest("Dados Invalidos");
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
                return NotFound($"Cliente com id= {id} nao localizado");
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }


    }
}
