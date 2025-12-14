using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    /// CAPA LÓGICA: CRUD completo para la gestión de reparaciones
    /// Usa el modelo de conexión correcto: Conexion.Conectar()
    public class CL_Reparacion
    {
        /// Registra una nueva solicitud de reparación para un equipo
        public int CrearReparacion(int equipoID, DateTime fechaSolicitud, string estado)
        {
            int resultado = 0;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@EquipoID", equipoID);
                    comando.Parameters.AddWithValue("@FechaSolicitud", fechaSolicitud);
                    comando.Parameters.AddWithValue("@Estado", estado);

                    c.Open();
                    // ExecuteScalar devuelve el ID generado por el SP
                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                // En caso de error (equipo no existe, BD no disponible, etc.)
                resultado = 0;
            }

            return resultado;
        }

        /// Obtiene la lista completa de todas las reparaciones con información del equipo y cliente
        public List<CD_Reparacion> ListarReparaciones()
        {
            // Inicializar lista vacía para almacenar las reparaciones
            List<CD_Reparacion> lista = new List<CD_Reparacion>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarReparaciones", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    while (registro.Read())
                    {
                        lista.Add(new CD_Reparacion
                        {
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            EquipoID = Convert.ToInt32(registro["EquipoID"]),
                            FechaSolicitud = Convert.ToDateTime(registro["FechaSolicitud"]),
                            Estado = registro["Estado"].ToString(),
                            TipoEquipo = registro["TipoEquipo"].ToString(), // Del JOIN con Equipos
                            Modelo = registro["Modelo"].ToString(),         // Del JOIN con Equipos
                            NombreUsuario = registro["NombreUsuario"].ToString() // Del JOIN con Usuarios
                        });
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error, devolver lista vacía
                lista = new List<CD_Reparacion>();
            }

            return lista;
        }

        /// Busca los datos de una reparación específica por su ID
        public CD_Reparacion ObtenerReparacionPorID(int reparacionID)
        {
            CD_Reparacion reparacion = null; // null indica que no se encontró

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerReparacionPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    if (registro.Read())
                    {
                        reparacion = new CD_Reparacion
                        {
                            ReparacionID = Convert.ToInt32(registro["ReparacionID"]),
                            EquipoID = Convert.ToInt32(registro["EquipoID"]),
                            FechaSolicitud = Convert.ToDateTime(registro["FechaSolicitud"]),
                            Estado = registro["Estado"].ToString(),
                            TipoEquipo = registro["TipoEquipo"].ToString(), // Del JOIN con Equipos
                            Modelo = registro["Modelo"].ToString(),         // Del JOIN con Equipos
                            NombreUsuario = registro["NombreUsuario"].ToString() // Del JOIN con Usuarios
                        };
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error, devolver null
                reparacion = null;
            }

            return reparacion;
        }

        /// Actualiza los datos de una reparación existente
        public bool ActualizarReparacion(int reparacionID, int equipoID, DateTime fechaSolicitud, string estado)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);
                    comando.Parameters.AddWithValue("@EquipoID", equipoID);
                    comando.Parameters.AddWithValue("@FechaSolicitud", fechaSolicitud);
                    comando.Parameters.AddWithValue("@Estado", estado);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                // En caso de error (reparación no existe, equipo no válido, etc.)
                resultado = false;
            }

            return resultado;
        }

        /// Elimina una reparación del sistema
        public bool EliminarReparacion(int reparacionID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarReparacion", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ReparacionID", reparacionID);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                // En caso de error (reparación no existe, restricción violada, etc.)
                resultado = false;
            }

            return resultado;
        }
    }
}
