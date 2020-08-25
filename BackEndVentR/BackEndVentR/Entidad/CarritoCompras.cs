using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public class CarritoCompras
    {


        private int id_carrito_compras;

        private int id_usuario;
     

        public List<ProductoCantidad> productocantidad = new List<ProductoCantidad>();



        public int idCarritoCompras
        {
            get { return id_carrito_compras; }
            set
            {
                if (value < 0) throw new Exception("El id del carrito de compras no puede ser negativo");
                id_carrito_compras = value;
            }
        }

        public int idUsuario
        {
            get { return id_usuario; }
            set
            {
                if (value < 0) throw new Exception("El id del usuario  no puede ser negativo");
                id_usuario = value;
            }
        }

        public CarritoCompras(int id_carrito_compras, int id_usuario)
        {
            this.idCarritoCompras = id_carrito_compras;
            this.idUsuario = id_usuario;
            
        }

        public CarritoCompras(List<ProductoCantidad> productocantidad)
        {
           
            this.productocantidad = productocantidad;
        }


        public CarritoCompras()
        {
       }
    }

}
