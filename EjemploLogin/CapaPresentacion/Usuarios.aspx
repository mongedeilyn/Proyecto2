<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.Usuarios" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Usuarios - Sistema UH</title>
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header con título y botón de regreso -->
            <div class="page-header page-header-usuarios">
                <h1><i class="fas fa-users"></i> Gestión de Usuarios</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Sección del formulario -->
            <div class="form-section">
                <h2 class="border-usuarios">
                    <asp:Label ID="lblTitulo" runat="server" Text="Agregar Nuevo Usuario"></asp:Label>
                </h2>

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje"></asp:Label>

                <!-- Campo oculto para el ID (usado en edición) -->
                <asp:HiddenField ID="hfUsuarioID" runat="server" Value="0" />

                <!-- Campo: Nombre Completo -->
                <div class="form-group">
                    <label>Nombre Completo *</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                        placeholder="Ejemplo: Juan Pérez González"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                        ControlToValidate="txtNombre" 
                        ErrorMessage="El nombre es obligatorio" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Correo Electrónico -->
                <div class="form-group">
                    <label>Correo Electrónico *</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" 
                        placeholder="ejemplo@correo.com" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" 
                        ControlToValidate="txtCorreo" 
                        ErrorMessage="El correo es obligatorio" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Teléfono -->
                <div class="form-group">
                    <label>Teléfono *</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" 
                        placeholder="1234-5678"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                        ControlToValidate="txtTelefono" 
                        ErrorMessage="El teléfono es obligatorio" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Botones de acción -->
                <div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" 
                        CssClass="btn btn-primary btn-primary-usuarios" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" 
                        CausesValidation="false" />
                </div>
            </div>

            <!-- Grid de usuarios -->
            <div class="grid-section">
                <h2>Lista de Usuarios Registrados</h2>
                <asp:GridView ID="gvUsuarios" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvUsuarios_RowCommand" 
                    DataKeyNames="UsuarioID">
                    <HeaderStyle CssClass="header-usuarios" />
                    <Columns>
                        <asp:BoundField DataField="UsuarioID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Completo" />
                        <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" CommandArgument='<%# Eval("UsuarioID") %>' 
                                    CssClass="btn-edit" CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" CommandArgument='<%# Eval("UsuarioID") %>' 
                                    CssClass="btn-delete" CausesValidation="false"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este usuario?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
