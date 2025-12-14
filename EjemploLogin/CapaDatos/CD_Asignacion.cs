using System;

namespace EjemploLogin.CapaDatos
{
    /// Modelo de datos para Asignaciones
    /// Representa la asignación de un técnico a una reparación
    public class CD_Asignacion
    {
        public int AsignacionID { get; set; }
        public int ReparacionID { get; set; }
        public int TecnicoID { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public string NombreTecnico { get; set; }
        public string EquipoInfo { get; set; } 
    }
}
