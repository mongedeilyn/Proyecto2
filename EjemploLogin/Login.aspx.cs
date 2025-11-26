using EjemploLogin.CapaLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLogin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static void MostrarAlerta(Page page, string message)
        {
            string script = $"<script type='text/javascript'>alert('{message}');</script>";
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "AlertScript", script);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CL_usuario.ValidarUsuario(tusuario.Text, tclave.Text) == 1)
                Response.Redirect("Principal.aspx");
            else
                MostrarAlerta(this, "Usuario o clave incorrecta");
        }
    }
}