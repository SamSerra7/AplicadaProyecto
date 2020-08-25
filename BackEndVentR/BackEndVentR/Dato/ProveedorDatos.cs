﻿using Entidad;
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
        public Proveedor buscarProveedor(int idProveedor)
        {

            Proveedor proveedor = new Proveedor();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select id_proveedor,nombre,activo from products.proveedor where activo<>false and id_proveedor=@idProveedor";

                using (var command = new NpgsqlCommand(sql, con))
                {

                   command.Parameters.AddWithValue("@idProveedor", idProveedor);
                  
                   

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedor = new Proveedor(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2));
                        }

                       
                    }
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
