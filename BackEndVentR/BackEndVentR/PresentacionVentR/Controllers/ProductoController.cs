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
    public class ProductoController : Controller
    {
       private ApiProducto apiProducto = new ApiProducto();
        private EnvioDatoProveedor apiProveedor = new EnvioDatoProveedor();
        // GET: ProductoController


        public async Task<ActionResult> ProductoProveedorView()
        {

            var respuesta = await apiProveedor.listarPersonaAsync();

            return View(respuesta);
        }
        [HttpPost]
        public async Task<ActionResult> Buscar(string IdProveedor)
        {
            HttpContext.Session.SetString("IdProveedor", IdProveedor);
            var respuesta = await apiProducto.listarProductoPorProveedor(Convert.ToInt32(IdProveedor));
            return View("Index",respuesta);
        }

        public async Task<IActionResult> DesactivarProducto(int idProducto)
        {

            if (await apiProducto.DesactivarProducto(idProducto))
            {
                return View("Index", await apiProducto.listarProductoPorProveedor(
                    Convert.ToInt32(HttpContext.Session.GetString("IdProveedor"))));
            }

            return View("Error", new ErrorViewModel { RequestId = "3" });

        }

        public async Task<IActionResult> Index() {
            return View("Index", await apiProducto.listarProductoPorProveedor(
        Convert.ToInt32(HttpContext.Session.GetString("IdProveedor"))));
        }

        public async Task<IActionResult> ActivarProducto(int idProducto)
        {

            if (await apiProducto.ActivarProducto(idProducto))
            {
                return View("Index", await apiProducto.listarProductoPorProveedor(
                    Convert.ToInt32(HttpContext.Session.GetString("IdProveedor"))));
            }

            return View("Error", new ErrorViewModel { RequestId = "3" });


        }

        // GET: ProductoController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Producto respuesta = await apiProducto.filtrandoProductoAsync(id);
            return View(respuesta);
        }






    }
}
