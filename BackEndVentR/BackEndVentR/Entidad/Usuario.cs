using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Correo { get; set; }
        public string Contrasennia { get; set; }
        public int Id_Rol { get; set; }

        public Usuario()
        {
              
        }

        public Usuario(string correo, string contrasennia, int rol)
        {

            this.Correo = correo;
            this.Contrasennia = contrasennia;
            this.Id_Rol = rol;
        }

        public Usuario(int id_Usuario, string correo, int rol)
        {
            this.Id_Usuario = id_Usuario;
            this.Correo = correo;
            this.Id_Rol = rol;
        }
    }
}
