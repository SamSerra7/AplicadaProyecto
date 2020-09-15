using Cliente3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Cliente3.EnvioDato
{
    public class CambioCantidad
    {
        public async System.Threading.Tasks.Task<Producto> disminuirCantidad(int id_producto)
        {
            using (var httpClient = new HttpClient())
            {

                string url = string.Concat(Constante.Constantes.URLParaDisminuirCantidadProducto, "/metodoControladorParadisminuircantidad/" + id_producto);
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