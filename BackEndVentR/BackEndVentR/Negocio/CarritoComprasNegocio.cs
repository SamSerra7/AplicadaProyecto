using Dato;
using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class CarritoComprasNegocio
    {

        private CarritoComprasDatos carrito_compras_datos = new CarritoComprasDatos();
        public bool agregar_producto_carrito(CarritoComprasProducto carrito)
        {
            return carrito_compras_datos.agregar_producto_carrito(carrito); 
        }

        
        public List<ProductoCantidad> buscar_carrito_compras(int id_usuario)
        {
            List<ProductoCantidad> carrito = carrito_compras_datos.buscar_carrito_compras(id_usuario);

            return carrito;

        }


    }
}
