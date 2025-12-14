using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EjemploLogin.CapaDatos
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["conexion"].ConnectionString
            );
        }
    }
}