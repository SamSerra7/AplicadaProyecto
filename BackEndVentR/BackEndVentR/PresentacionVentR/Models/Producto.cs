using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace PresentacionVentR.Models
{
    public class Producto
    {

        public int idProducto { get; set; }



        public string nombre { get; set; }



        public SqlMoney precio { get; set; }



        public string urlImg { get; set; }


        public string detalle { get; set; }

        public bool activo { get; set; }

        public int cantidad { get; set; }



        public Producto(int p_id_producto, string p_nombre, string p_urlImg,
            string p_detalle, int p_cantidad, bool p_activo)
        {
            idProducto = p_id_producto;
            nombre = p_nombre;
          //  precio = p_precio;
            urlImg = p_urlImg;
            detalle = p_detalle;
            cantidad = p_cantidad;
            activo = p_activo;
        }



        public Producto() { }

    }
}
