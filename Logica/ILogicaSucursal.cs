using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaSucursal
    {
        public List<Sucursal> ListarSucursales();
        public void AltaSucursal(Sucursal s);
        public void EliminarSucursal(Sucursal s);
        public Sucursal BuscarSucursal(Sucursal s);
        public void ActualizarSucursal(Sucursal c);
        public List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin);
        public void ArqueoCaja(Empleado e, ref decimal saldoCajaDolares, ref decimal saldoCajaPesos,
            ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos);
    }
}
