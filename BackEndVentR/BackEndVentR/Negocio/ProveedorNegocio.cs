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

        public bool eliminarProveedor(int IdProveedor)
        {
            return proveedorDatos.eliminarProveedor(IdProveedor);
        }
        public bool modificarProveedor(Proveedor proveedor)
        {
            return proveedorDatos.modificarProveedor(proveedor);
        }

        public bool AgregarProveedor(Proveedor proveedor) {
            return proveedorDatos.AgregarProveedor(proveedor);
        }

        public IEnumerable<Proveedor> listarProveedoresActivos()
        {
            List<Proveedor> proveedors = proveedorDatos.listarProveedoresActivos();

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedors;

        }
        public IEnumerable<Proveedor> listarProveedores()
        {
            List<Proveedor> proveedors = proveedorDatos.listarProveedores();

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedors;

        }
        
        public Proveedor buscarProveedorActivo(int idProveedor)
        {
            Proveedor proveedor = proveedorDatos.buscarProveedorActivo(idProveedor);

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedor;

        }
        public Proveedor buscarProveedor(int idProveedor)
        {
            Proveedor proveedor = proveedorDatos.buscarProveedor(idProveedor);

            //TODO: verificar que solo retorne productos y proveedores activos 
            return proveedor;

        }

        //Activa un proveedor por su ID
        public Boolean ActivarProveedor(int IdProveedor)
        {
            return proveedorDatos.ActivarProveedor(IdProveedor);
        }
        //Desactiva un proveedor por su ID
        public Boolean DesactivarProveedor(int IdProveedor)
        {
            return proveedorDatos.DesactivarProveedor(IdProveedor);
        }

        
        /// <summary>
        /// Samuel Serrano Guerra
        /// Método que sincriniza las ventas
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <param name="llave"></param>
        /// <returns>Json para el proveedor</returns>
        public IEnumerable<SyncronizationType> sincronizarProveedor(int idProveedor, string llave)
        {
            return proveedorDatos.sincronizarProveedor(idProveedor, llave);
        }
    }
}
