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

        /// <summary>
        /// Samuel Serrano 
        /// Método que obtiene todos los productos de la BD
        /// </summary>
        /// <returns>Products List</returns>
        public List<Producto> listarProductos()
        {

            List<Producto> productos = new List<Producto>();

            using(NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_producto,nombre,precio,url_img,detalle,activo,cantidad FROM products.\"Producto\"";
                
                using(var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add( new Producto(reader.GetInt32(0), reader.GetString(1),
                                                            reader.GetDecimal(2), reader.GetString(3), 
                                                                reader.GetString(4),reader.GetBoolean(5),
                                                                    reader.GetInt32(6))
                            );
                        }

                    }
                }
            }
            return productos;
        }



    }
}
