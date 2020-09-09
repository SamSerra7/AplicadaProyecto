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
        public int cantidad { get; set; }
        public byte estado { get; set; }
        public string descripcion { get; set; }
        public int id_proveedor { get; set; }

        public Producto()
        {
        }

        public Producto(int id_produto, string nombre, float precio, string url_img, int cantidad, byte estado, string descripcion, int id_proveedor)
        {
            this.id_produto = id_produto;
            this.nombre = nombre;
            this.precio = precio;
            this.url_img = url_img;
            this.cantidad = cantidad;
            this.estado = estado;
            this.descripcion = descripcion;
            this.id_proveedor = id_proveedor;
        }


        public Producto(int id_produto, int id_proveedor,int cantidad)
        {
            this.id_produto = id_produto;
           
            this.cantidad = cantidad;
            this.id_proveedor = id_proveedor;
        }
    }
}