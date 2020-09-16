using Cliente2.EnvioDato;
using Cliente2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cliente2.Controllers
{
    public class ApiConsumoDisminuirCantidadController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> DisminuirCantidad(int id_producto)
        {
            CambioCantidad dato = new CambioCantidad();
            Producto respuesta = await dato.disminuirCantidad(id_producto);
            return View(respuesta);
        }
    }
}
