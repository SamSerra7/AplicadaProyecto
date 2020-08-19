using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dato
{
    class Conexion
    {
        public NpgsqlConnection GetConexion()
        {
            /*CREDENCIALES SAMUEL*/
            var cs = "Host=localhost;Port=5433;Username=samuel;Password=s.1234;Database=VentR";

            return new NpgsqlConnection(cs);
        }
    }
}
