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
    public class CarritoComprasController : ControllerBase
    {
       
        private CarritoComprasNegocio carrito_compras_negocio = new CarritoComprasNegocio();

        // GET api/<CarritoComprasController>/5
        [HttpGet("{id_usuario}")]



        public List<ProductoCantidad> Get(int id_usuario)
        {
            return carrito_compras_negocio.buscar_carrito_compras(id_usuario);


        }


        // POST api/<CarritoComprasController>
        [HttpPost("{CarritoCompra}")]
        public bool Post([FromBody] CarritoComprasProducto carrito_compras_producto)
        {

            bool respuesta = carrito_compras_negocio.agregar_producto_carrito(carrito_compras_producto);
            return respuesta;
        }

        // PUT api/<CarritoComprasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarritoComprasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
