using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ProductoCantidad
    {
        private int id_producto;
        private int cantidad;
       
        private Producto productos;

        public Producto Productos
        {
            get { return productos; }
            set { productos = value ?? throw new Exception("Debe tener un producto"); }
        }

        public int Producto
        {
            get { return id_producto; }
            set
            {
                if (value < 0) throw new Exception("El producto no puede ser negativo");
                id_producto = value;
            }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value < 0) throw new Exception("La cantidad no puede ser negativo");
                cantidad = value;
            }
        }
        public ProductoCantidad()
        {

        }
      

        public ProductoCantidad(int producto, int cantidad,Producto productos)
        {
            this.id_producto = producto;
            this.Cantidad = cantidad;
            this.productos = productos;
        }


     


    }
}
