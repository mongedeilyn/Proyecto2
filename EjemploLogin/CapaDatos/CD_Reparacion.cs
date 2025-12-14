using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploLogin.CapaDatos
{
    public class CD_Reparacion
    {
        public int ReparacionID { get; set; }
        public int EquipoID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public string TipoEquipo { get; set; }
        public string Modelo { get; set; }
        public string NombreUsuario { get; set; }
    }
}
