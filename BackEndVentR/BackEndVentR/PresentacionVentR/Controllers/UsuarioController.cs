using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentacionVentR.EnvioDato;
using PresentacionVentR.Models;

namespace PresentacionVentR.Controllers
{
    public class UsuarioController : Controller
    {
        private ApiUsuarioHelper api = new ApiUsuarioHelper();

        // GET: UsuarioController
        public ActionResult AgregarUsuarioView()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ModificarUsuarioView(int idUsuario)
        {
            return View(await api.ObtenerUsuarioEspecifico(idUsuario));
        }

        public async Task<IActionResult> ListarUsuarioView()
        {
            var respuesta = await api.ListarUsuarios();
            return View(respuesta);

        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {

            if (await api.RegistrarUsuario(usuario))
            {
                return View("ListarUsuarioView", await api.ListarUsuarios());
            }
            return View("Error", new ErrorViewModel { RequestId = "2" });
        }

        [HttpPost]
        public async Task<IActionResult> Modificar(Usuario usuario)
        {
            if (await api.ActualizarUsuario(usuario)) {
                return View("ListarUsuarioView", await api.ListarUsuarios()); 
            }
            return View("Error", new ErrorViewModel { RequestId = "1"});
        }


        public async Task<IActionResult> EliminarUsuario(int idUsuario)
        {

            if (await api.EliminarUsuario(idUsuario))
            {
                return View("ListarUsuarioView", await api.ListarUsuarios());
            }

            return View("Error", new ErrorViewModel { RequestId = "3" });


        }

    }
}
