using System;
using System.Collections.Generic;
using System.Text;
using Entidad;
using Npgsql;

namespace Dato
{
    public class UsuarioDatos
    {
        private Conexion conexion = new Conexion();

        public List<Usuario> listarUsuarios()
        {

            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_usuario,correo,id_rol FROM users.\"Usuario\"";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario(reader.GetInt32(0), reader.GetString(1),reader.GetInt32(2)));
                        }

                    }
                }
            }
            return usuarios;
        }
    }
}
