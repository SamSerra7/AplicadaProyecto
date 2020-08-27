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
    public class ProductoController : ControllerBase
    {
        ///Variable capa negocios
        private ProductoNegocio productoNegocio = new ProductoNegocio();


        // GET: api/<ProductoController> listar sin usuario
        [HttpGet]
        public IEnumerable<Producto> listarProductos()
        {
            return productoNegocio.listarProductos();
        }

        // PUT api/<ProductoController> listar para un usuario específico
        [HttpPut]
        public IEnumerable<Producto> listarProductosUsuario([FromBody] int idUsuario)
        {
            return productoNegocio.listarProductosPorUsuario(idUsuario);
        }


        // GET api/<ProductoController>/5   buscar sin usuario
        [HttpGet("{idProducto}")]
        public Producto buscarProducto(int idProducto)
        {
            return productoNegocio.buscarProducto(idProducto);
        }

        // PUT api/<ProductoController>/5   buscar algún producto, para un usuario específico
        [HttpPut("{idProducto}")]
        public Producto buscarProductoUsuario(int idProducto, [FromBody] int idUsuario)
        {
            return productoNegocio.buscarProductoUsuario(idProducto,idUsuario);
        }


    }
}
