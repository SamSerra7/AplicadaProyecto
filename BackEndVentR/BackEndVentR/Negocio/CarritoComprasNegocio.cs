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
        public bool agregar_producto_carrito(int idUsuario, CarritoComprasProducto carrito)
        {
            return carrito_compras_datos.agregar_producto_carrito(idUsuario,carrito); 
        }

        
        public List<ProductoCantidad> buscar_carrito_compras(int id_usuario)
        {
            List<ProductoCantidad> carrito = carrito_compras_datos.buscar_carrito_compras(id_usuario);

            return carrito;

        }


        public Boolean agregar_cantidad(int idProducto, int idUsuario) {
          return  carrito_compras_datos.agregar_cantidad(idProducto, idUsuario);
        }

        public Boolean disminuir_cantidad(int idUsuario, int idProducto)
        {
            return carrito_compras_datos.disminuir_cantidad(idUsuario,idProducto);
        }


        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que permite borrar un elemento del carrito
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idUsuario"></param>
        /// <returns>variable booleana</returns>
        public bool borrarDelCarrito(int idUsuario, int idProducto)
        {
            return carrito_compras_datos.borrarDelCarrito(idUsuario, idProducto);
        }
    }
}
