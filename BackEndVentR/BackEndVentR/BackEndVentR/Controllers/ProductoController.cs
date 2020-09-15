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


        /// <summary>
        /// Metodo get que Lista los productos según el id del proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>Lista de Productos</returns>
        //GET: api/Producto/ListarProductoPorProveedor/1
        [HttpGet("ListarProductoPorProveedor/{idProveedor}")]
        public IEnumerable<Producto> ListaProductoPorProveedor(int idProveedor)
        {
            return productoNegocio.listarProductosPorProveedor(idProveedor);
        }


        // GET api/<ProductoController>/5   buscar sin usuario
        [HttpGet("{idProducto}")]
        public Producto buscarProducto(int idProducto)
        {
            return productoNegocio.buscarProducto(idProducto);
        }

        //Lista todos los producos de BD independientemente de su estado
        //api/Producto/Listar_todos_productos
        [HttpGet("Listar_todos_productos")]
        public IEnumerable<Producto> Listar_todos_productos()
        {
            return productoNegocio.listar_todos_productos();
        }

        //api/Producto/Obtener_producto/3
        [HttpGet("Obtener_producto/{idProducto}")]
        public Producto Obtener_producto(int idProducto)
        {
            return productoNegocio.listar_todos_productos_id(idProducto);
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

        //PUT api/Producto/AgregarCantidad                        Agrega una cantidad dada a un producto
        //Nota: Recibe un modelo con IdProductoProveedor,Proveedor(Solo su id) y cantidad
        [HttpPut("AgregarCantidad")]
        public Boolean AgregarCantidad([FromBody] Producto producto)
        {
            return productoNegocio.AgregarCantidad(producto);
        }

        //POST api/Producto/AgregarProducto        
        //Nota: Este metodo agrega un producto nuevo
        //Nota: Producto(int id_producto_proveedor, string nombre, SqlMoney precio, string urlImg,
        //string detalle, int cantidad, Proveedor proveedor)
        //Nota2: El Proveedor solo recibe el id Proveedor(int id_proveedor)
        [HttpPost("AgregarProducto")]
        public Boolean AgregarProducto([FromBody] Producto producto)
        {
            return productoNegocio.AgregarProducto(producto);
        }


    }
}
