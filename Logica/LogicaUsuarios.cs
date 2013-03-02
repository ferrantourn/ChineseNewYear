using System;
using System.Collections.Generic;
using Entidades;
using Persistencia;

namespace Logica
{
    public class LogicaUsuarios
    {

        public void AltaUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    PersistenciaClientes pclientes = new PersistenciaClientes();
                    pclientes.AltaCliente((Cliente)u);
                }
                else
                {
                    PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    pempleados.AltaEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ActualizarUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    PersistenciaClientes pclientes = new PersistenciaClientes();
                    //pclientes.AltaCliente((Cliente)u);
                }
                else
                {
                    PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    //pempleados.AltaEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EliminarUsuario(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    PersistenciaClientes pclientes = new PersistenciaClientes();
                    //pclientes.AltaCliente((Cliente)u);
                }
                else
                {
                    PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    //pempleados.AltaEmpleado((Empleado)u);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Cliente> ListarClientes ()
        {
            try
            {
                PersistenciaClientes pclientes = new PersistenciaClientes();
                return pclientes.ListarClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Usuario BuscarUsuarioPorCi(Usuario u)
        {
            try
            {
                if (u is Cliente)
                {
                    PersistenciaClientes pclientes = new PersistenciaClientes();
                    return pclientes.BuscarClientePorCi((Cliente)u);
                }
                else
                {
                    //PersistenciaEmpleados pempleados = new PersistenciaEmpleados();
                    //pempleados.AltaEmpleado((Empleado)u);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
