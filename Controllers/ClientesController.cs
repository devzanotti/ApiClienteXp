﻿using ApiClienteXp.Context;
using ApiClienteXp.Filters;
using ApiClienteXp.Models;
using ApiClienteXp.Repositories.Interfaces;
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
        private readonly IClienteRepository _clienteRepository;
        //private readonly IRepository<Cliente> _repository;
        private readonly ILogger _logger;

        public ClientesController(IClienteRepository clienteRepository, ILogger<ClientesController> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        [HttpGet("clientes/{nome}")]
        public ActionResult <IEnumerable<Cliente>> GetClientesNome(string nome)
        {
            var clientes = _clienteRepository.GetClientesPorNome(nome);

            if (clientes is null)
            {
                return NotFound();
            }

            return Ok(clientes);

        }

        //Usando Logging apenas nessa para demonstrar o uso, nao vejo necessidade de usar nessa api pequena
        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clientes = _clienteRepository.GetAll();
            return Ok(clientes);
        }

        //Restricao para nao atender requisicoes invalidas (id menor que 1)
        [HttpGet("{id:int:min(1)}", Name ="ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _clienteRepository.Get(c=> c.ClienteId == id);
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

            var clienteCriado = _clienteRepository.Create(cliente);
            
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

            _clienteRepository.Update(cliente);
            return Ok(cliente);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        { 
            var cliente = _clienteRepository.Get(c=> c.ClienteId == id);  
            if(cliente == null)
            { 
                return NotFound($"Cliente com id= {id} nao localizado");
            }

            var clienteExcluido = _clienteRepository.Delete(cliente);
       
            return Ok(clienteExcluido);
        }


    }
}
