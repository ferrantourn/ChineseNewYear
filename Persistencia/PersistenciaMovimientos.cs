﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ExcepcionesPersonalizadas;
using System.Data.SqlClient;
using System.Data;


namespace Persistencia
{

    public class PersistenciaMovimientos: IPersistenciaMovimientos
    {
        public void RealizarMovimiento (Movimiento m)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("AltaMovimiento", conexion, CommandType.StoredProcedure);

                SqlParameter _idSucursal = new SqlParameter("@IdSucursal", m.SUCURSAL.IDSUCURSAL);
                SqlParameter _tipo = new SqlParameter("@Tipo", m.TIPOMOVIMIENTO);
                SqlParameter _fecha = new SqlParameter("@Fecha", m.FECHA);
                SqlParameter _moneda = new SqlParameter("@Moneda", m.MONEDA);
                SqlParameter _viaWeb = new SqlParameter("@ViaWeb", m.VIAWEB);
                SqlParameter _monto = new SqlParameter("@Monto", m.MONTO);
                SqlParameter _ciUsuario = new SqlParameter("@CiUsuario", m.USUARIO.CI);
                SqlParameter _idCuenta = new SqlParameter("@IdCuenta", m.CUENTA.IDCUENTA);

                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_idSucursal);
                cmd.Parameters.Add(_tipo);
                cmd.Parameters.Add(_fecha);
                cmd.Parameters.Add(_moneda);
                cmd.Parameters.Add(_viaWeb);
                cmd.Parameters.Add(_monto);
                cmd.Parameters.Add(_ciUsuario);
                cmd.Parameters.Add(_idCuenta);

                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                    throw new ErrorSucursalNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -3)
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


        public void RealizarTransferencia(Movimiento morigen, Movimiento mdestino)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            conexion.Open();
            try
            {

                SqlCommand cmd = Conexion.GetCommand("spRealizarTransferencia", conexion, CommandType.StoredProcedure);

                SqlParameter _idSucursalOrigen = new SqlParameter("@IdSucursalOrigen", morigen.SUCURSAL.IDSUCURSAL);
                SqlParameter _idSucursalDestino = new SqlParameter("@IdSucursalDestino", mdestino.SUCURSAL.IDSUCURSAL);

                SqlParameter _moneda = new SqlParameter("@Moneda", morigen.MONEDA);
                SqlParameter _monto = new SqlParameter("@Monto", morigen.MONTO);
                SqlParameter _ciUsuario = new SqlParameter("@CiUsuario", morigen.USUARIO.CI);
                SqlParameter _idCuentaOrigen = new SqlParameter("@IdCuentaOrigen", morigen.CUENTA.IDCUENTA);
                SqlParameter _idCuentaDestino = new SqlParameter("@IdCuentaDestino", mdestino.CUENTA.IDCUENTA);


                SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _retorno.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(_idSucursalOrigen);
                cmd.Parameters.Add(_idSucursalDestino);

                cmd.Parameters.Add(_moneda);
                cmd.Parameters.Add(_monto);
                cmd.Parameters.Add(_ciUsuario);
                cmd.Parameters.Add(_idCuentaOrigen);
                cmd.Parameters.Add(_idCuentaDestino);


                cmd.Parameters.Add(_retorno);

                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -1)
                    throw new ErrorSucursalNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -2)
                    throw new ErrorUsuarioNoExiste();
                if (Convert.ToInt32(_retorno.Value) == -3)
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
    }

}
