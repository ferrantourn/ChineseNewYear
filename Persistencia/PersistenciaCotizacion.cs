using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    public class PersistenciaCotizacion
    {

      
        public List<Cotizacion> ListarCotizaciones()
        {
            List<Cotizacion> _listaCotizacion = new List<Cotizacion>();

            SqlConnection conexion = new SqlConnection(Conexion.Cnn);
            SqlCommand cmd = Conexion.GetCommand("spListarCotizacion", conexion, CommandType.StoredProcedure);
            SqlParameter _fechaInicio = new SqlParameter("@FechaInicio", DateTime.Now.AddYears(-1));
            SqlParameter _fechaFin = new SqlParameter("@FechaFin", DateTime.Now.AddYears(1));
            cmd.Parameters.Add(_fechaFin);
            cmd.Parameters.Add(_fechaInicio);

            SqlDataReader _Reader;
            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                _Reader = cmd.ExecuteReader();
                DateTime _fecha;
                decimal _precioCompra, _precioVenta;

                while (_Reader.Read())
                {
                    _fecha = Convert.ToDateTime (_Reader["Fecha"]);
                    _precioCompra = Convert.ToDecimal(_Reader["PrecioCompra"]);
                    _precioVenta = Convert.ToDecimal(_Reader["PrecioVenta"]);

                    Cotizacion s = new Cotizacion
                    {
                        FECHA = _fecha,
                        PRECIOCOMPRA = _precioCompra,
                        PRECIOVENTA = _precioVenta
                    };

                    _listaCotizacion.Add(s);
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

            return _listaCotizacion;
        }
    }
}
