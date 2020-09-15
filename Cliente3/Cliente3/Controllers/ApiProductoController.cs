using Cliente3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cliente3.Controllers
{
    public class ApiProductoController : ApiController
    {


        ProductoData producto = new ProductoData();

        ///api/ApiProducto
        public List<Producto> Get()
        {
            return producto.ObtenerProductos();
        }

        ///api/ApiProducto/#id
        public List<Producto> Get(int id)
        {
            return producto.ObtenerProductoId(id);
        }

        // POST: api/ApiProducto
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ApiProducto/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiProducto/5
        public void Delete(int id)
        {
        }
    }
}
