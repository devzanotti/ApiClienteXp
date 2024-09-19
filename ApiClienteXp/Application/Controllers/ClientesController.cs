using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ApiClienteXp.Domain.Domain.Models;
using ApiClienteXp.API.Filters;
using ApiClienteXp.Infraestructure.Repositories.Interfaces;

namespace ApiClienteXp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiVersion("1.0")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet("clientes/{nome}")]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesNome(string nome)
        {
            var clientes = await _clienteRepository.GetClientesPorNomeAsync(nome);

            if (clientes is null || !clientes.Any())
            {
                return NotFound();
            }

            return Ok(clientes);

        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCliente")]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _clienteRepository.Get(c => c.ClienteId == id);
            if (cliente is null)
            {
                return NotFound($"Cliente com id= {id} nao localizado");
            }
            return Ok(cliente);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest("Dados Invalidos");
            }

            var clienteCriado = _clienteRepository.Create(cliente);

            return new CreatedAtRouteResult("ObterCliente",
                new { id = clienteCriado.ClienteId }, clienteCriado);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest("Dados Invalidos");
            }

            _clienteRepository.Update(cliente);
            return Ok(cliente);

        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clienteRepository.Get(c => c.ClienteId == id);
            if (cliente == null)
            {
                return NotFound($"Cliente com id= {id} nao localizado");
            }

            var clienteExcluido = _clienteRepository.Delete(cliente);

            return Ok(clienteExcluido);
        }
    }
}