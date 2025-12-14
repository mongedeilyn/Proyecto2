using EjemploLogin.CapaLogica;
using System;
using System.Web.UI;

namespace EjemploLogin
{

    public partial class Login : System.Web.UI.Page
    {
        // Instancia de la capa lógica para autenticación
        private CL_usuario logicaUsuario = new CL_usuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Solo en la primera carga, no en postbacks
            {
                // Limpiar cualquier sesión previa para evitar datos residuales
                CapaDatos.CD_USuario.usuario = string.Empty;
                CapaDatos.CD_USuario.clave = string.Empty;
                CapaDatos.CD_USuario.nombre = string.Empty;
            }
        }

        ///Muestra un mensaje de alerta 
        private void MostrarAlerta(string message)
        {
            // Crear script JavaScript para mostrar alert
            string script = $"<script type='text/javascript'>alert('{message}');</script>";
            // Registrar el script para que se ejecute en el cliente
            ClientScript.RegisterStartupScript(this.GetType(), "Alert", script);
        }

        /// Event Handler: Procesa el intento de login del usuario
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(tusuario.Text) || string.IsNullOrWhiteSpace(tclave.Text))
            {
                MostrarAlerta("Por favor ingrese correo y contraseña");
                return; // Salir del método si la validación falla
            }

            // LLAMAR A CAPA LÓGICA para validar credenciales ✓
            if (CL_usuario.ValidarUsuario(tusuario.Text, tclave.Text) == 1)
            {
                // Credenciales válidas: redirigir al menú principal
                Response.Redirect("Principal.aspx");
            }
            else
            {
                // Credenciales inválidas: mostrar mensaje de error
                MostrarAlerta("Correo o contraseña incorrectos");
            }
        }

        /// Event Handler: Muestra el panel de registro ocultando el panel de login
        protected void bmostrarRegistro_Click(object sender, EventArgs e)
        {
            // Alternar visibilidad de paneles
            panelLogin.Visible = false;
            panelRegistro.Visible = true;
            
            // Limpiar mensaje previo
            lmsg.Text = "";
            
            // Limpiar campos de registro para empezar limpio
            tcorreoN.Text = string.Empty;
            tclaveN.Text = string.Empty;
            tnombreN.Text = string.Empty;
        }

        //Registra un nuevo usuario en el sistema
        protected void bguardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tcorreoN.Text) ||
                string.IsNullOrWhiteSpace(tclaveN.Text) ||
                string.IsNullOrWhiteSpace(tnombreN.Text))
            {
                // Mostrar mensaje de error en rojo
                lmsg.ForeColor = System.Drawing.Color.Red;
                lmsg.Text = "Todos los campos son obligatorios.";
                return; // Salir del método si la validación falla
            }

            // LLAMAR A CAPA LÓGICA para registrar usuario
            int resultado = logicaUsuario.AgregarUsuario(tcorreoN.Text, tclaveN.Text, tnombreN.Text);

            if (resultado == 1)
            {
                // Registro exitoso: mostrar mensaje de éxito en verde
                lmsg.ForeColor = System.Drawing.Color.Green;
                lmsg.Text = "Usuario registrado correctamente.";
                
                // Limpiar campos después de registro exitoso
                tcorreoN.Text = string.Empty;
                tclaveN.Text = string.Empty;
                tnombreN.Text = string.Empty;
            }
            else
            {
                // Error en registro: mostrar mensaje de error en rojo
                lmsg.ForeColor = System.Drawing.Color.Red;
                lmsg.Text = "Error al registrar usuario. El correo puede estar duplicado.";
            }
        }

        /// Regresa al panel de login ocultando el panel de registro
        protected void bvolver_Click(object sender, EventArgs e)
        {
            // Alternar visibilidad de paneles
            panelRegistro.Visible = false;
            panelLogin.Visible = true;
            
            // Limpiar mensaje previo
            lmsg.Text = "";
            
            // Limpiar campos de registro
            tcorreoN.Text = string.Empty;
            tclaveN.Text = string.Empty;
            tnombreN.Text = string.Empty;
        }
    }
}
