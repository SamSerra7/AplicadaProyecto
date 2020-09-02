using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PresentacionVentR.Models;

namespace PresentacionVentR.EnvioDato
{
    public class EnvioDatoProveedor
    {


        private List<Proveedor> listandoProveedorAsync = new List<Proveedor>();
        public async System.Threading.Tasks.Task<IEnumerable<Proveedor>> listarPersonaAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(Constantes.Constantes.URLParaEnvioDatosProveedor, "/ListarProveedores");

                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<List<Proveedor>>(apiResponse);
                    return respuesta;
                }
            }
        }

        public async Task<bool> eliminarProveedorAsync(int idProveedor)
        {
            bool respuesta = false;
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(Constantes.Constantes.URLParaEnvioDatosProveedor, "/EliminarProveedor/" + idProveedor);

                using (var response = await httpClient.DeleteAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<bool>(apiResponse);
                    return respuesta;
                }
            }
        }
        public async Task<bool> modificarProveedorAsync(Proveedor proveedor)
        {
            bool respuesta = false;

            using (var httpClient = new HttpClient())
            {
                StringContent contenido = new StringContent(JsonConvert.SerializeObject(proveedor), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(Constantes.Constantes.URLParaEnvioDatosProveedor, contenido))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return respuesta;
        }
        public async Task<bool> registrarProveedorAsync(Proveedor proveedor)
        {
            bool respuesta = false;
          
            using (var httpClient = new HttpClient())
            {
                StringContent contenido = new StringContent(JsonConvert.SerializeObject(proveedor), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(Constantes.Constantes.URLParaEnvioDatosProveedor, contenido))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return respuesta;
        }


        public async System.Threading.Tasks.Task<Proveedor> filtrandoProveedorAsync(int proveedorId)
        {
            using (var httpClient = new HttpClient())
            {

                string url = string.Concat(Constantes.Constantes.URLParaEnvioDatosProveedor, "/BuscarProveedor/" + proveedorId);
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
               
                    var respuesta = JsonConvert.DeserializeObject<Proveedor>(apiResponse);
                    return respuesta;
                }
            }
        }

   

    }
}
