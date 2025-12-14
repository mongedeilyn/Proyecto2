using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    public partial class Equipos : System.Web.UI.Page
    {
        // Instancias de las capas lógicas 
        private CL_Equipo logicaEquipo = new CL_Equipo();
        private CL_UsuarioExamen logicaUsuario = new CL_UsuarioExamen();

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
                CargarUsuarios();  // Llenar dropdown de clientes
                CargarEquipos();   // Llenar GridView
            }
        }

        /// MÉTODO: Carga todos los usuarios/clientes en el dropdown
        private void CargarUsuarios()
        {
            try
            {
                List<CD_UsuarioExamen> usuarios = logicaUsuario.ListarUsuarios();
                
                // Limpiar dropdown y agregar opción por defecto
                ddlUsuario.Items.Clear();
                ddlUsuario.Items.Add(new ListItem("-- Seleccione un cliente --", "0"));
                
                // Agregar cada usuario al dropdown
                foreach (var usuario in usuarios)
                {
                    ddlUsuario.Items.Add(new ListItem(usuario.Nombre, usuario.UsuarioID.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar clientes: " + ex.Message, false);
            }
        }

        /// MÉTODO: Carga todos los equipos desde la BD y los muestra en el GridView
        private void CargarEquipos()
        {
            try
            {
                // Obtener lista de equipos (incluye NombreUsuario por el JOIN)
                List<CD_Equipo> equipos = logicaEquipo.ListarEquipos();
                
                // Vincular datos al GridView
                gvEquipos.DataSource = equipos;
                gvEquipos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar equipos: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Guardar Equipo"
        /// Crea un nuevo equipo o actualiza uno existente
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                // Obtener valores del formulario
                int equipoID = Convert.ToInt32(hfEquipoID.Value);
                int usuarioID = Convert.ToInt32(ddlUsuario.SelectedValue);
                string tipoEquipo = ddlTipoEquipo.SelectedValue;
                string modelo = txtModelo.Text.Trim();

                bool exito;

                if (equipoID == 0)
                {
                    // CREAR NUEVO EQUIPO
                    // CL_Equipo.CrearEquipo retorna el ID del nuevo equipo
                    int resultado = logicaEquipo.CrearEquipo(tipoEquipo, modelo, usuarioID);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje($"Equipo creado exitosamente. ID: {resultado}", true);
                    }
                }
                else
                {
                    // ACTUALIZAR EQUIPO EXISTENTE
                    // CL_Equipo.ActualizarEquipo retorna true si se actualizó
                    exito = logicaEquipo.ActualizarEquipo(equipoID, tipoEquipo, modelo, usuarioID);

                    if (exito)
                    {
                        MostrarMensaje("Equipo actualizado exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();
                    CargarEquipos();      // Recargar GridView
                    CargarUsuarios();     // Recargar dropdown por si cambió algo
                }
                else
                {
                    MostrarMensaje("Error al guardar el equipo. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar "Cancelar"
        /// Limpia el formulario y restablece el modo a "Agregar"
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// EVENTO: Maneja los comandos "Editar" y "Eliminar" del GridView
        protected void gvEquipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int equipoID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // CARGAR DATOS DEL EQUIPO EN EL FORMULARIO
                try
                {
                    CD_Equipo equipo = logicaEquipo.ObtenerEquipoPorID(equipoID);

                    if (equipo != null)
                    {
                        // Llenar formulario con datos del equipo
                        hfEquipoID.Value = equipo.EquipoID.ToString();
                        ddlUsuario.SelectedValue = equipo.UsuarioID.ToString();
                        ddlTipoEquipo.SelectedValue = equipo.TipoEquipo;
                        txtModelo.Text = equipo.Modelo;

                        // Cambiar título a modo edición
                        lblTitulo.Text = "Editar Equipo";
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Equipo no encontrado", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar equipo: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                // ELIMINAR EQUIPO
                try
                {
                    bool exito = logicaEquipo.EliminarEquipo(equipoID);

                    if (exito)
                    {
                        MostrarMensaje("Equipo eliminado exitosamente", true);
                        CargarEquipos();
                        LimpiarCampos();
                    }
                    else
                    {
                        MostrarMensaje("No se puede eliminar. El equipo tiene reparaciones asociadas.", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        /// MÉTODO: Limpia todos los campos del formulario
        /// Restablece el modo a "Agregar Nuevo Equipo"
        private void LimpiarCampos()
        {
            hfEquipoID.Value = "0";
            ddlUsuario.SelectedIndex = 0;
            ddlTipoEquipo.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            lblTitulo.Text = "Agregar Nuevo Equipo";
            lblMensaje.Visible = false;
        }

        /// MÉTODO: Muestra un mensaje de éxito (verde) o error (rojo) al usuario
        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "mensaje mensaje-exito" : "mensaje mensaje-error";
            lblMensaje.Visible = true;
        }
    }
}
