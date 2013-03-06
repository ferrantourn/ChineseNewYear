using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaPrestamo
    {

        public List<Prestamo> ListarPrestamo()
        {
            try
            {
                PersistenciaPrestamo ps = new PersistenciaPrestamo();
                //return ps.ListarPrestamo();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaPrestamo(Prestamo s)
        {
            try
            {
                PersistenciaPrestamo pc = new PersistenciaPrestamo();
                //pc.AltaPrestamo(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarPrestamo(Prestamo s)
        {
            try
            {
                PersistenciaPrestamo pc = new PersistenciaPrestamo();
                //pc.EliminarPrestamo(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Prestamo BuscarPrestamo(Prestamo s)
        {
            try
            {
                PersistenciaPrestamo pc = new PersistenciaPrestamo();
                //return pc.BuscaPrestamo();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarPrestamo(Prestamo c)
        {
            try
            {
                PersistenciaPrestamo pc = new PersistenciaPrestamo();
                //pc.ModificarPrestamo(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
