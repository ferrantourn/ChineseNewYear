﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Persistencia
{
    public class PersistenciaPrestamo
    {
        public void AltaPrestamo(Prestamo P)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaPrestamo", conexion, CommandType.StoredProcedure);

                SqlParameter _IdCliente = new SqlParameter("@IdCliente", P.CLIENTE);
                SqlParameter _Fecha = new SqlParameter("@Fecha", P.FECHAEMITIDO);
                SqlParameter _Cuotas = new SqlParameter("@Cuotas", P.TOTALCUOTAS);
                SqlParameter _Moneda = new SqlParameter("@Moneda", P.MONEDA);
                SqlParameter _Monto = new SqlParameter("@Moneda", P.MONTO);
                SqlParameter _retorno = new SqlParameter("@Mont", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_IdCliente);
                cmd.Parameters.Add(_Fecha);
                cmd.Parameters.Add(_Cuotas);
                cmd.Parameters.Add(_Moneda);
                cmd.Parameters.Add(_Monto);
                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) <= -4)
                    throw new ErrorBaseDeDatos();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        /*CancelarPrestamo*/
        public void CancelarPrestamo(Prestamo P)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("CancelarPrestamo", conexion, CommandType.StoredProcedure);

                SqlParameter _IdSucursal = new SqlParameter("@IdSucursal", P.SUCURSAL);
                SqlParameter _IdPrestamo = new SqlParameter("@NumeroPrestamo", P.IDPRESTAMO);
                SqlParameter _retorno = new SqlParameter("@Mont", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_IdSucursal);
                cmd.Parameters.Add(_IdPrestamo);

                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                {
                    throw new ErrorSucursalNoExiste();
                }
                else if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioNoExiste();
                else if (Convert.ToInt32(_retorno.Value) <= -4)
                    throw new ErrorBaseDeDatos();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
       
        public List<Prestamo> ListarPrestamos(Sucursal s, bool cancelado)
        {

            List<Prestamo> listaPrestamos = new List<Prestamo>();
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarPrestamos", conexion, CommandType.StoredProcedure);
            SqlParameter _numSucursal = new SqlParameter("@IdSucursal", s.IDSUCURSAL);
            SqlParameter _cancelado = new SqlParameter("@Cancelado", cancelado);
            cmd.Parameters.Add(_numSucursal);
            cmd.Parameters.Add(_cancelado);
            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                _Reader = cmd.ExecuteReader();
                int _idEmpleado, _idPrestamo, _cuotasPrestamo, _idCliente;
                string _moneda;
                decimal _montoTotalPrestamo;
                DateTime _fechaEmitidoPrestamo;

                while (_Reader.Read())
                {
                    _fechaEmitidoPrestamo = Convert.ToDateTime(_Reader["Fecha"]);
                    _idPrestamo = Convert.ToInt32(_Reader["IdPrestamo"]);
                    _cuotasPrestamo = Convert.ToInt32(_Reader["Cuotas"]);
                    _idCliente = Convert.ToInt32(_Reader["IdCliente"]);
                    _moneda = Convert.ToString(_Reader["Moneda"]);
                    _montoTotalPrestamo = Convert.ToDecimal(_Reader["Monto"]);
                    _idEmpleado = Convert.ToInt32(_Reader["IdEmpleado"]);

                    Prestamo p = new Prestamo
                    {
                        CANCELADO = cancelado,
                        CLIENTE = new Cliente { CI = _idCliente },
                        FECHAEMITIDO = _fechaEmitidoPrestamo,
                        IDPRESTAMO = _idPrestamo,
                        MONEDA = _moneda,
                        MONTO = _montoTotalPrestamo,
                        SUCURSAL = s,
                        TOTALCUOTAS = _cuotasPrestamo
                    };

                    listaPrestamos.Add(p);
                }
                _Reader.Close();

                return listaPrestamos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        








































      

    }
}