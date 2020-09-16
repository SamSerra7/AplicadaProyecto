using Cliente3.EnvioDato;
using Cliente3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cliente3.Controllers
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
