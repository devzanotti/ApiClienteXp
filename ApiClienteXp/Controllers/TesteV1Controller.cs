using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClienteXp.Controllers
{
    [Route("api/teste")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TesteV1Controller : ControllerBase
    {
        [HttpGet]
        public string GerVersion()
        {
            return "TesteV1 - GET - API versao 1.0";
        }
    }
}
