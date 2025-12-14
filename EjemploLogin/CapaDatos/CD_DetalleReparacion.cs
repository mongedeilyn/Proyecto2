using System;

namespace EjemploLogin.CapaDatos
{
    /// Modelo de datos para DetallesReparacion
    /// Representa los detalles de una reparación específica
    public class CD_DetalleReparacion
    {
        public int DetalleID { get; set; }
        public int ReparacionID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string EquipoInfo { get; set; }  
    }
}
