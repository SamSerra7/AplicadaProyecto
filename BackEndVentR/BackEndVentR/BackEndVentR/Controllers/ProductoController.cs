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

        


        // GET api/<ProductoController>/5   buscar sin usuario
        [HttpGet("{idProducto}")]
        public Producto buscarProducto(int idProducto)
        {
            return productoNegocio.buscarProducto(idProducto);
        }

        
        //GET api/Producto/ActivarProducto/1    Activa un producto especifico por su id de producto
        [HttpGet("ActivarProducto/{IdProducto}")]
        public Boolean ActivarProducto(int IdProducto)
        {
            return productoNegocio.ActivarProducto(IdProducto);
        }

        //GET api/Producto/DesactivarProducto/1             Desactiva un producto especifico por su id de producto
        [HttpGet("DesactivarProducto/{IdProducto}")]
        public Boolean DesactivarProducto( int IdProducto)
        {
            return productoNegocio.DesactivarProducto(IdProducto);
        }


    }
}
