using EjemploLogin.CapaDatos;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EjemploLogin.CapaLogica
{

    public class CL_usuario
    {

        /// Valida las credenciales de un usuario para permitir el acceso al sistema
        public static int ValidarUsuario(string correo, string clave)
        {
            // Guardar credenciales en la clase estática CD_USuario para mantener la sesión
            CD_USuario.usuario = correo;
            CD_USuario.clave = clave;

            int existe = 0; // Variable que indica si el usuario existe (1) o no (0)

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(
                    "SELECT nombre FROM UsuariosPasswords WHERE correo = @correo AND clave = @clave",
                    c))
                {
                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@clave", clave);

                    c.Open();
                    SqlDataReader registro = comando.ExecuteReader();

                    // Si se encontró un registro, el usuario existe y las credenciales son correctas
                    if (registro.Read())
                    {
                        // Guardar el nombre del usuario en la sesión
                        CD_USuario.nombre = registro["nombre"].ToString();
                        existe = 1; // Marcar como existente
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error (BD no disponible, etc.), marcar como no existente
                existe = 0;
            }

            return existe;
        }


        /// Registra un nuevo usuario en el sistema de autenticación
        public int AgregarUsuario(string correo, string clave, string nombre)
        {
            int resultado = 0; // Variable que indica el resultado de la operación

            try
            {
                using (SqlConnection c = Conexion.Conectar())
                using (SqlCommand comando = new SqlCommand(
                    "INSERT INTO UsuariosPasswords(correo, clave, nombre) VALUES(@correo, @clave, @nombre)",
                    c))
                {
                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@clave", clave);
                    comando.Parameters.AddWithValue("@nombre", nombre);

                    c.Open();
                    comando.ExecuteNonQuery();
                }

                resultado = 1;
            }
            catch (Exception)
            {
                // En caso de error (correo duplicado, BD no disponible, etc.)
                resultado = 0;
            }

            return resultado;
        }
    }
}
