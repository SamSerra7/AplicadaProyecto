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

        private ProductoData pd;

        // GET: Producto
        public ActionResult ObtenerLista()
        {
            pd = new ProductoData();
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

                    pd = new ProductoData();
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



        [HttpGet]
        public ActionResult ModificarProducto(int id_producto, int id_proveedor, int cantidad)
        {
            pd = new ProductoData();
            pd.ActualizarCantidad(new Producto(id_producto, id_proveedor, cantidad));

            return View("ObtenerLista", pd.ObtenerProductos());

        }



        public ActionResult Actualizar(int id)
        {
            pd = new ProductoData();
            return View(pd.ObtenerProductos().Find(t => t.id_producto == id));
        }
        [HttpPost]
        public ActionResult Actualizar(int id, Producto producto)
        {
            try
            {
                pd = new ProductoData();
                if (pd.Actualizar(producto))
                {
                    ViewBag.Mensagen = " Modificado con exito ";
                }
                return View(pd.ObtenerProductos().Find(t => t.id_producto == id));
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
                pd = new ProductoData();
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