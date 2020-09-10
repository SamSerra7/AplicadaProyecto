using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
    public class SyncronizationType
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public SyncronizationType(int idProducto,int cantidad)
        {
            IdProducto = idProducto;
            Cantidad = cantidad;
        }
    }
}
