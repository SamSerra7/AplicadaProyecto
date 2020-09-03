using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentacionVentR.EnvioDato;
using PresentacionVentR.Models;

namespace PresentacionVentR.Controllers
{
    public class ProveedorController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListarProveedores()
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();

            var respuesta = await envioDatoProveedor.listarPersonaAsync();

            return View(respuesta);
        }

        public ActionResult RegistrarProveedor()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Registrar([FromBody]  Proveedor proveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
      
           bool respuesta = await envioDatoProveedor.registrarProveedorAsync(proveedor);

            return Json(respuesta);
        }

        [HttpGet]
        public async Task<IActionResult> FiltrarProveedor(int idProveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
            Proveedor respuesta = await envioDatoProveedor.filtrandoProveedorAsync(idProveedor);

            return View(respuesta);
        }
        [HttpGet]
        public async Task<IActionResult> DetalleProveedor(int idProveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
            Proveedor respuesta = await envioDatoProveedor.filtrandoProveedorAsync(idProveedor);

            return View(respuesta);
        }


        [HttpPut]
        public async Task<JsonResult> Modificar([FromBody] Proveedor proveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();

            bool respuesta = await envioDatoProveedor.modificarProveedorAsync(proveedor);

            return Json(respuesta);
        }


        public async Task<IActionResult> EliminarProveedor(int idProveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
            bool respuesta = await envioDatoProveedor.eliminarProveedorAsync(idProveedor);

            return Json(respuesta);
            //return View("ListarProveedores", await envioDatoProveedor.listarPersonaAsync());
        }



        [HttpGet]
        public async Task<IActionResult> ActivarProveedor(int IdProveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
            bool respuesta = await envioDatoProveedor.activarProveedorAsync(IdProveedor);
            return Json(respuesta);
        }
        [HttpGet]
        public async Task<IActionResult> DesactivarProveedor(int IdProveedor)
        {
            EnvioDatoProveedor envioDatoProveedor = new EnvioDatoProveedor();
            bool respuesta = await envioDatoProveedor.desactivarProveedorAsync(IdProveedor);
            return Json(respuesta);
        }

    }
}
