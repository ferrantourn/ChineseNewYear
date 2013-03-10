﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaSucursal
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
                //pc.AltaSucursal(c);
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
                //return pc.BuscaSucursal();
                return null;
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
                //pc.ModificarSucursal(c);
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

    }
}
