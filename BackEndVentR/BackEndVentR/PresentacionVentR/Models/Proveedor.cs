using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionVentR.Models
{
    public class Proveedor
    {

        //private int id_proveedor;
        public int IdProveedor
        {

            get; set;
        }
           
        

       // private string nombre;
        public string Nombre
        {
            get; set;

        }


        public bool activo { get; set; }

        //public Proveedor()
        //{
        //}

        //public Proveedor(string nombre)
        //{
        //    this.Nombre = nombre;
        //}

        //public Proveedor(int id_proveedor, string nombre, bool activo)
        //{
        //    this.IdProveedor = id_proveedor;
        //    this.Nombre = nombre;
        //    this.activo = activo;
        //}

        //public Proveedor(int id_proveedor)
        //{
        //    this.IdProveedor = id_proveedor;

       // }


    }
}
