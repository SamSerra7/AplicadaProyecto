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

        // GET: ProductoController


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            
            var respuesta = await apiProducto.listarProductoAsync();

            return View(respuesta);
        }
        // GET: ProductoController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Producto respuesta = await apiProducto.filtrandoProductoAsync(id);
            return View(respuesta);
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
