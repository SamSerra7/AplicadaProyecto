using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Entidad
{
    public class Llave
    {

        private int id_llave;

        public int IdLlave
        {
            get { return id_llave; }
            set {
                if (value < 0) throw new Exception("El id no puede ser negativo");
                id_llave = value; 
            }
        }

        private string cod_llave;

        public string CodLlave
        {
            get { return cod_llave; }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) throw new Exception("La llave no puede estar vacía");
                cod_llave = value; 
            }
        }


        private Proveedor proveedor;

        public Proveedor Proveedor
        {
            get { return proveedor; }
            set { proveedor = value ?? throw new Exception("Debe tener un proveedor"); }
        }

        private DateTime fechaVencimiento;

        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set {
                if (value == null) throw new Exception("Debe tener fecha de vencimiento");    
                fechaVencimiento = value; 
            }
        }

        public Llave(int idLlave, string codLlave, Proveedor proveedor, DateTime fechaVencimiento)
        {
            IdLlave = idLlave;
            CodLlave = codLlave;
            Proveedor = proveedor;
            FechaVencimiento = fechaVencimiento;
        }

        public Llave(string codLlave, Proveedor proveedor, DateTime fechaVencimiento)
        {
            CodLlave = codLlave;
            Proveedor = proveedor;
            FechaVencimiento = fechaVencimiento;
        }

    }
}
