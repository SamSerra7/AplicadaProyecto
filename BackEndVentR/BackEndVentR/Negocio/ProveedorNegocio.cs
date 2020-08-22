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
        public Proveedor buscarProveedor(int idProveedor)
        {
            Proveedor proveedor = proveedorDatos.buscarProveedor(idProveedor);

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedor;

        }

    }
}
