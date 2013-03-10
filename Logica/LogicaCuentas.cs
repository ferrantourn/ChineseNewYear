using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using Persistencia;
using ExcepcionesPersonalizadas;

namespace Logica
{
    public class LogicaCuentas : ILogicaCuentas
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


        public List<Cuenta> ListarCuentasCliente(Cliente c)
        {
            try
            {
                PersistenciaCuentas pc = new PersistenciaCuentas();
                return pc.BuscarCuentasCliente(c);
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


        public void RealizarMovimiento(Movimiento m)
        {
            try
            {
                LogicaCuentas lcuentas = new LogicaCuentas();
                Cuenta cuenta = lcuentas.BuscarCuenta(m.CUENTA);

                if (cuenta != null)
                {
                    PersistenciaMovimientos pc = new PersistenciaMovimientos();

                    if (m.CUENTA.MONEDA != m.MONEDA)
                    {
                        //OBTENEMOS COTIZACION
                        //--------------------
                        LogicaCotizacion lc = new LogicaCotizacion();
                        Cotizacion c = new Cotizacion();
                        c.FECHA = DateTime.Now;
                        c = lc.BuscarCotizacion(c);

                        if (c != null)
                        {
                            decimal montoMovimiento;
                            if (m.CUENTA.MONEDA == "USD")
                            {
                                //la cuenta esta en dolares y el deposito se esta haciendo en pesos
                                montoMovimiento = m.MONTO / c.PRECIOVENTA;
                            }
                            else
                            {
                                //la cuenta esta en pesos y el deposito se esta haciendo en dolares
                                montoMovimiento = m.MONTO * c.PRECIOCOMPRA;
                            }

                            //actualizamos el nuevo monto del movimiento;
                            //-------------------------------------------
                            m.MONTO = montoMovimiento;
                        }
                    }

                    if (m.MONTO > cuenta.SALDO)
                    {
                        throw new ErrorSaldoInsuficienteParaRetiro(); 
                    }

                    pc.RealizarMovimiento(m);
                }
            }
            catch (ErrorSaldoInsuficienteParaRetiro ex)
            {
                throw ex;
            }
            catch (ErrorSucursalNoExiste ex)
            {
                throw ex;
            }
            catch (ErrorUsuarioNoExiste ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
