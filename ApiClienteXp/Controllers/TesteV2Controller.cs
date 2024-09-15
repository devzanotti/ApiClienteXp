using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClienteXp.Controllers
{
    [Route("api/teste")]
    [ApiController]
    [ApiVersion("2.0")]
    public class TesteV2Controller : ControllerBase
    {
        [HttpGet]
        public string GerVersion()
        {
            return "TesteV2 - GET - API versao 2.0";
        }
    }
}
