using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ProductoProveedorModel
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public float precio { get; set; }
        public string url_img { get; set; }
        public int cantidad { get; set; }
        public byte estado { get; set; }
        public string descripcion { get; set; }
        public int id_proveedor { get; set; }

        public ProductoProveedorModel()
        {
        }

        public ProductoProveedorModel(int id_produto, string nombre, float precio, string url_img, int cantidad, byte estado, string descripcion, int id_proveedor)
        {
            this.id_producto = id_produto;
            this.nombre = nombre;
            this.precio = precio;
            this.url_img = url_img;
            this.cantidad = cantidad;
            this.estado = estado;
            this.descripcion = descripcion;
            this.id_proveedor = id_proveedor;
        }
        public ProductoProveedorModel(int id_produto, string nombre, float precio, string url_img,  string descripcion, int cantidad,byte estado)
        {
            this.id_producto = id_produto;
            this.nombre = nombre;
            this.precio = precio;
            this.url_img = url_img;
            this.cantidad = cantidad;
            this.estado = estado;
            this.descripcion = descripcion;
        }

    }
}
