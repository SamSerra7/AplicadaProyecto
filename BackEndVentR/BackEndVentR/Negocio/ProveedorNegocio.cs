using Dato;
using Entidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class ProveedorNegocio
    {

        private ProveedorDatos proveedorDatos = new ProveedorDatos();

        public IEnumerable<Proveedor> listarProveedores()
        {
            List<Proveedor> proveedors = proveedorDatos.listarProveedores();

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedors;

        }
        public IEnumerable<Proveedor> buscarProveedor(Proveedor proveedoor)
        {
            List<Proveedor> proveedors = proveedorDatos.buscarProveedor(proveedoor);

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedors;

        }

    }
}
