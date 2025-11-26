<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CP_usuario.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.CP_usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
            <label> usuario:  </label>
            <asp:TextBox ID="tusuario" runat="server"></asp:TextBox>    
            <br />
            <label>Clave:  </label>
            <asp:TextBox ID="tclave" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button Text="Ingresar" ID="bingresar" runat="server" />
            <asp:Button Text="Borrar" ID="bborrar" runat="server" />
            <asp:Button Text="Modificar" ID="bmodificar" runat="server" />

        </div>
    </form>
</body>
</html>
