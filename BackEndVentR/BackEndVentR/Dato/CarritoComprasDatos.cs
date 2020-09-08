using Entidad;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dato
{
    public class CarritoComprasDatos
    {
        private Conexion conexion = new Conexion();
        private ProductoDatos productoDato = new ProductoDatos();


        public void disminuir_cantidad(int idProducto, int idUsuario)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                try
                {
                    con.Open();
                    string sql = "call products.pa_disminuir_cantidad(@idUsuario,@idProducto); ";

                    using (var command = new NpgsqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idProducto", idProducto);

                        int result = command.ExecuteNonQuery();

                    }

                }
                catch (Exception)
                {
                  
                }
            }
        }

        public void agregar_cantidad(int idProducto, int idUsuario)
        {

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                try
                {
                    con.Open();
                    string sql = "call  products.pa_aumentar_cantidad(@idUsuario,@idProducto); ";

                    using (var command = new NpgsqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idProducto", idProducto);
                        
                        int result = command.ExecuteNonQuery();

                     
                    }

                }
                catch (Exception)
                {
                }
            }
        }

        public bool agregar_producto_carrito(int idUsuario, CarritoComprasProducto carrito)
        {
            
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                try
                {
                    con.Open();
                    string sql = "call  products.pa_agregar_carrito_compras(@idUsuario,@idProducto,@cantidad_agregada); ";

                    using (var command = new NpgsqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@idProducto", carrito.idProducto);
                        command.Parameters.AddWithValue("@cantidad_agregada", carrito.Cantidad);

                        int result = command.ExecuteNonQuery();

                        if (result == -1)
                            return true;
                        else
                            return false;
                    }

                }
                catch (Exception) {
                    return false;
                }
            }
        }

        public List<ProductoCantidad> buscar_carrito_compras(int id_usuario)
        {


            List<ProductoCantidad> carrito_compras = new List<ProductoCantidad>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "select id_carrito_compras from products.carrito_compras where id_usuario = @id_Usuario";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@id_Usuario", id_usuario);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            carrito_compras = new List<ProductoCantidad>(buscar_carrito_compras_producto(reader.GetInt32(0)));
                        }

                    }
                }
            }
            return carrito_compras;
        }



        public List <ProductoCantidad> buscar_carrito_compras_producto(int id_carrito_compras)
        {

            List<ProductoCantidad> lista_producto_cantidad = new List<ProductoCantidad>();

            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "select id_producto,cantidad from products.carrito_compras_producto where id_carrito_compras = @id_carrito_compras ";

                using (var command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@id_carrito_compras", id_carrito_compras);


                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto  obtener_Producto= productoDato.buscarProducto(reader.GetInt32(0));
                           
                           lista_producto_cantidad.Add( new ProductoCantidad(obtener_Producto,reader.GetInt32(1)));
                          

                        }

                    }
                }
            }
            return lista_producto_cantidad;
        }


        public Decimal venderProductoCarrito(int idUsuario)
        {


            using (NpgsqlConnection con = conexion.GetConexion())
            {
                con.Open();
                string sql = "Select pa_venderProductoCarrito(@idUsuario)";

                using (var command = new NpgsqlCommand(sql, con))
                {

                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    Decimal total = (decimal)command.ExecuteScalar();
                    command.ExecuteNonQuery();
                    return total;

                }
            }
        }

    }
}
