using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionVentR.Models
{
    public class Proveedor
    {

        public int IdProveedor
        {

            get; set;
        }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre{
            get; set; }


        public bool activo { get; set; }

      


    }
}
