using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Rol
    {
        public int Id_Rol { get; set; }
        public string Nombre { get; set; }

        public Rol()
        {
        }

        public Rol(int id_rol,string nombre)
        {
            this.Id_Rol = id_rol;
            this.Nombre = nombre;
        }

    }
}
