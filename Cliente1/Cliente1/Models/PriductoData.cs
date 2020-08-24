using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cliente1.Models
{
    public class PriductoData
    {
        private SqlConnection _con;

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
            _con = new SqlConnection(constr); 
        }

        public bool Add(Producto producto)
        {
            Connection();

            int i;

            using (SqlCommand command = new SqlCommand("InsertProduct", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre",producto.nombre);
                command.Parameters.AddWithValue("@precio", producto.precio);
                command.Parameters.AddWithValue("@url_img", producto.url_img);
                command.Parameters.AddWithValue("@descripcion", producto.descripcion);

                _con.Open();

                i = command.ExecuteNonQuery();

            }
            _con.Close();

            return i >= 1;

        }

        public List<Producto> obtenerProductos()
        {
            Connection();
            List<Producto> productolista = new List<Producto>();

            using (SqlCommand command = new SqlCommand("SelectProduct", _con))
            {
                command.CommandType = CommandType.StoredProcedure;
                _con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Producto p = new Producto()
                    {
                        id_produto = Convert.ToInt32(reader["id_producto"]),
                        nombre = Convert.ToString(reader["nombre"]),
                        precio = Convert.ToInt32(reader["precio"]),
                        url_img = Convert.ToString(reader["url_img"]),
                        cantidad = Convert.ToInt32(reader["cantidad"]),
                        estado = Convert.ToByte(reader["estado"]),
                        descripcion = Convert.ToString(reader["descripcion"]),
                    };
                    productolista.Add(p);
                }
                _con.Close();
                return productolista;
            }
        }
    }
}