using System.Collections.Generic;

namespace Entidades
{
    public class Cuenta
    {
        private int _idCuenta;
        public int IDCUENTA { get; set; }

        private Sucursal _sucursal;
        public Sucursal SUCURSAL { get; set; }

        private string _moneda;
        public string MONEDA { get; set; }

        private Cliente _cliente;
        public Cliente CLIENTE { get; set; }

        private decimal _saldo;
        public decimal SALDO { get; set; }

        private List<Movimiento> _movimientosCuenta;
        public List<Movimiento> MOVIMIENTOSCUENTA { get; set; }

        public override string ToString() 
        {
            return "Numero cuenta:"  + " " + IDCUENTA + " - " + "Moneda: " + MONEDA + " - " + "Saldo: " + SALDO;
        }
        public string TOSTRING
        {
            get
            {
                return ToString();
            }
        }

    }
}
