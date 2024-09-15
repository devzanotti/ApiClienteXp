using ApiClienteXp.Context;
using ApiClienteXp.Filters;
using ApiClienteXp.Models;
using ApiClienteXp.Repositories;
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
        private readonly IClienteRepository _repository;
        private readonly ILogger _logger;

        public ClientesController(IClienteRepository repository, ILogger<ClientesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Usando Logging apenas nessa para demonstrar o uso, nao vejo necessidade de usar nessa api pequena
        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clientes = _repository.GetClientes();
            return Ok(clientes);
        }

        //Restricao para nao atender requisicoes invalidas (id menor que 1)
        [HttpGet("{id:int:min(1)}", Name ="ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente =  _repository.GetCliente(id);
            if(cliente is null)
            {
                return NotFound($"Cliente com id= {id} nao localizado");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            if(cliente is null)
            {
                return BadRequest("Dados Invalidos");
            }

            var clienteCriado = _repository.Create(cliente);
            
            return new CreatedAtRouteResult("ObterCliente",
                new { id = clienteCriado.ClienteId }, clienteCriado);
        }




        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            if(id != cliente.ClienteId)
            {
                return BadRequest("Dados Invalidos");
            }

            _repository.Update(cliente);
            return Ok(cliente);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        { 
            var cliente = _repository.GetCliente(id);  
            if(cliente == null)
            { 
                return NotFound($"Cliente com id= {id} nao localizado");
            }

            var clienteExcluido = _repository.Delete(id);
       
            return Ok(clienteExcluido);
        }


    }
}
