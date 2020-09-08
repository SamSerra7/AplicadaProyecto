using Dato;
using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class LlaveNegocio
    {
        //Variable Capa Datos
        private LlaveDatos llaveDatos = new LlaveDatos();

        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que verifica si un usuario tiene una llave activa
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>variable booleana</returns>
        public bool tieneLlaveActiva(int idProveedor)
        {
            return idProveedor > 0 ? llaveDatos.tieneLlaveActiva(idProveedor) : false;
        }


        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que crea una llave para un proveedor en específico
        /// </summary>
        /// <param name="idProveedor"></param>
        public void crearLlave(int idProveedor)
        {
            if(idProveedor <= 0) throw new Exception("Proveedor inválido");
            llaveDatos.crearLlave(idProveedor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>obeto tipo Llave</returns>
        public Llave llaveActiva(int idProveedor)
        {
            return tieneLlaveActiva(idProveedor) ? llaveDatos.llaveActiva(idProveedor) : llaveDatos.eliminarLlave(idProveedor);
        }
    }
}
