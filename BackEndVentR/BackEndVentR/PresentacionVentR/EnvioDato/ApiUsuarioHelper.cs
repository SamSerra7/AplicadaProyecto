using Newtonsoft.Json;
using PresentacionVentR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionVentR.EnvioDato
{
    public class ApiUsuarioHelper
    {
        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            using (var httpClient = new HttpClient())
            {


                using (var response = await httpClient.GetAsync(Constantes.Constantes.BaseURLApiUsuario))
                {
                    string ApiRespuesta = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<List<Usuario>>(ApiRespuesta);
                    return respuesta;
                }
            }
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            Boolean respuesta;
            using (var httpClient = new HttpClient())
            {
                StringContent contenido = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(Constantes.Constantes.BaseURLApiUsuario, contenido))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return respuesta;
        }

        public async Task<Boolean> EliminarUsuario(int IdUsuario)
        {
            bool resp = false;
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync(Constantes.Constantes.BaseURLApiUsuario + "/EliminarUsuario/" + IdUsuario))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }
            return resp;
        }

        public async Task<Usuario> ObtenerUsuarioEspecifico(int idUsuario)
        {

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync(Constantes.Constantes.BaseURLApiUsuario + "/" + idUsuario))
                {
                    string respuestaApi = await response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<Usuario>(respuestaApi);
                    return respuesta;
                }
            }
        }


        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            bool resp = false;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(Constantes.Constantes.BaseURLApiUsuario, content))
                {
                    string respuesta = await response.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<bool>(respuesta);
                }
            }
            return resp;
        }
    }
}
