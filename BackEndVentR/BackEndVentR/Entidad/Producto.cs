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
                if (id_producto < 0) throw new Exception("El id no puede ser negativo"); 
                id_producto = value; 
            }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set {
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrWhiteSpace(nombre)) throw new Exception("El nombre no puede ser vacío");
                nombre = value; 
            }
        }

        private SqlMoney precio;

        public SqlMoney Precio
        {
            get { return precio; }
            set { if (precio < 0) throw new Exception("El precio no puede ser negativo"); 
                precio = value; 
            }
        }

        private string urlImg;

        public string UrlImg
        {
            get { return urlImg; }
            set
            {
                if (string.IsNullOrEmpty(urlImg) || string.IsNullOrWhiteSpace(urlImg)) throw new Exception("Cada producto debe tener una imagen");
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
                if (cantidad > 0) throw new Exception("La cantidad no puede ser negativa");
                    cantidad = value; 
            }
        }



    }
}
