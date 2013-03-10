using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace Logica
{
    interface ILogicaCotizacion
    {
        public List<Cotizacion> ListarCotizaciones();
        public void AltaCotizacion(Cotizacion s);
        public void EliminarCotizacion(Cotizacion s);
        public Cotizacion BuscarCotizacion(Cotizacion s);
        public void ActualizarCotizacion(Cotizacion s, Empleado e);


    }
}
