using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
  public  class Proveedor{

        public int id_proveedor { get; set; }

        public string nombre { get; set; }

        public string cedula { get; set; }

        public int activo { get; set; }

        public Proveedor()
        {
        }

        public Proveedor(int id_proveedor, string nombre, string cedula, int activo)
        {
            this.id_proveedor = id_proveedor;
            this.nombre = nombre;
            this.cedula = cedula;
            this.activo = activo;
        }
    }
}
