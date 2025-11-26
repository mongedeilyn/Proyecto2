using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploLogin.CapaDatos
{
    public class CD_Equipo
    {
        public int EquipoID { get; set; }
        public string tipo { get; set; }
        public string modelo { get; set; }
        public int UsuarioID { get; set; }
    }
}