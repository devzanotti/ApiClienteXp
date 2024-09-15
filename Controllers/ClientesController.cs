using ApiClienteXp.Context;
using ApiClienteXp.Filters;
using ApiClienteXp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiClienteXp.Controllers
{
    //Metodos retornando todos os registros sem filtros por ser um Case com poucos registros
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController :ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public ClientesController(AppDbContext context, ILogger<ClientesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Usando Logging apenas nessa para demonstrar o uso, nao vejo necessidade de usar nessa api pequena
        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            try
            {
                _logger.LogInformation("########### GET api/clientes #############");
                var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
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

        //Restricao para nao atender requisicoes invalidas (id menor que 1)
        [HttpGet("{id:int:min(1)}", Name ="ObterCliente")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c=> c.ClienteId == id);
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
