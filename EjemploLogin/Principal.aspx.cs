using System;
using System.Web.UI;

namespace EjemploLogin
{
    ///Página Principal del Sistema (Dashboard)
    public partial class Principal : System.Web.UI.Page
    {
        /// Evento que se ejecuta al cargar la página
        /// Verifica que el usuario esté autenticado antes de mostrar el dashboard
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay un usuario autenticado en sesión
                if (string.IsNullOrEmpty(CapaDatos.CD_USuario.nombre))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    // Mostrar mensaje de bienvenida personalizado
                    Lbnombre.Text = CapaDatos.CD_USuario.nombre;
                }
            }
        }

        /// Event Handler: Cierra la sesión del usuario y redirige al login
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Limpiar todos los campos de sesión del usuario
            CapaDatos.CD_USuario.usuario = string.Empty;
            CapaDatos.CD_USuario.clave = string.Empty;
            CapaDatos.CD_USuario.nombre = string.Empty;
            
            // Redirigir al login
            Response.Redirect("Login.aspx");
        }
    }
}