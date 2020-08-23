using Entidad;
using Npgsql;
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
                            if(reader.GetDate(0).CompareTo(DateTime.Now)>0 )
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
        /// Método que crea en la BD una llave para un proveedor
        /// </summary>
        /// <param name="idProveedor"></param>
        public void crearLlave(int idProveedor)
        {
            string cod = (idProveedor * 7 + 5 * 143 - 3) + "salllave";
            string hash = cod.GetHashCode().ToString();
            Llave llave = new Llave(hash, proveedorDatos.buscarProveedor(idProveedor), DateTime.Now.AddDays(3));
            
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "INSERT INTO products.llave (id_proveedor,llave,fecha_vencimiento) VALUES (@idProveedor,@llave,@fechaVencimiento)";

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
