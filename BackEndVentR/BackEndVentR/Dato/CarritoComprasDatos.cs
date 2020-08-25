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


        public bool agregar_producto_carrito(CarritoComprasProducto carritocompras)
        {
            
            using (NpgsqlConnection con = conexion.GetConexion())
            {
                try
                {
                    con.Open();
                    string sql = "call pa_insertar_producto_carrito_compras(@idCarritoCompras,@idProducto,@Cantidad); ";

                    using (var command = new NpgsqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@idCarritoCompras", carritocompras.idCarritoCompras);
                        command.Parameters.AddWithValue("@idProducto", carritocompras.idProducto);
                        command.Parameters.AddWithValue("@Cantidad", carritocompras.Cantidad);

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
                           
                           lista_producto_cantidad.Add( new ProductoCantidad(reader.GetInt32(0), reader.GetInt32(1),obtener_Producto));
                          

                        }

                    }
                }
            }
            return lista_producto_cantidad;
        }

     
    }
}
