using System;
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
    }
}
