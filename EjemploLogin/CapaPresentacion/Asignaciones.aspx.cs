using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    public partial class Asignaciones : System.Web.UI.Page
    {
        private CL_Asignacion logicaAsignacion = new CL_Asignacion();
        private CL_Reparacion logicaReparacion = new CL_Reparacion();
        private CL_Tecnico logicaTecnico = new CL_Tecnico();

        /// Se ejecuta al cargar la página
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
                CargarReparaciones();
                CargarTecnicos();
                CargarAsignaciones();
                
                // Establecer fecha actual por defecto
                txtFechaAsignacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        ///Carga todas las reparaciones en el dropdown
        private void CargarReparaciones()
        {
            try
            {
                List<CD_Reparacion> reparaciones = logicaReparacion.ListarReparaciones();
                
                ddlReparacion.Items.Clear();
                ddlReparacion.Items.Add(new ListItem("-- Seleccione una reparación --", "0"));
                
                foreach (var rep in reparaciones)
                {
                    string texto = $"ID: {rep.ReparacionID} - {rep.TipoEquipo} {rep.Modelo} ({rep.Estado})";
                    ddlReparacion.Items.Add(new ListItem(texto, rep.ReparacionID.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar reparaciones: " + ex.Message, false);
            }
        }

        ///Carga todos los técnicos en el dropdown
        private void CargarTecnicos()
        {
            try
            {
                List<CD_Tecnico> tecnicos = logicaTecnico.ListarTecnicos();
                
                ddlTecnico.Items.Clear();
                ddlTecnico.Items.Add(new ListItem("-- Seleccione un técnico --", "0"));
                
                foreach (var tec in tecnicos)
                {
                    string texto = $"{tec.Nombre} - {tec.Especialidad}";
                    ddlTecnico.Items.Add(new ListItem(texto, tec.TecnicoID.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar técnicos: " + ex.Message, false);
            }
        }

        private void CargarAsignaciones()
        {
            try
            {
                List<CD_Asignacion> asignaciones = logicaAsignacion.ListarAsignaciones();
                gvAsignaciones.DataSource = asignaciones;
                gvAsignaciones.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar asignaciones: " + ex.Message, false);
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int asignacionID = Convert.ToInt32(hfAsignacionID.Value);
                int reparacionID = Convert.ToInt32(ddlReparacion.SelectedValue);
                int tecnicoID = Convert.ToInt32(ddlTecnico.SelectedValue);
                DateTime fechaAsignacion = Convert.ToDateTime(txtFechaAsignacion.Text);

                bool exito;

                if (asignacionID == 0)
                {
                    // CREAR NUEVA
                    int resultado = logicaAsignacion.CrearAsignacion(reparacionID, tecnicoID, fechaAsignacion);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje($"Asignación registrada exitosamente. ID: {resultado}", true);
                    }
                }
                else
                {
                    // ACTUALIZAR
                    exito = logicaAsignacion.ActualizarAsignacion(asignacionID, reparacionID, tecnicoID, fechaAsignacion);

                    if (exito)
                    {
                        MostrarMensaje("Asignación actualizada exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();
                    CargarAsignaciones();
                    CargarReparaciones();
                    CargarTecnicos();
                }
                else
                {
                    MostrarMensaje("Error al guardar la asignación. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void gvAsignaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int asignacionID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                try
                {
                    CD_Asignacion asignacion = logicaAsignacion.ObtenerAsignacionPorID(asignacionID);

                    if (asignacion != null)
                    {
                        hfAsignacionID.Value = asignacion.AsignacionID.ToString();
                        ddlReparacion.SelectedValue = asignacion.ReparacionID.ToString();
                        ddlTecnico.SelectedValue = asignacion.TecnicoID.ToString();
                        txtFechaAsignacion.Text = asignacion.FechaAsignacion.ToString("yyyy-MM-dd");

                        lblTitulo.Text = "Editar Asignación";
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Asignación no encontrada", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar asignación: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    bool exito = logicaAsignacion.EliminarAsignacion(asignacionID);

                    if (exito)
                    {
                        MostrarMensaje("Asignación eliminada exitosamente", true);
                        CargarAsignaciones();
                        LimpiarCampos();
                    }
                    else
                    {
                        MostrarMensaje("No se puede eliminar la asignación.", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        private void LimpiarCampos()
        {
            hfAsignacionID.Value = "0";
            ddlReparacion.SelectedIndex = 0;
            ddlTecnico.SelectedIndex = 0;
            txtFechaAsignacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTitulo.Text = "Nueva Asignación de Técnico";
            lblMensaje.Visible = false;
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "mensaje mensaje-exito" : "mensaje mensaje-error";
            lblMensaje.Visible = true;
        }
    }
}
