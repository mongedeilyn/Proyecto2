using EjemploLogin.CapaDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EjemploLogin.CapaLogica
{
    public class CL_usuario
    {

       public static int ValidarUsuario(string usuario, string clave)
        {

            CD_USuario.usuario = usuario;
            CD_USuario.clave = clave;

            int existe = 0;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand("SELECT correo, clave, nombre FROM usuario WHERE correo = @correo AND clave = @clave", conexion))
                {
                    // Usar parámetros evita inyección SQL
                    comando.Parameters.AddWithValue("@correo", CD_USuario.usuario);
                    comando.Parameters.AddWithValue("@clave", CD_USuario.clave);

                    conexion.Open();
                    using (SqlDataReader registro = comando.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            CD_USuario.nombre = registro["nombre"].ToString();
                            existe = 1;
                        }

                    }
                }
            }
            catch (Exception)
            {

                existe = 0;
            }
            
            return existe;
        }
        

        public int AgregarUsuario(string usuario, string clave)
        {
            int existe = 0;

            try
            {
                String s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                SqlConnection conexion = new SqlConnection(s);
                conexion.Open();
                SqlCommand comando = new SqlCommand(" INSERT INTO usuario VALUES('" + usuario + "', '" + clave + "')", conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                existe = 1;
            }
            catch (Exception)
            { 
               existe = 0;
            }
           
            return existe;
        }

        public void EliminarUsuario(int usuarioID)
        {
            // Lógica para eliminar un usuario (simulada)
            // Aquí se podría eliminar el usuario de una base de datos
        }
    }
}