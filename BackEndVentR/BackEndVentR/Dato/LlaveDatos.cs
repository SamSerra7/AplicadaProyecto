using Entidad;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Dato
{
    /// <summary>
    /// Clase que se conecta con la tabla Llave en la base de datos
    /// </summary>
    public class LlaveDatos
    {

        ///Variables globales
        private Conexion conexion = new Conexion();
        private ProveedorDatos proveedorDatos = new ProveedorDatos();


        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que verifica en la BD si algún proveedor específico tiene una llave activa
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>variable booleana</returns>
        public bool tieneLlaveActiva(int idProveedor)
        {
            bool llaveActiva = false;

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select fecha_vencimiento from products.llave where id_proveedor=@idProveedor";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idProveedor", idProveedor);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.GetDateTime(0).CompareTo(DateTime.Now)>0 )
                            {
                                llaveActiva = true;
                            }
                        }


                    }
                }
            }

            return llaveActiva;

        }
        
        /// <summary>
        /// Samuel Serrano Guerra
        /// Elimina las llaves inactivas
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>null Object</returns>
        public Llave eliminarLlave(int idProveedor)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "CALL products.pa_eliminar_llave_inactiva(@idProveedor);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@idProveedor", idProveedor);
                    command.ExecuteNonQuery();
                }
            }
            return null;
        }

        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que retorna la llave de un proveeddor específico
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns>objeto de tipo Llave</returns>
        public Llave llaveActiva(int idProveedor)
        {

            Llave llave = null;

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT * FROM products.pa_get_llave_proveedor(@idProveedor)";


                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idProveedor", idProveedor);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        
                            llave = new Llave(  reader.GetInt32(0),
                                                reader.GetString(1),
                                                proveedorDatos.buscarProveedor(reader.GetInt32(2)),
                                                reader.GetDateTime(3));
                        
                        }


                    }
                }
            }

            return llave;
        }


        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que crea en la BD una llave para un proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        public void crearLlave(int idProveedor)
        {
            string cod = (idProveedor * 7 + 5 * 143 - 3)+"trhdJHYGVhtrfc";
            string hash = cod.GetHashCode().ToString() + "salllave";
            Llave llave = new Llave(hash, proveedorDatos.buscarProveedor(idProveedor), DateTime.Now.AddDays(3));
            
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "CALL products.pa_crear_llave(@idProveedor,@llave,@fechaVencimiento);";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idProveedor", idProveedor);
                    command.Parameters.AddWithValue("@llave", llave.CodLlave);
                    command.Parameters.AddWithValue("@fechaVencimiento", llave.FechaVencimiento);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
