using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Entidad;


namespace BackEndVentR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private RolNegocio rolNegocio = new RolNegocio();
        

        [HttpGet]
        public IEnumerable<Rol> ListarRoles()
        {
            return rolNegocio.listarRoles();
        }

       
    }
}
