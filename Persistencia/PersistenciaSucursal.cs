using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;
using ExcepcionesPersonalizadas;

namespace Persistencia
{
    public class PersistenciaSucursal
    {


        public void AltaSucursal(Sucursal L)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("AltaSucursal", conexion, CommandType.StoredProcedure);


            SqlParameter _nombre = new SqlParameter("@Nombre", L.NOMBRE);
            SqlParameter _direccion = new SqlParameter("@Direccion", L.DIRECCION);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };


            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_direccion);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) < 0)
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

        public void ModificarSucursal(Sucursal L)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spModificarSucursal", conexion, CommandType.StoredProcedure);

            SqlParameter _IdSucursal = new SqlParameter("@IdSucursal", L.NOMBRE);
            SqlParameter _nombre = new SqlParameter("@Nombre", L.NOMBRE);
            SqlParameter _direccion = new SqlParameter("@Direccion", L.DIRECCION);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };


            cmd.Parameters.Add(_IdSucursal);
            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_direccion);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) < 0)
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

        /// <summary>
        /// LISTA LAS SUCURSALES DEL BANCO
        /// </summary>
        /// <returns></returns>
        public List<Sucursal> ListarSucursales()
        {
            List<Sucursal> _listaSucursales = new List<Sucursal>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarSucursal", conexion, CommandType.StoredProcedure);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _idSucursal;
                string _nombre, _direccion;
                bool _activa;

                while (_Reader.Read())
                {
                    _idSucursal = (int)_Reader["IdSucursal"];
                    _nombre = (string)_Reader["Nombre"];
                    _direccion = (string)_Reader["Direccion"];
                    _activa = (bool)_Reader["Activa"]; 

                    Sucursal s = new Sucursal
                    {
                        IDSUCURSAL = _idSucursal,
                        NOMBRE = _nombre,
                        DIRECCION = _direccion,
                        ACTIVA = _activa
                    };

                    _listaSucursales.Add(s);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return _listaSucursales;
        }

        public List<Sucursal> ListadoProductividadComparativo(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Sucursal> _listaSucursales = new List<Sucursal>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListadoProductividadComparativo", conexion, CommandType.StoredProcedure);
            SqlParameter _fechaInicio = new SqlParameter("@FechaInicio", fechaInicio);
            SqlParameter _fechaFin = new SqlParameter("@FechaFin", fechaFin);
            cmd.Parameters.Add(_fechaInicio);
            cmd.Parameters.Add(_fechaFin);


            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                string _nombre, _direccion;
                int _cantidadCuentas, _cantidadPrestamos;

                while (_Reader.Read())
                {
                    _nombre = (string)_Reader["Nombre"];
                    _direccion = (string)_Reader["Direccion"];
                    _cantidadCuentas = Convert.ToInt32(_Reader["CantCuentasAbiertas"]);
                    _cantidadPrestamos = Convert.ToInt32(_Reader["CantPrestamosOtorgados"]);


                    Sucursal s = new Sucursal
                    {
                        NOMBRE = _nombre,
                        DIRECCION = _direccion,
                        CANTIDADCUENTASABIERTAS = _cantidadCuentas,
                        CANTIDADPRESTAMOS = _cantidadPrestamos
                    };

                    _listaSucursales.Add(s);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return _listaSucursales;
        }
    }
}
