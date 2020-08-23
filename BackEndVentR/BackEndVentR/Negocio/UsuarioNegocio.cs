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

        public IEnumerable<Usuario> listarUsuarios()
        {
            List<Usuario> usuarios = usuarioDatos.listarUsuarios();

            return usuarios;

        }
    }
}
