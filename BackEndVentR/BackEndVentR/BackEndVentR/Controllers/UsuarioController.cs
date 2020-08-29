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
        public string Post([FromBody] Usuario u)
        {
            return usuarioNegocio.AgregarUsuario(u);
        }

        //POST : api/Usuario/VerificarUsuario
        [HttpPost]
        [Route("[action]")]
        public Boolean VerificarUsuario([FromBody] Usuario u)
        {
            return usuarioNegocio.VerificarUsuario(u);
        }


        //GET  http://localhost:59292/api/usuario/venta/3

        //Get : api/usuario/venta
        [HttpGet("venta/{idUsuario}")]
        public Boolean realizarVenta(int idUsuario)
        {
            return usuarioNegocio.registrarVenta(idUsuario);
        }

        //PRODUCTO




        // PUT api/<ProductoController> listar para un usuario específico
        [HttpGet("{idUsuario}")]
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


    }
}
