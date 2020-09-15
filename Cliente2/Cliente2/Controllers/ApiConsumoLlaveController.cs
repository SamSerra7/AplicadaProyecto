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
    public class ApiConsumoLlaveController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> RecibirLlave()
        {
            ReciboDatoLlave dato = new ReciboDatoLlave();
            Llave respuesta = await dato.recibirLlave();

            return View(respuesta);
        }
    }
}
