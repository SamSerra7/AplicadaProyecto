using Cliente3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Cliente3.EnvioDato
{
    public class ReciboDatoLlave
    {

        public async System.Threading.Tasks.Task<Llave> recibirLlave()
        {
            using (var httpClient = new HttpClient())
            {

                string url = string.Concat(Constante.Constantes.URLParaRecibirLlave, "/2/llave");
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<Llave>(apiResponse);
                    return respuesta;
                }
            }
        }

    }
}