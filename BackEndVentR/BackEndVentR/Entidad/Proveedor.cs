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

        public Proveedor(string nombre)
        {
            this.Nombre = nombre;
        }

        public Proveedor(int id_proveedor, string nombre, bool activo)
        {
            this.IdProveedor = id_proveedor;
            this.Nombre = nombre;
            this.activo = activo;
        }

        public Proveedor(int id_proveedor)
        {
            this.IdProveedor = id_proveedor;
          
        }
    }
}
