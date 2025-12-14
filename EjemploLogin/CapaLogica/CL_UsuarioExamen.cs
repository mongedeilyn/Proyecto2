using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{
    public class CL_UsuarioExamen
    {
        /// Crea un nuevo cliente en el sistema de gestión de reparaciones
        public int CrearUsuario(string nombre, string correo, string telefono)
        {
            int resultado = 0; // Almacenará el ID del nuevo usuario o 0 si hay error

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_CrearUsuario", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@CorreoElectronico", correo);
                    comando.Parameters.AddWithValue("@Telefono", telefono ?? (object)DBNull.Value);

                    c.Open();
                    resultado = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                // En caso de error (correo duplicado, BD no disponible, etc.)
                resultado = 0;
            }

            return resultado;
        }

        /// Obtiene la lista completa de todos los clientes registrados
        public List<CD_UsuarioExamen> ListarUsuarios()
        {
            List<CD_UsuarioExamen> lista = new List<CD_UsuarioExamen>();

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ListarUsuarios", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    while (registro.Read())
                    {
                        lista.Add(new CD_UsuarioExamen
                        {
                            UsuarioID = Convert.ToInt32(registro["UsuarioID"]),
                            Nombre = registro["Nombre"].ToString(),
                            CorreoElectronico = registro["CorreoElectronico"].ToString(),
                            Telefono = registro["Telefono"].ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error, devolver lista vacía
                lista = new List<CD_UsuarioExamen>();
            }

            return lista;
        }

        /// busca los datos de un cliente específico por su ID
        public CD_UsuarioExamen ObtenerUsuarioPorID(int usuarioID)
        {
            CD_UsuarioExamen usuario = null; // null indica que no se encontró

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ObtenerUsuarioPorID", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@UsuarioID", usuarioID);

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    // Si se encontró el registro, crear el objeto
                    if (registro.Read())
                    {
                        usuario = new CD_UsuarioExamen
                        {
                            UsuarioID = Convert.ToInt32(registro["UsuarioID"]),
                            Nombre = registro["Nombre"].ToString(),
                            CorreoElectronico = registro["CorreoElectronico"].ToString(),
                            Telefono = registro["Telefono"].ToString()
                        };
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error, devolver null
                usuario = null;
            }

            return usuario;
        }

        /// Actualiza los datos de un cliente existente
        public bool ActualizarUsuario(int usuarioID, string nombre, string correo, string telefono)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_ActualizarUsuario", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@UsuarioID", usuarioID);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@CorreoElectronico", correo);
                    comando.Parameters.AddWithValue("@Telefono", telefono ?? (object)DBNull.Value);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                // En caso de error (usuario no existe, correo duplicado, etc.)
                resultado = false;
            }

            return resultado;
        }

        /// Elimina un cliente del sistema (si no tiene equipos asociados)
        public bool EliminarUsuario(int usuarioID)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand("SP_EliminarUsuario", c))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@UsuarioID", usuarioID);

                    c.Open();
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception)
            {
                // En caso de error (usuario no existe, tiene equipos asociados, etc.)
                resultado = false;
            }

            return resultado;
        }
    }
}
