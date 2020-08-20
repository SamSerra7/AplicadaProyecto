using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente1.Models
{
    public class Producto
    {

        public int id_produto { get; set; }
        public string nombre { get; set; }
        public float precio { get; set; }
        public string url_img { get; set; }
        public string descripcion { get; set; }

        public Producto()
        {
        }

        public Producto(int id_produto, string nombre, float precio, string url_img, string descripcion)
        {
            this.id_produto = id_produto;
            this.nombre = nombre;
            this.precio = precio;
            this.url_img = url_img;
            this.descripcion = descripcion;
        }

       
    }
}
