using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Entidad
{
    public class Producto
    {

        private int id_producto;

        public int IdProducto
        {
            get { return id_producto; }
            set {
                if (value < 0) throw new Exception("El id no puede ser negativo"); 
                id_producto = value; 
            }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) throw new Exception("El nombre no puede ser vacío");
                nombre = value; 
            }
        }

        private SqlMoney precio;

        public SqlMoney Precio
        {
            get { return precio; }
            set { if (value < 0) throw new Exception("El precio no puede ser negativo"); 
                precio = value; 
            }
        }

        private string urlImg;

        public string UrlImg
        {
            get { return urlImg; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) throw new Exception("Cada producto debe tener una imagen");
                urlImg = value;
            }
        }

        public string Detalle { get; set; }

        public bool Activo { get; set; }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set {
                if (value < 0) throw new Exception("La cantidad no puede ser negativa");
                    cantidad = value; 
            }
        }

        private Proveedor proveedor;

        public Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value ?? throw new Exception("Debe tener un proveedor"); }
        }


        public Producto(int id_producto, string nombre, SqlMoney precio, string urlImg,
            string detalle, bool activo, int cantidad, Proveedor proveedor)
        {
            IdProducto = id_producto;
            Nombre = nombre;
            Precio = precio;
            UrlImg = urlImg;
            Detalle = detalle;
            Activo = activo;
            Cantidad = cantidad;
            Proveedor = proveedor;
        }

        public Producto(string nombre, SqlMoney precio, string urlImg, string detalle, bool activo, int cantidad, Proveedor proveedor)
        {
            Nombre = nombre;
            Precio = precio;
            UrlImg = urlImg;
            Detalle = detalle;
            Activo = activo;
            Cantidad = cantidad;
            Proveedor = proveedor;
        }

        public Producto(){}

    }
}
