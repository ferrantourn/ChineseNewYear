using System;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using ExcepcionesPersonalizadas;

namespace Persistencia
{
    public class Empleados
    {


        /// <summary>
        /// Ingresa un nuevo Empleado en el sistema
        /// </summary>
        /// <param name="e"></param>
        public void AltaEmpleado(Empleado e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("AltaCliente", conexion, CommandType.StoredProcedure);

            SqlParameter _ci = new SqlParameter("@Ci", e.CI);
            SqlParameter _nombreusuario = new SqlParameter("@NombreUsuario", e.NOMBREUSUARIO);
            SqlParameter _nombre = new SqlParameter("@Nombre", e.NOMBRE);
            SqlParameter _apellido = new SqlParameter("@Apellido", e.APELLIDO);
            SqlParameter _pass = new SqlParameter("@Pass", e.PASS);
            SqlParameter _idSucursal = new SqlParameter("@IdSucursal", e.SUCURSAL.IDSUCURSAL);
            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int)
                                        {Direction = ParameterDirection.ReturnValue};

            cmd.Parameters.Add(_ci);
            cmd.Parameters.Add(_nombreusuario);
            cmd.Parameters.Add(_nombre);
            cmd.Parameters.Add(_apellido);
            cmd.Parameters.Add(_pass);
            cmd.Parameters.Add(_idSucursal);
            cmd.Parameters.Add(_retorno);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(_retorno.Value) < 0 )
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
