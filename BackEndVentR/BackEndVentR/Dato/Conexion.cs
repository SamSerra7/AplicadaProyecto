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
            /*CREDENCIALES SAMUEL
            var cs = "Host=localhost;Port=5433;Username=samuel;Password=s.1234;Database=VentR";
        
             */   
            
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=s.1234;Database=VentR";
  




            /*CREDENCIALES Karol
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=VentR";
            
            
            var cs = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=VentR";
            */
            return new NpgsqlConnection(cs);
        }
    }
}
