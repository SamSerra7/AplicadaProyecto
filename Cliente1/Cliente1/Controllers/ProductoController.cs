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
            return View(pd.ObtenerProductos());
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
                        ViewBag.Mensagem = " ingresado con exito ";
                    }

                }
                return View();
            }
            catch (Exception)
            {
                return View("ObtenerLista");
            }
        }

        public ActionResult Actualizar(int id)
        {
            pd = new PriductoData();
            return View(pd.ObtenerProductos().Find(t => t.id_produto == id));
        }

        public ActionResult Actualizar(int id, Producto producto)
        {
            try
            {
                pd = new PriductoData();
                pd.Actualizar(producto);
                return RedirectToAction("ObtenerLista");
            }
            catch (Exception)
            {
                return View("ObtenerLista");
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                pd = new PriductoData();
                if (pd.Eliminar(id))
                {
                    ViewBag.Mensagem = "Eliminado con exito";
                }
                return RedirectToAction("ObtenerLista");
            }
            catch (Exception)
            {
                return View("ObtenerLista");
            }
        }

    }
}