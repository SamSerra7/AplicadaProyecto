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


        //Agrega un nuevo proveedor con el Nombre, el id lo genera la base y el estado es default true
        public bool AgregarProveedor(Proveedor proveedor)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_agregar_proveedor(@p_nombre);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    

                        command.Parameters.AddWithValue(":p_nombre",proveedor.Nombre );
                        command.ExecuteNonQuery();
                    
                    
                }
                return true;
            }
        }

        //Devuelve true o false, verifica si el proveedor

        public bool validarProveedor(int idproveedor, string llave_proveedor)
        {


            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "select products.verificadorProveedor(@idproveedor,@llave_proveedor)";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idproveedor", idproveedor);
                    command.Parameters.AddWithValue("@llave_proveedor", llave_proveedor);

                    bool resultado = (bool)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    return resultado;

                }
            }
        }

        public bool modificarProveedor(Proveedor proveedor)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_modificar_proveedor(@p_idproveedor,@p_nombre);";

                using (var command = new NpgsqlCommand(sql, con))
                {


                    command.Parameters.AddWithValue(":p_idproveedor", proveedor.IdProveedor);
                    command.Parameters.AddWithValue(":p_nombre", proveedor.Nombre);
                    int result = command.ExecuteNonQuery();

                    if (result == -1)
                        return true;
                    else
                        return false;

                }
            }
        }

        
        public bool eliminarProveedor(int IdProveedor)
        {

            try
            {
                using (NpgsqlConnection con = conexion.GetConexion())
                {
                    con.Open();
                    string sql = "Call products.pa_eliminar_proveedor(@p_idproveedor);";

                    using (var command = new NpgsqlCommand(sql, con))
                    {


                        command.Parameters.AddWithValue("@p_idproveedor", IdProveedor);

                        int result = command.ExecuteNonQuery();

                        if (result == -1)
                            return true;
                        else
                            return false;


                    }

                }
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que retorna de la tabla de Sincronizacion
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <param name="llave"></param>
        /// <returns></returns>
        public List<SyncronizationType> sincronizarProveedor(int idProveedor, string llave)
        {
            List<SyncronizationType> datosSinc = new List<SyncronizationType>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_sincronizacion(@idProveedor,@Llave);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            datosSinc.Add(
                                new SyncronizationType(reader.GetInt32(0), reader.GetInt32(1))
                            );
                        }

                    }
                }
            }
            return datosSinc;
        }

        public Proveedor buscarProveedorActivo(int idProveedor)
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
        public Proveedor buscarProveedor(int idProveedor)
        {

            Proveedor proveedor = new Proveedor();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select id_proveedor,nombre,activo from products.proveedor where   id_proveedor=@idProveedor";

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
        public List<Proveedor> listarProveedoresActivos()
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
                            proveedor.Add(new Proveedor(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2))
                            );
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
                string sql = "Select id_proveedor,nombre,activo from products.Proveedor;";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedor.Add(new Proveedor(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2))
                            );
                        }

                    }
                }
            }
            return proveedor;
        }

        //Desactiva un proveedor por usando su ID
        public Boolean DesactivarProveedor(int IdProveedor)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_desactivar_proveedor(@p_idProveedor);";

                using (var command = new NpgsqlCommand(sql, con))
                {

                   
                        command.Parameters.AddWithValue(":p_idProveedor", IdProveedor);
                        command.ExecuteNonQuery();
                        
                   
                }

                return true;
            }

        }

        //Activa un proveedor por usando su ID
        public Boolean ActivarProveedor(int IdProveedor)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Call products.pa_activar_proveedor(@p_idProveedor);";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    

                        command.Parameters.AddWithValue(":p_idProveedor", IdProveedor);
                        command.ExecuteNonQuery();
                       
                   
                }

                return true;
            }

        }


    }
}
