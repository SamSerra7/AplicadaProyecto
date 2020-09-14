using Newtonsoft.Json;
using PresentacionVentR.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresentacionVentR.EnvioDato
{
    public class ApiProducto
    {
        private List<Producto> listandoProveedorAsync = new List<Producto>();
        public async System.Threading.Tasks.Task<IEnumerable<Producto>> listarProductoAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(Constantes.Constantes.urlProducto, "/Listar_todos_productos");

                List<Producto> listaProductos = new List<Producto>();
                using (var response = await httpClient.GetAsync(url))
                {
                 
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<List<Producto>>(apiResponse);

                    foreach (Producto prod in respuesta)
                    {
                        listaProductos.Add(
                         new Producto(prod.idProducto,
                             prod.nombre,
                   
                             prod.urlImg,
                             prod.detalle,
                             prod.cantidad,
                             prod.activo
                       )
                         );
                    }
                        return listaProductos;
                }
            }
        }
        public async System.Threading.Tasks.Task<Producto> filtrandoProductoAsync(int idProducto)
        {
            using (var httpClient = new HttpClient())
            {

                string url = string.Concat(Constantes.Constantes.urlProducto, "/Obtener_producto/" + idProducto);
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<Producto>(apiResponse);
                    return respuesta;
                }
            }
        }


    }
}
