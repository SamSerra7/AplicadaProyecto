using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
  public  class Proveedor{

        private int id_proveedor;
        public int IdProveedor
        {
            get { return id_proveedor; }
            set
            {
                if (value < 0) throw new Exception("El id no puede ser negativo");
                id_proveedor = value;
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) throw new Exception("El nombre no puede ser vacío");
                nombre = value;
            }
        }


        public bool activo { get; set; }

        public Proveedor()
        {
        }

        public Proveedor(int id_proveedor, string nombre, bool activo)
        {
            this.id_proveedor = id_proveedor;
            this.nombre = nombre;
            this.activo = activo;
        }
    }
}
