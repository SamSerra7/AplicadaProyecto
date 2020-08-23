using System;
using System.Collections.Generic;
using System.Text;
using Dato;
using Entidad;

namespace Negocio
{
    public class RolNegocio
    {

        private RolDatos rolDatos = new RolDatos();

        public IEnumerable<Rol> listarRoles()
        {
            List<Rol> roles = rolDatos.listarRoles();

            return roles;

        }

    }
}
