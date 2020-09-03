using System;
using System.Collections.Generic;
using System.Text;
using Dato;
using Entidad;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private UsuarioDatos usuarioDatos = new UsuarioDatos();


        
        public bool AgregarUsuario(Usuario usuario)
        {
            return usuarioDatos.AgregarUsuario(usuario);
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            return usuarioDatos.ModificarUsuario(usuario);
        }

        public bool EliminarUsuario(int idUsuario)
        {
            return usuarioDatos.EliminarUsuario(idUsuario);
        }

        public Usuario BuscarUsuarioEspecifico(int Id_U)
        {
            return usuarioDatos.BuscarUsuarioEspecifico(Id_U);
        }

        public IEnumerable<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = usuarioDatos.ListarUsuarios();

            return usuarios;

        }

        

        public Boolean VerificarUsuario(Usuario usuario)
        {
            return usuarioDatos.VerificarUsuario(usuario.Correo, usuario.Contrasennia);
        }


        public Boolean registrarVenta(int idUsuario)
        {
            return usuarioDatos.registrarVenta(idUsuario);
        }
    }
}

