using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class ProveedorController : ControllerBase 
    {

        private ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
        // GET: api/<ProveedorController>
        [HttpGet]
        public IEnumerable<Proveedor> Get()
        {
            return proveedorNegocio.listarProveedores();
            
        }

        // GET api/<ProveedorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProveedorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProveedorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProveedorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
