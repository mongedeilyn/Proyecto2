using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    /// CAPA LÓGICA: CRUD completo para la gestión de Detalles de Reparación
    public class CL_DetalleReparacion
    {
        /// Crea un nuevo detalle de reparación
        public int CrearDetalleReparacion(int reparacionID, string descripcion, DateTime fechaInicio, DateTime? fechaFin)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearDetalleReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@FechaFin", fechaFin ?? (object)DBNull.Value);

                    c.Open();
                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                resultado = 0;
            }

            return resultado;
        }

        /// Lista todos los detalles de reparación
        public List<CD_DetalleReparacion> ListarDetallesReparacion()
        {
            List<CD_DetalleReparacion> lista = new List<CD_DetalleReparacion>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarDetallesReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    while (registro.Read())
                    {
                        lista.Add(new CD_DetalleReparacion
                        {
                            DetalleID = Convert.ToInt32(registro["DetalleID"]),
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            Descripcion = registro["Descripcion"].ToString(),
                            FechaInicio = Convert.ToDateTime(registro["FechaInicio"]),
                            FechaFin = registro["FechaFin"] != DBNull.Value 
                                ? Convert.ToDateTime(registro["FechaFin"]) 
                                : (DateTime?)null
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<CD_DetalleReparacion>();
            }

            return lista;
        }

        /// Obtiene un detalle de reparación por su ID
        public CD_DetalleReparacion ObtenerDetalleReparacionPorID(int detalleID)
        {
            CD_DetalleReparacion detalle = null;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerDetalleReparacionPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@DetalleID", detalleID);

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    if (registro.Read())
                    {
                        detalle = new CD_DetalleReparacion
                        {
                            DetalleID = Convert.ToInt32(registro["DetalleID"]),
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            Descripcion = registro["Descripcion"].ToString(),
                            FechaInicio = Convert.ToDateTime(registro["FechaInicio"]),
                            FechaFin = registro["FechaFin"] != DBNull.Value 
                                ? Convert.ToDateTime(registro["FechaFin"]) 
                                : (DateTime?)null
                        };
                    }
                }
            }
            catch (Exception)
            {
                detalle = null;
            }

            return detalle;
        }

        /// Actualiza un detalle de reparación
        public bool ActualizarDetalleReparacion(int detalleID, int reparacionID, string descripcion, DateTime fechaInicio, DateTime? fechaFin)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarDetalleReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@DetalleID", detalleID);
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@FechaFin", fechaFin ?? (object)DBNull.Value);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

        /// Elimina un detalle de reparación
        public bool EliminarDetalleReparacion(int detalleID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarDetalleReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@DetalleID", detalleID);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}
