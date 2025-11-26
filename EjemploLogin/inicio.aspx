<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="EjemploLogin.inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Usuario:</label>   
            <br />
            <asp:TextBox ID="tusuario" runat="server"></asp:TextBox>
            <br />
            <label>Contraseña:</label>
            <br />
            <asp:TextBox ID="tclave" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button Text="Ingresar" ID="bingresar" runat="server" OnClick="bingresar_Click" />
            <br />
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
