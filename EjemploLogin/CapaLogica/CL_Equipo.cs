using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    /// CAPA LÓGICA: CRUD completo para la gestión de equipos
    /// Usa el modelo de conexión correcto: Conexion.Conectar()
    public class CL_Equipo
    {
        /// Registra un nuevo equipo en el sistema vinculado a un cliente
        public int CrearEquipo(string tipoEquipo, string modelo, int usuarioID)
        {
            int resultado = 0; // Almacenará el ID del nuevo equipo o 0 si hay error

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearEquipo", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@TipoEquipo", tipoEquipo);
                    comando.Parameters.AddWithValue("@Modelo", modelo);
                    comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    
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

        /// Obtiene la lista completa de todos los equipos con información del propietario
        public List<CD_Equipo> ListarEquipos()
        {
            List<CD_Equipo> lista = new List<CD_Equipo>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarEquipos", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    
                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();
                    
                    while (registro.Read())
                    {
                        lista.Add(new CD_Equipo
                        {
                            EquipoID = Convert.ToInt32(registro["EquipoID"]),
                            TipoEquipo = registro["TipoEquipo"].ToString(),
                            Modelo = registro["Modelo"].ToString(),
                            UsuarioID = Convert.ToInt32(registro["UsuarioID"]),
                            NombreUsuario = registro["NombreUsuario"].ToString() // Del JOIN
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<CD_Equipo>();
            }

            return lista;
        }

        /// Busca los datos de un equipo específico por su ID
        public CD_Equipo ObtenerEquipoPorID(int equipoID)
        {
            CD_Equipo equipo = null;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerEquipoPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@EquipoID", equipoID);
                    
                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();
                    
                    if (registro.Read())
                    {
                        equipo = new CD_Equipo
                        {
                            EquipoID = Convert.ToInt32(registro["EquipoID"]),
                            TipoEquipo = registro["TipoEquipo"].ToString(),
                            Modelo = registro["Modelo"].ToString(),
                            UsuarioID = Convert.ToInt32(registro["UsuarioID"]),
                            NombreUsuario = registro["NombreUsuario"].ToString()
                        };
                    }
                }
            }
            catch (Exception)
            {
                equipo = null;
            }

            return equipo;
        }

        /// Actualiza los datos de un equipo existente
        public bool ActualizarEquipo(int equipoID, string tipoEquipo, string modelo, int usuarioID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarEquipo", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@EquipoID", equipoID);
                    comando.Parameters.AddWithValue("@TipoEquipo", tipoEquipo);
                    comando.Parameters.AddWithValue("@Modelo", modelo);
                    comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    
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

        /// Elimina un equipo del sistema (si no tiene reparaciones asociadas)
        public bool EliminarEquipo(int equipoID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarEquipo", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@EquipoID", equipoID);
                    
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
