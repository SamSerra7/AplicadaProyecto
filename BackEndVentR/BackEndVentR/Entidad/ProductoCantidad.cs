using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class ProductoCantidad
    {
       // private int id_producto;
        private int cantidad_solicitada;
       
        private Producto productos;

        public Producto Productos
        {
            get { return productos; }
            set { productos = value ?? throw new Exception("Debe tener un producto"); }
        }

   

        public int Cantidad_Solicitada
        {
            get { return cantidad_solicitada; }
            set
            {
                if (value < 0) throw new Exception("La cantidad no puede ser negativo");
                cantidad_solicitada = value;
            }
        }
        public ProductoCantidad()
        {

        }
      

        public ProductoCantidad(Producto productos,int cantidad)
        {
            this.productos = productos;
            this.Cantidad_Solicitada = cantidad;
        }


     


    }
}
