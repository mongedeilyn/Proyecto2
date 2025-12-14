using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    /// CAPA PRESENTACIÓN: Gestión de Detalles de Reparación
    public partial class DetallesReparacion : System.Web.UI.Page
    {
        private CL_DetalleReparacion logicaDetalle = new CL_DetalleReparacion();
        private CL_Reparacion logicaReparacion = new CL_Reparacion();

        /// EVENTO: Se ejecuta al cargar la página
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
                CargarDetalles();
                
                // Establecer fecha de inicio por defecto
                txtFechaInicio.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /// MÉTODO: Carga todas las reparaciones en el dropdown
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

        /// MÉTODO: Carga todos los detalles en el GridView
        private void CargarDetalles()
        {
            try
            {
                List<CD_DetalleReparacion> detalles = logicaDetalle.ListarDetallesReparacion();
                gvDetalles.DataSource = detalles;
                gvDetalles.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar detalles: " + ex.Message, false);
            }
        }

        /// EVENTO: Guardar detalle
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                int detalleID = Convert.ToInt32(hfDetalleID.Value);
                int reparacionID = Convert.ToInt32(ddlReparacion.SelectedValue);
                string descripcion = txtDescripcion.Text.Trim();
                DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
                DateTime? fechaFin = string.IsNullOrWhiteSpace(txtFechaFin.Text) 
                    ? (DateTime?)null 
                    : Convert.ToDateTime(txtFechaFin.Text);

                bool exito;

                if (detalleID == 0)
                {
                    // CREAR NUEVO
                    int resultado = logicaDetalle.CrearDetalleReparacion(reparacionID, descripcion, fechaInicio, fechaFin);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje($"Detalle registrado exitosamente. ID: {resultado}", true);
                    }
                }
                else
                {
                    // ACTUALIZAR
                    exito = logicaDetalle.ActualizarDetalleReparacion(detalleID, reparacionID, descripcion, fechaInicio, fechaFin);

                    if (exito)
                    {
                        MostrarMensaje("Detalle actualizado exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();
                    CargarDetalles();
                    CargarReparaciones();
                }
                else
                {
                    MostrarMensaje("Error al guardar el detalle. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        /// EVENTO: Cancelar
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// EVENTO: Comandos del GridView
        protected void gvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int detalleID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                try
                {
                    CD_DetalleReparacion detalle = logicaDetalle.ObtenerDetalleReparacionPorID(detalleID);

                    if (detalle != null)
                    {
                        hfDetalleID.Value = detalle.DetalleID.ToString();
                        ddlReparacion.SelectedValue = detalle.ReparacionID.ToString();
                        txtDescripcion.Text = detalle.Descripcion;
                        txtFechaInicio.Text = detalle.FechaInicio.ToString("yyyy-MM-dd");
                        txtFechaFin.Text = detalle.FechaFin.HasValue 
                            ? detalle.FechaFin.Value.ToString("yyyy-MM-dd") 
                            : "";

                        lblTitulo.Text = "Editar Detalle de Reparación";
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Detalle no encontrado", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar detalle: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    bool exito = logicaDetalle.EliminarDetalleReparacion(detalleID);

                    if (exito)
                    {
                        MostrarMensaje("Detalle eliminado exitosamente", true);
                        CargarDetalles();
                        LimpiarCampos();
                    }
                    else
                    {
                        MostrarMensaje("No se puede eliminar el detalle.", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        /// MÉTODO: Limpia los campos
        private void LimpiarCampos()
        {
            hfDetalleID.Value = "0";
            ddlReparacion.SelectedIndex = 0;
            txtDescripcion.Text = string.Empty;
            txtFechaInicio.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFechaFin.Text = string.Empty;
            lblTitulo.Text = "Registrar Nuevo Detalle";
            lblMensaje.Visible = false;
        }

        /// MÉTODO: Muestra mensaje
        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "mensaje mensaje-exito" : "mensaje mensaje-error";
            lblMensaje.Visible = true;
        }
    }
}
