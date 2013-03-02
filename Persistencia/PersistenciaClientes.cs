﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using ExcepcionesPersonalizadas;

namespace Persistencia
{
    public class PersistenciaClientes
    {


        /// <summary>
        /// Ingresa un nuevo cliente en el sistema
        /// </summary>
        /// <param name="u"></param>
        public void AltaCliente(Cliente c)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("AltaCliente", conexion, CommandType.StoredProcedure);

            SqlParameter _ci = new SqlParameter("@Ci", c.CI);
            SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", c.NOMBREUSUARIO);
            SqlParameter _nombre = new SqlParameter("@Nombre", c.NOMBRE);
            SqlParameter _apellido = new SqlParameter("@Apellido", c.APELLIDO);
            SqlParameter _pass = new SqlParameter("@Pass", c.PASS);
            SqlParameter _direccion = new SqlParameter("@Direccion", c.DIRECCION);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(_ci);
            cmd.Parameters.Add(_nombreusuario);
            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_apellido);
            cmd.Parameters.Add(_pass);
            cmd.Parameters.Add(_direccion);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) == -3)
                    throw new ErrorUsuarioYaExiste();
                else if (Convert.ToInt32(_retorno.Value) < 0)
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


        public Cliente BuscarClientePorCi(Cliente cliente)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);

            try
            {
                SqlCommand cmd = Conexion.GetCommand("spBuscarClientePorCi", conexion, CommandType.StoredProcedure);
                SqlParameter _Ci = new SqlParameter("@Ci", cliente.CI);
                cmd.Parameters.Add(_Ci);

                SqlDataReader reader;
                Cliente c = null;
                int _ci;
                string _nombreusuario, _nombre, _apellido, _password, _direccion;


                conexion.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _ci = (int)reader["Ci"];
                    _nombre = (string)reader["Nombre"];
                    _nombreusuario = (string)reader["NombreUsuario"];
                    _apellido = (string)reader["Apellido"];
                    _password = (string)reader["Pass"];
                    _direccion = (string)reader["Direccion"];
                    c = new Cliente
                            {
                                CI = _ci,
                                NOMBREUSUARIO = _nombreusuario,
                                NOMBRE = _nombre,
                                APELLIDO = _apellido,
                                PASS = _password,
                                DIRECCION = _direccion
                            };
                }
                reader.Close();

                return c;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }
            finally
            {
                conexion.Close();
            }


        }


        /// <summary>
        /// LISTA LOS CLIENTES
        /// </summary>
        /// <returns></returns>
        public List<Cliente> ListarClientes()
        {
            List<Cliente> _listaClientess = new List<Cliente>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarClientes", conexion, CommandType.StoredProcedure);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                int _ci;
                string _nombreUsuario, _nombre, _apellido, _pass;
                bool _activo;

                while (_Reader.Read())
                {
                    _ci = (int)_Reader["Ci"];
                    _nombreUsuario = (string)_Reader["NombreUsuario"];
                    _nombre = (string)_Reader["Nombre"];
                    _apellido = (string)_Reader["Apellido"];
                    _pass = (string)_Reader["Pass"];
                    _activo = (bool)_Reader["Activo"];

                    Cliente c = new Cliente
                    {
                        CI = _ci,
                        NOMBREUSUARIO = _nombreUsuario,
                        PASS = _pass,
                        NOMBRE = _nombre,
                        APELLIDO = _apellido,
                        ACTIVO = _activo

                    };

                    _listaClientess.Add(c);
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

            return _listaClientess;
        }

        //public void ModificarCliente (Cliente c)
        //{
        //    SqlConnection conexion = new SqlConnection(Conexion.Cnn);
        //    SqlCommand cmd = Conexion.GetCommand("spModificarDocente", conexion, CommandType.StoredProcedure);

        //    SqlParameter _ci = new SqlParameter("@Ci", u.CI);
        //    SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", u.NOMBRE_USUARIO);
        //    SqlParameter _nombre = new SqlParameter("@Nombre", u.NOMBRE);
        //    SqlParameter _apellido = new SqlParameter("@Apellido", u.APELLIDO);
        //    SqlParameter _pass = new SqlParameter("@Pass", u.PASS);
        //    SqlParameter _materias = new SqlParameter("@Materias", u.MateriasToString());
        //    SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);

        //    _retorno.Direction = ParameterDirection.ReturnValue;

        //    cmd.Parameters.Add(_ci);
        //    cmd.Parameters.Add(_nombreusuario);
        //    cmd.Parameters.Add(_nombre);
        //    cmd.Parameters.Add(_apellido);
        //    cmd.Parameters.Add(_pass);
        //    cmd.Parameters.Add(_materias);
        //    cmd.Parameters.Add(_retorno);


        //    try
        //    {
        //        conexion.Open();
        //        cmd.ExecuteNonQuery();

        //        if (Convert.ToInt32(_retorno.Value) == -1)
        //            throw new ErrorUsuarioNoExiste();

        //        if (Convert.ToInt32(_retorno.Value) == -2)
        //            throw new ErrorBaseDeDatos();

        //        if (Convert.ToInt32(_retorno.Value) <= -3)
        //            throw new ErrorBaseDeDatos();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }


        //}
    }
}
