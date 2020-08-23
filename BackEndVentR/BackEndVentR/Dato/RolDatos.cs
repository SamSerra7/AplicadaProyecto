using System;
using System.Collections.Generic;
using System.Text;
using Entidad;
using Npgsql;

namespace Dato
{
    public class RolDatos
    {
        private Conexion conexion = new Conexion();

        public List<Rol> listarRoles()
        {

            List<Rol> roles = new List<Rol>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_rol,nombre FROM users.\"Rol\"";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Rol(reader.GetInt32(0), reader.GetString(1)));
                        }

                    }
                }
            }
            return roles;
        }
    }
}
