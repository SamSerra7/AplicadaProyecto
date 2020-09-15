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
        public IEnumerable<Producto> listar_todos_productos()
        {
            List<Producto> productos = productoDatos.listar_todos_productos();

            //TODO: verificar que solo retorne productos y proveedores activos 


            return productos;

        }

        public Producto listar_todos_productos_id(int idProveedor)
        {
            Producto productos = productoDatos.listar_todos_productos_id(idProveedor);

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
        /// Lista los productos según el id del proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>Lista de Productos</returns>
        public IEnumerable<Producto> listarProductosPorProveedor(int idProveedor)
        {
            return productoDatos.listarProductosPorProveedor(idProveedor);
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

        /// <summary>
        /// Metodo que activa mediante el id del producto
        /// </summary>
        /// <param name="IdProducto"></param>
        /// <returns>Boolean respuesta</returns>
        public Boolean ActivarProducto(int IdProducto)
        {
            return productoDatos.ActivarProducto(IdProducto);
        }

        /// <summary>
        /// Metodo que desactiva mediante el id del producto
        /// </summary>
        /// <param name="IdProducto"></param>
        /// <returns>Boolean respuesta</returns>
        public Boolean DesactivarProducto(int IdProducto)
        {
            return productoDatos.DesactivarProducto(IdProducto);
        }


        /// <summary>
        /// Método para aumentar la cantidad de productos en inventario, recibe el id del producto y el proveedor
        /// y la cantidad que se desea aumentar a dicho producto.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns>Boolean resultado</returns>
        public Boolean AgregarCantidad(Producto producto) {
            return productoDatos.AgregarCantidad(producto.IdProducto,producto.Proveedor.IdProveedor,producto.Cantidad);
        }

        /// <summary>
        /// Metodo que agrega un nuevo producto, recibe un Objeto de Tipo Producto
        /// </summary>
        /// <param name="producto"></param>
        /// <returns>Boolean respuesta</returns>
        public Boolean AgregarProducto(Producto producto)
        {
            return productoDatos.AgregarProducto(producto);
        }
    }


}
