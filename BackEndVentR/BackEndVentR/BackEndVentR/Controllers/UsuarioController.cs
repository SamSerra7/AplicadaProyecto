using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entidad;
using Negocio;


namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private ProductoNegocio productoNegocio = new ProductoNegocio();
        private CarritoComprasNegocio carrito_compras_negocio = new CarritoComprasNegocio();

        //GET : api/Usuario"
        [HttpGet]
        public IEnumerable<Usuario> ListarUsuarios()
        {
            return usuarioNegocio.ListarUsuarios();
        }


        //GET : api/Usuario/1
        [HttpGet("{id_usuario}")]
        public Usuario Get(int id_usuario)
        {
            return usuarioNegocio.BuscarUsuarioEspecifico(id_usuario);
        }

        //POST : api/Usuario

        [HttpPost]
        public bool Post([FromBody] Usuario u)
        {
            return usuarioNegocio.AgregarUsuario(u);
        }


        //PUT api/Usuario              Recibe un usuario para modificar su registro en la base de datos
        [HttpPut]
        public bool ActualizarUsuario([FromBody] Usuario u)
        {
            return usuarioNegocio.ModificarUsuario(u);
        }

        //POST : api/Usuario/VerificarUsuario
        [HttpPost]
        [Route("[action]")]
        public Boolean VerificarUsuario([FromBody] Usuario u)
        {
            return usuarioNegocio.VerificarUsuario(u);
        }

        //Delete api/Usuario/EliminarUsuario/2
        [HttpDelete("EliminarUsuario/{IdUsuario}")]
        public Boolean EliminarUsuario(int IdUsuario)
        {
            return usuarioNegocio.EliminarUsuario(IdUsuario);
        }


        
        //Get : api/usuario/venta
        [HttpGet("venta/{idUsuario}")]
        public Boolean realizarVenta(int idUsuario)
        {
            return usuarioNegocio.registrarVenta(idUsuario);
        }

        //PRODUCTO




        // PUT api/<ProductoController> listar para un usuario específico
        [HttpGet("{idUsuario}/producto")]
        public IEnumerable<Producto> listarProductosUsuario(int idUsuario)
        {
            return productoNegocio.listarProductosPorUsuario(idUsuario);
        }

        // PUT api/<ProductoController>/5   buscar algún producto, para un usuario específico
        [HttpGet("{idUsuario}/producto/{idProducto}")]
        public Producto buscarProductoUsuario(int idProducto, int idUsuario)
        {
            return productoNegocio.buscarProductoUsuario(idProducto, idUsuario);
        }



        // Disminuyen en 1 los productos del carrito de compras
        //  GET api/carritocompras/DisminuirCantidad
        [HttpGet("{idUsuario}/Producto/{idProducto}/DisminuirCantidad")]
        public Boolean DisminuirCantidad(int idUsuario, int idProducto)
        {
            return carrito_compras_negocio.disminuir_cantidad(idUsuario, idProducto);
        }


        // Aumenta en 1 los productos del carrito de compras
        //  GET api/carritocompras/DisminuirCantidad
        [HttpGet("{idUsuario}/Producto/{idProducto}/Agregarcantidad")]

        public Boolean Agregar_cantidad(int idUsuario, int idProducto)
        {
            return carrito_compras_negocio.agregar_cantidad(idProducto, idUsuario);
        }

    }
}
