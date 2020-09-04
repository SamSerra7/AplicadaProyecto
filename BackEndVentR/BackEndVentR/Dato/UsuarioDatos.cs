using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using Entidad;
using Npgsql;
using NpgsqlTypes;

namespace Dato
{
    public class UsuarioDatos
    {
        private Conexion conexion = new Conexion();
        private CarritoComprasDatos carritoCompraDatos = new CarritoComprasDatos();

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

        public bool AgregarUsuario(Usuario usuario)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call users.pa_insertar_usuario(@u_correo,@u_contrasennia,@u_idrol);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    

                        command.Parameters.AddWithValue(":u_correo", usuario.Correo);
                        command.Parameters.AddWithValue(":u_contrasennia", usuario.Contrasennia);
                        command.Parameters.AddWithValue(":u_idrol", usuario.Id_Rol);
                        command.ExecuteNonQuery();
                    
                }

                return true;
            }
        }

        public bool ModificarUsuario(Usuario usuario)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call users.pa_actualizar_usuario(@u_id,@u_correo,@u_contras,@u_rol);";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue(":u_id", usuario.Id_Usuario);
                    command.Parameters.AddWithValue(":u_correo", usuario.Correo);
                    command.Parameters.AddWithValue(":u_contras", usuario.Contrasennia);
                    command.Parameters.AddWithValue(":u_rol", usuario.Id_Rol);
                    command.ExecuteNonQuery();

                }

                return true;
            }
        }

        public bool EliminarUsuario(int id)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call users.pa_eliminar_usuario(@u_id);";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue(":u_id", id);
                    command.ExecuteNonQuery();

                }
                return true;
            }
        }

        public Usuario BuscarUsuarioEspecifico(int Id_U)
        {
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_usuario,correo,contrasennia,id_rol FROM users.usuario where id_usuario=@id_u";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@id_u", Id_U);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
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


        public Boolean registrarVenta(int idUsuario)
        {
            Decimal facturaTotal = carritoCompraDatos.venderProductoCarrito(idUsuario);
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "call pa_registrar_venta(@idUsuario, @totalFactura)";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@totalFactura", facturaTotal);
                    int result = command.ExecuteNonQuery();

                    if (result == -1)
                        return true;
                    else
                        return false;


                }
            }
        }

    }



}


