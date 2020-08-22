using Entidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dato
{
    public class ProveedorDatos
    {

        private Conexion conexion = new Conexion();
        public List<Proveedor> buscarProveedor(Proveedor provee)
        {

            List<Proveedor> proveedor = new List<Proveedor>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select id_proveedor,nombre,activo from products.Proveedor where activo<>false and id_proveedor=@idProveedor";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idProveedor", provee.IdProveedor);
                  
                   

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedor.Add(new Proveedor(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2))
                            );
                        }

                       
                    }
                    command.Prepare();
                }
            }
            return proveedor;
        }

        public List<Proveedor> listarProveedores()
        {

            List<Proveedor> proveedor = new List<Proveedor>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select id_proveedor,nombre,activo from products.Proveedor where activo<>false;";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedor.Add(new Proveedor(reader.GetInt32(0),reader.GetString(1), reader.GetBoolean(2))
                            );
                        }

                    }
                }
            }
            return proveedor;
        }


    }
}
