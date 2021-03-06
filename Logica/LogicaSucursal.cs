﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaSucursal : ILogicaSucursal
    {

        public List<Sucursal> ListarSucursales()
        {
            try
            {
                PersistenciaSucursal ps = new PersistenciaSucursal();
                return ps.ListarSucursales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaSucursal(Sucursal s)
        {
            try
            {
                PersistenciaSucursal pc = new PersistenciaSucursal();
                pc.AltaSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarSucursal(Sucursal s)
        {
            try
            {
                PersistenciaSucursal pc = new PersistenciaSucursal();
                //pc.EliminarSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sucursal BuscarSucursal(Sucursal s)
        {
            try
            {
                PersistenciaSucursal pc = new PersistenciaSucursal();
                return pc.BuscarSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarSucursal(Sucursal c)
        {
            try
            {
                PersistenciaSucursal pc = new PersistenciaSucursal();
                pc.ModificarSucursal(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Sucursal> ListadoProductividadComparativo (DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                PersistenciaSucursal ps = new PersistenciaSucursal();
                return ps.ListadoProductividadComparativo(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void ArqueoCaja (Empleado e, ref decimal saldoCajaDolares, ref decimal saldoCajaPesos,
            ref int cantTotalDepositos, ref int cantTotalRetiros, ref int cantTotalPagos)
        {
            try
            {
                PersistenciaSucursal ps = new PersistenciaSucursal();
                // ps.ArqueoCaja(DateTime.Now,"USD",e);
                // ps.ArqueoCaja(DateTime.Now,"UYU",e);

                ps.TotalesArqueoCaja(e, ref cantTotalDepositos, ref cantTotalRetiros, ref cantTotalPagos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
