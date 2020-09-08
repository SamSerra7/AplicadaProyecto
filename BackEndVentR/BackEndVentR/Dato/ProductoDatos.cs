using Entidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Dato
{
    /// <summary>
    /// Clase que se conecta con la tabla de Productos en lla base de datos
    /// </summary>
    public class ProductoDatos
    {

        ///Variables globales
        private Conexion conexion = new Conexion();
        private ProveedorDatos proveedorDatos = new ProveedorDatos();

        /// <summary>
        /// Samuel Serrano 
        /// Método que obtiene todos los productos de la BD
        /// </summary>
        /// <returns>Products List</returns>
        public List<Producto> listarProductos()
        {

            List<Producto> productos = new List<Producto>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_producto,nombre,precio,url_img,detalle,activo,cantidad,id_proveedor FROM products.producto WHERE activo<>false";


                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto(reader.GetInt32(0), reader.GetString(1),
                                                            reader.GetDecimal(2), reader.GetString(3),
                                                                reader.GetString(4), reader.GetBoolean(5),
                                                                    reader.GetInt32(6), proveedorDatos.buscarProveedor(reader.GetInt32(7)))
                            );
                        }

                    }
                }
            }
            return productos;
        }


        /// <summary>
        /// Samuel Serrano
        /// Método que obtiene un producto específico de la base de datos (por ID)
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns>objeto Producto</returns>
        public Producto buscarProducto(int idProducto)
        {
            Producto producto = new Producto();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_producto,nombre,precio,url_img,detalle,activo,cantidad,id_proveedor FROM products.producto "
                                + " WHERE activo<>false AND id_producto = @idProducto";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idProducto", idProducto);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            producto = new Producto(reader.GetInt32(0), reader.GetString(1),
                                                    reader.GetDecimal(2), reader.GetString(3),
                                                    reader.GetString(4), reader.GetBoolean(5),
                                                    reader.GetInt32(6), proveedorDatos.buscarProveedor(reader.GetInt32(7)));
                        }


                    }
                }
            }

            return producto;
        }


        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que gestiona las búsquedas de un usuario hacia un producto (aumenta la cantidad de búsquedas por producto)
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idProducto"></param>
        public void AgregarBusquedaProductoUsuario(int idProducto, int idUsuario)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "CALL products.agregar_busquedas_prod_usuario(@id_usuario,@id_producto);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue(":id_usuario", idUsuario);
                    command.Parameters.AddWithValue(":id_producto", idProducto);
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Samuel Serrano 
        /// Método que obtiene todos los productos de la BD, ordenados por cantidad de apariciones
        /// </summary>
        /// <returns>Products List</returns>
        public List<Producto> listarProductosPorUsuario(int idUsuario)
        {

            List<Producto> productos = new List<Producto>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT * FROM products.pa_listarProductosPorUsuario(@idUsuario)";


                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue(":idUsuario", idUsuario);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto(reader.GetInt32(0), reader.GetString(1),
                                                            reader.GetDecimal(2), reader.GetString(3),
                                                                reader.GetString(4), reader.GetBoolean(5),
                                                                    reader.GetInt32(6), proveedorDatos.buscarProveedor(reader.GetInt32(7)))
                            );
                        }

                    }
                }
            }
            return productos;
        }


        //Desactiva un producto por su id
        public Boolean DesactivarProducto(int IdProducto)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_desactivar_producto(@p_idProducto);";

                using (var command = new NpgsqlCommand(sql, con))
                {


                    command.Parameters.AddWithValue(":p_idProducto", IdProducto);
                    command.ExecuteNonQuery();

                }

                return true;
            }

        }


        //Activa un producto por su id
        public Boolean ActivarProducto(int IdProducto)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_activar_producto(@p_idProducto);";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue(":p_idProducto", IdProducto);
                    command.ExecuteNonQuery();

                }

                return true;
            }

        }


        /// <summary>
        /// Método para aumentar la cantidad de productos en inventario, recibe el id del producto y el proveedor
        /// y la cantidad que se desea aumentar a dicho producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idProveedor"></param>
        /// <param name="cantidad"></param>
        /// <returns>Boolean resultado</returns>
        public Boolean AgregarCantidad(int idProducto, int idProveedor,int cantidad)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "CALL products.pa_agregar_cantidad(@p_id_producto,@p_id_proveedor,@p_cantidad);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue(":p_id_producto", idProducto);
                    command.Parameters.AddWithValue(":p_id_proveedor", idProveedor);
                    command.Parameters.AddWithValue(":p_cantidad", cantidad);
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

    }
}
