using Cliente1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cliente1.Controllers
{
    public class ProductoController : Controller
    {

        private PriductoData pd;

        // GET: Producto
        public ActionResult ObtenerLista()
        {
            pd = new PriductoData();
            ModelState.Clear();
            return View(pd.obtenerProductos());
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    pd = new PriductoData();
                    if (pd.Add(producto))
                    {
                        ViewBag.Mensagen = " ingresado con exito ";
                    }

                }
                return View();
            }
            catch (Exception) 
            {
                return View("ObtenerLista");
            }
        }


    }
}