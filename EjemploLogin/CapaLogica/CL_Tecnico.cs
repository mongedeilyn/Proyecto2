using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    /// CAPA LÓGICA: CRUD completo para la gestión de técnicos
    /// Usa el modelo de conexión correcto: Conexion.Conectar()
    public class CL_Tecnico
    {
        /// Registra un nuevo técnico en el sistema
        public int CrearTecnico(string nombre, string especialidad)
        {
            int resultado = 0;
            
            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearTecnico", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Especialidad", especialidad);
                    
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

        /// Obtiene la lista completa de todos los técnicos registrados
        public List<CD_Tecnico> ListarTecnicos()
        {
            List<CD_Tecnico> lista = new List<CD_Tecnico>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarTecnicos", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    
                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();
                    
                    while (registro.Read())
                    {
                        lista.Add(new CD_Tecnico
                        {
                            TecnicoID = Convert.ToInt32(registro["TecnicoID"]),
                            Nombre = registro["Nombre"].ToString(),
                            Especialidad = registro["Especialidad"].ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<CD_Tecnico>();
            }

            return lista;
        }

        /// Busca los datos de un técnico específico por su ID
        public CD_Tecnico ObtenerTecnicoPorID(int tecnicoID)
        {
            CD_Tecnico tecnico = null;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerTecnicoPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@TecnicoID", tecnicoID);
                    
                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();
                    
                    if (registro.Read())
                    {
                        tecnico = new CD_Tecnico
                        {
                            TecnicoID = Convert.ToInt32(registro["TecnicoID"]),
                            Nombre = registro["Nombre"].ToString(),
                            Especialidad = registro["Especialidad"].ToString()
                        };
                    }
                }
            }
            catch (Exception)
            {
                tecnico = null;
            }

            return tecnico;
        }

        /// Actualiza los datos de un técnico existente
        public bool ActualizarTecnico(int tecnicoID, string nombre, string especialidad)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarTecnico", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@TecnicoID", tecnicoID);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Especialidad", especialidad);
                    
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

        /// Elimina un técnico del sistema (si no tiene reparaciones asignadas)
        public bool EliminarTecnico(int tecnicoID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarTecnico", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@TecnicoID", tecnicoID);
                    
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
