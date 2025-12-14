using EjemploLogin.CapaDatos;
using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin.CapaPresentacion
{
    /// CAPA PRESENTACIÓN: Gestión de Usuarios (Clientes)
    /// Esta página permite administrar los usuarios/clientes del sistema de reparaciones.
    /// Implementa las operaciones CRUD completas (Crear, Leer, Actualizar, Eliminar).
    public partial class Usuarios : System.Web.UI.Page
    {
        // Instancia de la capa lógica para operaciones CRUD de usuarios
        private CL_UsuarioExamen logicaUsuario = new CL_UsuarioExamen();

        /// EVENTO: Se ejecuta al cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Solo en la primera carga, no en postbacks
            {
                // Verificar autenticación
                if (string.IsNullOrEmpty(CD_USuario.nombre))
                {
                    // Usuario no autenticado, redirigir al login
                    Response.Redirect("../Login.aspx");
                    return;
                }

                // Usuario autenticado, cargar datos
                CargarUsuarios();
            }
        }

        /// MÉTODO: Carga todos los usuarios desde la base de datos y los muestra en el GridView
        private void CargarUsuarios()
        {
            try
            {
                // Obtener lista de usuarios desde la capa lógica
                List<CD_UsuarioExamen> usuarios = logicaUsuario.ListarUsuarios();
                
                // Vincular datos al GridView
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si falla la carga
                MostrarMensaje("Error al cargar usuarios: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar el botón "Guardar Usuario"
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificar que todas las validaciones pasen
            if (!Page.IsValid)
                return;

            try
            {
                // Obtener ID del HiddenField (0 = nuevo, >0 = editar)
                int usuarioID = Convert.ToInt32(hfUsuarioID.Value);
                
                // Obtener valores de los campos del formulario
                string nombre = txtNombre.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                bool exito;

                if (usuarioID == 0)
                {
                    // INSERTAR NUEVO USUARIO
                    // Llamar a CL_UsuarioExamen.CrearUsuario()
                    // Retorna el ID del nuevo usuario (>0 si exitoso, 0 si hay error)
                    int resultado = logicaUsuario.CrearUsuario(nombre, correo, telefono);
                    exito = resultado > 0;

                    if (exito)
                    {
                        MostrarMensaje("Usuario creado exitosamente con ID: " + resultado, true);
                    }
                }
                else
                {
                    // ACTUALIZAR USUARIO EXISTENTE
                    // Llamar a CL_UsuarioExamen.ActualizarUsuario()
                    // Retorna true si se actualizó correctamente
                    exito = logicaUsuario.ActualizarUsuario(usuarioID, nombre, correo, telefono);

                    if (exito)
                    {
                        MostrarMensaje("Usuario actualizado exitosamente", true);
                    }
                }

                if (exito)
                {
                    LimpiarCampos();     
                    CargarUsuarios();      
                }
                else
                {
                    // Si hubo error en la operación
                    MostrarMensaje("Error al guardar el usuario. Verifique los datos.", false);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier error inesperado
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        /// EVENTO: Se ejecuta al presionar el botón "Cancelar"
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// EVENTO: Se ejecuta cuando se presiona un botón dentro del GridView
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del usuario desde el CommandArgument
            int usuarioID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // MODO EDICIÓN: Cargar datos del usuario en el formulario
                try
                {
                    // Obtener usuario por ID desde la capa lógica
                    CD_UsuarioExamen usuario = logicaUsuario.ObtenerUsuarioPorID(usuarioID);

                    if (usuario != null)
                    {
                        // Llenar el formulario con los datos del usuario
                        hfUsuarioID.Value = usuario.UsuarioID.ToString();
                        txtNombre.Text = usuario.Nombre;
                        txtCorreo.Text = usuario.CorreoElectronico;
                        txtTelefono.Text = usuario.Telefono;

                        // Cambiar el título del formulario
                        lblTitulo.Text = "Editar Usuario";

                        // Ocultar cualquier mensaje previo
                        lblMensaje.Visible = false;
                    }
                    else
                    {
                        MostrarMensaje("Usuario no encontrado", false);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar usuario: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                // MODO ELIMINACIÓN: Intentar eliminar el usuario
                try
                {
                    // Llamar a la capa lógica para eliminar
                    bool exito = logicaUsuario.EliminarUsuario(usuarioID);

                    if (exito)
                    {
                        // Eliminación exitosa
                        MostrarMensaje("Usuario eliminado exitosamente", true);
                        CargarUsuarios(); // Recargar GridView
                        LimpiarCampos();  // Limpiar formulario por si estaba en edición
                    }
                    else
                    {
                        // No se pudo eliminar (probablemente tiene equipos asociados)
                        MostrarMensaje("No se puede eliminar. El usuario tiene equipos asociados.", false);
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
            hfUsuarioID.Value = "0";           // Resetear a modo INSERT
            txtNombre.Text = string.Empty;     // Limpiar nombre
            txtCorreo.Text = string.Empty;     // Limpiar correo
            txtTelefono.Text = string.Empty;   // Limpiar teléfono
            lblTitulo.Text = "Agregar Nuevo Usuario"; // Restablecer título
            lblMensaje.Visible = false;        // Ocultar mensaje
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
