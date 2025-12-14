<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EjemploLogin.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>

    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="css/style.css">
</head>

<body class="img js-fullheight" style="background-image: url(images/fondo.jpg);">
    <form id="form1" runat="server">

        <section class="ftco-section">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6 text-center mb-5">
                        <h2 class="heading-section">Universidad Hispanoamericana</h2>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-md-6 col-lg-4">
                        <div class="login-wrap p-0">
                            <h3 class="mb-4 text-center">Login</h3>

                            <!-- PANEL LOGIN -->
                            <asp:Panel ID="panelLogin" runat="server">

                                <div class="form-group">
                                    <asp:TextBox ID="tusuario" runat="server" CssClass="form-control" placeholder="Correo"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:TextBox ID="tclave" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" Text="Ingresar" CssClass="form-control btn btn-primary submit px-3" OnClick="Button1_Click" />
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="bmostrarRegistro" runat="server" Text="Registrar nuevo usuario" CssClass="form-control btn btn-secondary" OnClick="bmostrarRegistro_Click" />
                                </div>

                            </asp:Panel>

                            <!-- PANEL REGISTRO -->
                            <asp:Panel ID="panelRegistro" runat="server" Visible="false">

                                <h4 class="text-center">Registrar Usuario</h4>

                                <div class="form-group">
                                    <asp:TextBox ID="tcorreoN" runat="server" CssClass="form-control" placeholder="Correo"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:TextBox ID="tclaveN" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:TextBox ID="tnombreN" runat="server" CssClass="form-control" placeholder="Nombre completo (OBLIGATORIO)"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Button ID="bguardar" runat="server" Text="Guardar Usuario" CssClass="form-control btn btn-success" OnClick="bguardar_Click" />
                                </div>

                                <asp:Label ID="lmsg" runat="server" Font-Bold="true"></asp:Label>

                                <div class="form-group">
                                    <asp:Button ID="bvolver" runat="server" Text="Volver al Login" CssClass="form-control btn btn-dark" OnClick="bvolver_Click" />
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </section>

    </form>
</body>
</html>
