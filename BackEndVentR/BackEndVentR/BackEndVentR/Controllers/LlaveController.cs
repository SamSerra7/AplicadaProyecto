using System;
using System.Collections.Generic;
using System.Linq;
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
        public void crearLlave([FromBody] int idProveedor)
        {
            llaveNegocio.crearLlave(idProveedor);
        }

       

    }
}
