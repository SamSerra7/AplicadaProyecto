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

        [HttpGet]
        public IEnumerable<Usuario> ListarUsuarios()
        {
            return usuarioNegocio.listarUsuarios();
        }

    }
}
