using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    /// CAPA PRESENTACIÓN: Gestión de Técnicos
    /// Permite administrar los técnicos que realizan reparaciones en el sistema.
    /// Implementa CRUD completo (Crear, Leer, Actualizar, Eliminar).
    public partial class Tecnicos : System.Web.UI.Page
    {
        // Instancia de la capa lógica para operaciones CRUD de técnicos
        private CL_Tecnico logicaTecnico = new CL_Tecnico();

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
                CargarTecnicos();
            }
        }

        /// MÉTODO: Carga todos los técnicos desde la BD y los muestra en el GridView
        private void CargarTecnicos()
        {
            try
            {
                // Obtener lista de técnicos desde la capa lógica
                List<CD_Tecnico> tecnicos = logicaTecnico.ListarTecnicos();
                
                // Vincular datos al GridView
                gvTecnicos.DataSource = tecnicos;
                gvTecnicos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar técnicos: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Guardar Técnico"
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Obtener valores del formulario
                int tecnicoID = Convert.ToInt32(hfTecnicoID.Value);
                string nombre = txtNombre.Text.Trim();
                string especialidad = txtEspecialidad.Text.Trim();

                bool exito;

                if (tecnicoID == 0)
                {
                    // CREAR NUEVO TÉCNICO
                    // CL_Tecnico.CrearTecnico retorna el ID del nuevo técnico
                    int resultado = logicaTecnico.CrearTecnico(nombre, especialidad);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje($"Técnico creado exitosamente. ID: {resultado}", true);
                    }
                }
                else
                {
                    // ACTUALIZAR TÉCNICO EXISTENTE
                    // CL_Tecnico.ActualizarTecnico retorna true si se actualizó
                    exito = logicaTecnico.ActualizarTecnico(tecnicoID, nombre, especialidad);

                    if (exito)
                    {
                        MostrarMensaje("Técnico actualizado exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();
                    CargarTecnicos();  // Recargar GridView
                }
                else
                {
                    MostrarMensaje("Error al guardar el técnico. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Cancelar"
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// EVENTO: Maneja los comandos "Editar" y "Eliminar" del GridView
        protected void gvTecnicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int tecnicoID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // CARGAR DATOS DEL TÉCNICO EN EL FORMULARIO
                try
                {
                    CD_Tecnico tecnico = logicaTecnico.ObtenerTecnicoPorID(tecnicoID);

                    if (tecnico != null)
                    {
                        // Llenar formulario con datos del técnico
                        hfTecnicoID.Value = tecnico.TecnicoID.ToString();
                        txtNombre.Text = tecnico.Nombre;
                        txtEspecialidad.Text = tecnico.Especialidad;

                        // Cambiar título a modo edición
                        lblTitulo.Text = "Editar Técnico";
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Técnico no encontrado", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar técnico: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                // ELIMINAR TÉCNICO
                try
                {
                    bool exito = logicaTecnico.EliminarTecnico(tecnicoID);

                    if (exito)
                    {
                        MostrarMensaje("Técnico eliminado exitosamente", true);
                        CargarTecnicos();
                        LimpiarCampos();
                    }
                    else
                    {
                        MostrarMensaje("No se puede eliminar. El técnico tiene asignaciones activas.", false);
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
            hfTecnicoID.Value = "0";
            txtNombre.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            lblTitulo.Text = "Agregar Nuevo Técnico";
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
