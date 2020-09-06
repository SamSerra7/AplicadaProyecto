using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LlaveController : ControllerBase
    {
        ///Variable capa negocios
        private LlaveNegocio llaveNegocio = new LlaveNegocio();


        
        // GET api/<LlaveController>/5
        [HttpGet("{idProveedor}")]
        public bool tieneLlaveActiva(int idProveedor)
        {
            return llaveNegocio.tieneLlaveActiva(idProveedor);
        }


        // POST api/<LlaveController>
        [HttpPost]
        public IActionResult crearLlave([FromBody] JsonElement body)
        {
            llaveNegocio.crearLlave(body.GetProperty("idProveedor").GetInt32());
            return Ok();
        }

        // GET api/<LlaveController>/5/llave
        [HttpGet("{idProveedor}/llave")]
        public Llave llaveActiva(int idProveedor)
        {
            return llaveNegocio.llaveActiva(idProveedor);
        }



    }
}
