using Dato;
using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class ProductoNegocio
    {

        ///variable capa datos
        private ProductoDatos productoDatos = new ProductoDatos();

        /// <summary>
        /// Samuel Serrano
        /// Método que se encarga de las reglas de negocio para productos
        /// </summary>
        /// <returns>Enumerable de productos</returns>
        public IEnumerable<Producto> listarProductos()
        {
            List<Producto> productos = productoDatos.listarProductos();

            //TODO: verificar que solo retorne productos y proveedores activos 


            return productos;

        }

        /// <summary>
        /// Samuel Serrano 
        /// Método que retorna un producto buscado por ID
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns>objeto Producto</returns>
        public Producto buscarProducto(int idProducto)
        {
            //TODO revisar que esté activo 
            
            return productoDatos.buscarProducto(idProducto);
        }


        /// <summary>
        /// Lista los productos según la preferencia de el usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Lista de Productos</returns>
        public IEnumerable<Producto> listarProductosPorUsuario(int idUsuario)
        {
            return productoDatos.listarProductosPorUsuario(idUsuario);
        }


        /// <summary>
        /// Busca un producto por USUARIO específico
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idUsuario"></param>
        /// <returns>objeto Producto</returns>
        public Producto buscarProductoUsuario(int idProducto, int idUsuario)
        {
            productoDatos.AgregarBusquedaProductoUsuario(idProducto,idUsuario);
            return productoDatos.buscarProducto(idProducto);
        }

        public Boolean ActivarProducto(int IdProducto)
        {
            return productoDatos.ActivarProducto(IdProducto);
        }

        public Boolean DesactivarProducto(int IdProducto)
        {
            return productoDatos.DesactivarProducto(IdProducto);
        }
    }


}
