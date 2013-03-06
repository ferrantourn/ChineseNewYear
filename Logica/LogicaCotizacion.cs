using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaCotizacion
    {

        public List<Cotizacion> ListarCotizaciones()
        {
            try
            {
                PersistenciaCotizacion ps = new PersistenciaCotizacion();
                return ps.ListarCotizaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaCotizacion(Cotizacion s)
        {
            try
            {
                PersistenciaCotizacion pc = new PersistenciaCotizacion();
                //pc.AltaSucursal(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarCotizacion(Cotizacion s)
        {
            try
            {
                PersistenciaCotizacion pc = new PersistenciaCotizacion();
                //pc.EliminarSucursal(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cotizacion BuscarCotizacion(Cotizacion s)
        {
            try
            {
                PersistenciaCotizacion pc = new PersistenciaCotizacion();
                //return pc.BuscaSucursal();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCotizacion(Cotizacion c)
        {
            try
            {
                PersistenciaCotizacion pc = new PersistenciaCotizacion();
                //pc.ModificarSucursal(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
