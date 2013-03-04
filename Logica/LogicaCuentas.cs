using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaCuentas
    {


        public void AltaCuenta(Cuenta c)
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                pc.AltaCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Cuenta> ListarCuentas()
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                return pc.ListarCuentas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarCuenta(Cuenta c)
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                pc.EliminarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cuenta BuscarCuenta(Cuenta c)
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                return pc.BuscarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCuenta(Cuenta c)
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                pc.ModificarCuenta(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
