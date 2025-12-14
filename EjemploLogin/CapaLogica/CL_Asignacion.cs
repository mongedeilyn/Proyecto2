using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    /// CAPA LÓGICA: CRUD completo para la gestión de Asignaciones
    public class CL_Asignacion
    {
        /// Crea una nueva asignación de técnico a reparación
        public int CrearAsignacion(int reparacionID, int tecnicoID, DateTime fechaAsignacion)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearAsignacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);
                    comando.Parameters.AddWithValue("@TecnicoID", tecnicoID);
                    comando.Parameters.AddWithValue("@FechaAsignacion", fechaAsignacion);

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

        /// Lista todas las asignaciones
        public List<CD_Asignacion> ListarAsignaciones()
        {
            List<CD_Asignacion> lista = new List<CD_Asignacion>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarAsignaciones", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    while (registro.Read())
                    {
                        lista.Add(new CD_Asignacion
                        {
                            AsignacionID = Convert.ToInt32(registro["AsignacionID"]),
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            TecnicoID = Convert.ToInt32(registro["TecnicoID"]),
                            FechaAsignacion = Convert.ToDateTime(registro["FechaAsignacion"]),
                            NombreTecnico = registro["Tecnico"].ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<CD_Asignacion>();
            }

            return lista;
        }

        /// Obtiene una asignación por su ID
        public CD_Asignacion ObtenerAsignacionPorID(int asignacionID)
        {
            CD_Asignacion asignacion = null;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerAsignacionPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@AsignacionID", asignacionID);

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    if (registro.Read())
                    {
                        asignacion = new CD_Asignacion
                        {
                            AsignacionID = Convert.ToInt32(registro["AsignacionID"]),
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            TecnicoID = Convert.ToInt32(registro["TecnicoID"]),
                            FechaAsignacion = Convert.ToDateTime(registro["FechaAsignacion"]),
                            NombreTecnico = registro["Tecnico"].ToString()
                        };
                    }
                }
            }
            catch (Exception)
            {
                asignacion = null;
            }

            return asignacion;
        }

        /// Actualiza una asignación
        public bool ActualizarAsignacion(int asignacionID, int reparacionID, int tecnicoID, DateTime fechaAsignacion)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarAsignacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@AsignacionID", asignacionID);
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);
                    comando.Parameters.AddWithValue("@TecnicoID", tecnicoID);
                    comando.Parameters.AddWithValue("@FechaAsignacion", fechaAsignacion);

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

        /// Elimina una asignación
        public bool EliminarAsignacion(int asignacionID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarAsignacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@AsignacionID", asignacionID);

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
