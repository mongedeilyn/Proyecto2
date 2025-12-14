using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    /// CAPA PRESENTACIÓN: Gestión de Reparaciones.
    public partial class Reparaciones : System.Web.UI.Page
    {
        // Instancias de las capas lógicas necesarias
        private CL_Reparacion logicaReparacion = new CL_Reparacion();
        private CL_Equipo logicaEquipo = new CL_Equipo();

        /// EVENTO: Se ejecuta al cargar la página
        /// Verifica autenticación, carga equipos en dropdown y reparaciones en grid
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar autenticación
                if (string.IsNullOrEmpty(CD_USuario.nombre))
                {
                    Response.Redirect("../Login.aspx");
                    return;
                }

                // Cargar datos iniciales
                CargarEquipos();        // Llenar dropdown de equipos
                CargarReparaciones();   // Llenar GridView
                
                // Establecer fecha actual por defecto
                txtFechaSolicitud.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /// MÉTODO: Carga todos los equipos en el dropdown
        private void CargarEquipos()
        {
            try
            {
                // Obtener lista de equipos desde la capa lógica
                List<CD_Equipo> equipos = logicaEquipo.ListarEquipos();
                
                // Limpiar dropdown y agregar opción por defecto
                ddlEquipo.Items.Clear();
                ddlEquipo.Items.Add(new ListItem("-- Seleccione un equipo --", "0"));
                
                // Agregar cada equipo al dropdown con formato descriptivo
                foreach (var equipo in equipos)
                {
                    string texto = $"{equipo.TipoEquipo} {equipo.Modelo} ({equipo.NombreUsuario})";
                    ddlEquipo.Items.Add(new ListItem(texto, equipo.EquipoID.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar equipos: " + ex.Message, false);
            }
        }

        /// MÉTODO: Carga todas las reparaciones desde la BD y las muestra en el GridView
        private void CargarReparaciones()
        {
            try
            {
                // Obtener lista de reparaciones (incluye info de equipo y cliente)
                List<CD_Reparacion> reparaciones = logicaReparacion.ListarReparaciones();
                
                // Vincular datos al GridView
                gvReparaciones.DataSource = reparaciones;
                gvReparaciones.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar reparaciones: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Guardar Reparación"
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Obtener valores del formulario
                int reparacionID = Convert.ToInt32(hfReparacionID.Value);
                int equipoID = Convert.ToInt32(ddlEquipo.SelectedValue);
                DateTime fechaSolicitud = Convert.ToDateTime(txtFechaSolicitud.Text);
                string estado = ddlEstado.SelectedValue;

                bool exito;

                if (reparacionID == 0)
                {
                    // CREAR NUEVA REPARACIÓN
                    // CL_Reparacion.CrearReparacion retorna el ID de la nueva reparación
                    int resultado = logicaReparacion.CrearReparacion(equipoID, fechaSolicitud, estado);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje($"Reparación registrada exitosamente. ID: {resultado}", true);
                    }
                }
                else
                {
                    // ACTUALIZAR REPARACIÓN EXISTENTE
                    // CL_Reparacion.ActualizarReparacion retorna true si se actualizó
                    exito = logicaReparacion.ActualizarReparacion(reparacionID, equipoID, fechaSolicitud, estado);

                    if (exito)
                    {
                        MostrarMensaje("Reparación actualizada exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();
                    CargarReparaciones();  // Recargar GridView
                    CargarEquipos();       // Recargar dropdown por si cambió algo
                }
                else
                {
                    MostrarMensaje("Error al guardar la reparación. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Cancelar"
        /// Limpia el formulario y restablece el modo a "Registrar"
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// EVENTO: Maneja los comandos "Editar" y "Eliminar" del GridView
        protected void gvReparaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reparacionID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // CARGAR DATOS DE LA REPARACIÓN EN EL FORMULARIO
                try
                {
                    CD_Reparacion reparacion = logicaReparacion.ObtenerReparacionPorID(reparacionID);

                    if (reparacion != null)
                    {
                        // Llenar formulario con datos de la reparación
                        hfReparacionID.Value = reparacion.ReparacionID.ToString();
                        ddlEquipo.SelectedValue = reparacion.EquipoID.ToString();
                        txtFechaSolicitud.Text = reparacion.FechaSolicitud.ToString("yyyy-MM-dd");
                        ddlEstado.SelectedValue = reparacion.Estado;

                        // Cambiar título a modo edición
                        lblTitulo.Text = "Editar Reparación";
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Reparación no encontrada", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar reparación: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                // ELIMINAR REPARACIÓN
                try
                {
                    bool exito = logicaReparacion.EliminarReparacion(reparacionID);

                    if (exito)
                    {
                        MostrarMensaje("Reparación eliminada exitosamente", true);
                        CargarReparaciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MostrarMensaje("No se puede eliminar la reparación.", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        /// MÉTODO: Limpia todos los campos del formulario
        private void LimpiarCampos()
        {
            hfReparacionID.Value = "0";
            ddlEquipo.SelectedIndex = 0;
            txtFechaSolicitud.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ddlEstado.SelectedIndex = 0;
            lblTitulo.Text = "Registrar Nueva Reparación";
            lblMensaje.Visible = false;
        }

        /// MÉTODO: Muestra un mensaje de éxito o error al usuario
        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "mensaje mensaje-exito" : "mensaje mensaje-error";
            lblMensaje.Visible = true;
        }
    }
}
