<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Equipos.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.Equipos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Equipos - Sistema UH</title>
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header con título y botón de regreso -->
            <div class="page-header page-header-equipos">
                <h1><i class="fas fa-laptop"></i> Gestión de Equipos</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Sección del formulario -->
            <div class="form-section">
                <h2 class="border-equipos">
                    <asp:Label ID="lblTitulo" runat="server" Text="Agregar Nuevo Equipo"></asp:Label>
                </h2>

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje"></asp:Label>

                <!-- Campo oculto para el ID (usado en edición) -->
                <asp:HiddenField ID="hfEquipoID" runat="server" Value="0" />

                <!-- Campo: Cliente/Usuario Propietario -->
                <div class="form-group">
                    <label>Cliente Propietario *</label>
                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
                        ControlToValidate="ddlUsuario" 
                        InitialValue="0"
                        ErrorMessage="Debe seleccionar un cliente" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Tipo de Equipo -->
                <div class="form-group">
                    <label>Tipo de Equipo *</label>
                    <asp:DropDownList ID="ddlTipoEquipo" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="Laptop">Laptop</asp:ListItem>
                        <asp:ListItem Value="Desktop">Desktop</asp:ListItem>
                        <asp:ListItem Value="Tablet">Tablet</asp:ListItem>
                        <asp:ListItem Value="Smartphone">Smartphone</asp:ListItem>
                        <asp:ListItem Value="Impresora">Impresora</asp:ListItem>
                        <asp:ListItem Value="Servidor">Servidor</asp:ListItem>
                        <asp:ListItem Value="Otro">Otro</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTipo" runat="server" 
                        ControlToValidate="ddlTipoEquipo" 
                        ErrorMessage="Debe seleccionar un tipo de equipo" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Modelo -->
                <div class="form-group">
                    <label>Modelo *</label>
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" 
                        placeholder="Ejemplo: Dell Inspiron 15 3000"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvModelo" runat="server" 
                        ControlToValidate="txtModelo" 
                        ErrorMessage="El modelo es obligatorio" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Botones de acción -->
                <div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Equipo" 
                        CssClass="btn btn-primary btn-primary-equipos" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" 
                        CausesValidation="false" />
                </div>
            </div>

            <!-- Grid de equipos -->
            <div class="grid-section">
                <h2>Lista de Equipos Registrados</h2>
                <asp:GridView ID="gvEquipos" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvEquipos_RowCommand" 
                    DataKeyNames="EquipoID">
                    <HeaderStyle CssClass="header-equipos" />
                    <Columns>
                        <asp:BoundField DataField="EquipoID" HeaderText="ID" />
                        <asp:BoundField DataField="TipoEquipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Propietario" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" CommandArgument='<%# Eval("EquipoID") %>' 
                                    CssClass="btn-edit" CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" CommandArgument='<%# Eval("EquipoID") %>' 
                                    CssClass="btn-delete" CausesValidation="false"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este equipo?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
