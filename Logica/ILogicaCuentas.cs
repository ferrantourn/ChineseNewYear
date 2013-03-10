using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaCuentas
    {
        public void AltaCuenta(Cuenta c);
        public List<Cuenta> ListarCuentas();
        public List<Cuenta> ListarCuentasCliente(Cliente c);
        public void EliminarCuenta(Cuenta c);
        public Cuenta BuscarCuenta(Cuenta c);
        public void ActualizarCuenta(Cuenta c);
        public void RealizarMovimiento(Movimiento m);

    }
}
