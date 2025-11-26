<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="EjemploLogin.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Bienvenido a la página principal</h2>
            <asp:Label ID="lblMensaje" runat="server" Text="Aquí va el contenido principal."></asp:Label>
            <br />
            <asp:Label ID="Lbnombre" runat="server" BorderStyle="Ridge" ForeColor="Red"></asp:Label>
            <br />
        </div>
        <asp:HyperLink ID="HyperLink1" runat="server">Regreso a inicio</asp:HyperLink>
        <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl="~/inicio.aspx" />
    </form>
</body>
</html>
