using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class CarritoComprasProducto
    {

        private int id_carrito_compras_producto;
        private int id_producto;
        private int id_carrito_compras;
        private int cantidad;

      
        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value ?? throw new Exception("Debe tener un proveedor"); }
        }


        public int idCarritoComprasProducto
        {
            get { return id_carrito_compras_producto; }
            set
            {
                if (value < 0) throw new Exception("El id no puede ser negativo");
                id_carrito_compras_producto = value;
            }
        }
        public int idProducto
        {
            get { return id_producto; }
            set
            {
                if (value < 0) throw new Exception("El id no puede ser negativo");
                id_producto = value;
            }
        }

        public int idCarritoCompras
        {
            get { return id_carrito_compras; }
            set
            {
                if (value < 0) throw new Exception("El id no puede ser negativo");
                id_carrito_compras = value;
            }
        }


        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value < 0) throw new Exception("La cantidad no puede ser negativa");
                cantidad = value;
            }
        }

        public CarritoComprasProducto(int idCarritoComprasProducto, int idCarritoCompras , int idProducto, int cantidad)
        {
            
            this.idCarritoComprasProducto = idCarritoComprasProducto;
            this.idProducto = idProducto;
            this.idCarritoCompras = idCarritoCompras;
            this.Cantidad = cantidad;
        }

        public CarritoComprasProducto( int idCarritoCompras, int idProducto, int cantidad)
        {

            this.idProducto = idProducto;
            this.idCarritoCompras = idCarritoCompras;
            this.Cantidad = cantidad;
        }

      
        public CarritoComprasProducto()
        {
        }
    }
}
