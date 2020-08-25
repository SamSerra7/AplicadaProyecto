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

    }
}
