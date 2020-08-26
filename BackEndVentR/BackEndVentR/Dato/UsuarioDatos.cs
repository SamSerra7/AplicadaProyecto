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

        public List<Usuario> ListarUsuarios()
        {

            List<Usuario> usuarios = new List<Usuario>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_usuario,correo,id_rol FROM users.usuario";

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

        public string AgregarUsuario(Usuario usuario)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call users.pa_insertar_usuario(@u_correo,@u_contrasennia,@u_idrol);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    try
                    {

                        command.Parameters.AddWithValue(":u_correo", usuario.Correo);
                        command.Parameters.AddWithValue(":u_contrasennia", usuario.Contrasennia);
                        command.Parameters.AddWithValue(":u_idrol", usuario.Id_Rol);
                        command.ExecuteNonQuery();
                    }
                    catch (PostgresException ex)
                    {
                        if (ex.SqlState == "23505")
                        {                         

                            con.Close();
                            return "El correo ya se encuentra registrado";
                        }
                    }
                }
                con.Close();
                return "Registrado con exito";
            }
        }

        public Usuario BuscarUsuarioEspecifico(int Id_U)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_usuario,correo,id_rol FROM users.usuario where id_usuario=@id_u";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@id_u", Id_U);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }

                    }
                }
                con.Close();
            }
            return null;
        }

        public Boolean VerificarUsuario(string correo, string contrasennia)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_usuario FROM users.usuario where correo=@correo AND contrasennia=@contrasennia";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@correo", correo);
                    command.Parameters.AddWithValue("@contrasennia", contrasennia);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            con.Close();
                            return true;
                        }

                    }
                }
                con.Close();
            }
            return false;
        }

    }



}


