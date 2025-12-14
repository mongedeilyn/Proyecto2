<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reparaciones.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.Reparaciones" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Reparaciones - Sistema UH</title>
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header con título y botón de regreso -->
            <div class="page-header page-header-reparaciones">
                <h1><i class="fas fa-tools"></i> Gestión de Reparaciones</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Sección del formulario -->
            <div class="form-section">
                <h2 class="border-reparaciones">
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Nueva Reparación"></asp:Label>
                </h2>

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje"></asp:Label>

                <!-- Campo oculto para el ID (usado en edición) -->
                <asp:HiddenField ID="hfReparacionID" runat="server" Value="0" />

                <!-- Campo: Equipo a Reparar -->
                <div class="form-group">
                    <label>Equipo a Reparar *</label>
                    <asp:DropDownList ID="ddlEquipo" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEquipo" runat="server" 
                        ControlToValidate="ddlEquipo" 
                        InitialValue="0"
                        ErrorMessage="Debe seleccionar un equipo" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Fecha de Ingreso -->
                <div class="form-group">
                    <label>Fecha de Ingreso *</label>
                    <asp:TextBox ID="txtFechaSolicitud" runat="server" CssClass="form-control" 
                        TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server" 
                        ControlToValidate="txtFechaSolicitud" 
                        ErrorMessage="La fecha de ingreso es obligatoria" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Estado -->
                <div class="form-group">
                    <label>Estado *</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                        <asp:ListItem Value="En Proceso">En Proceso</asp:ListItem>
                        <asp:ListItem Value="Completada">Completada</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEstado" runat="server" 
                        ControlToValidate="ddlEstado" 
                        ErrorMessage="Debe seleccionar un estado" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Botones de acción -->
                <div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Reparación" 
                        CssClass="btn btn-primary btn-primary-reparaciones" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" 
                        CausesValidation="false" />
                </div>
            </div>

            <!-- Grid de reparaciones -->
            <div class="grid-section">
                <h2>Lista de Reparaciones Registradas</h2>
                <asp:GridView ID="gvReparaciones" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvReparaciones_RowCommand" 
                    DataKeyNames="ReparacionID">
                    <HeaderStyle CssClass="header-reparaciones" />
                    <Columns>
                        <asp:BoundField DataField="ReparacionID" HeaderText="ID" />
                        <asp:TemplateField HeaderText="Equipo">
                            <ItemTemplate>
                                <%# Eval("TipoEquipo") %> <%# Eval("Modelo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Cliente" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Ingreso" 
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='badge badge-<%# Eval("Estado").ToString().ToLower().Replace(" ", "") %>'>
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" CommandArgument='<%# Eval("ReparacionID") %>' 
                                    CssClass="btn-edit" CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" CommandArgument='<%# Eval("ReparacionID") %>' 
                                    CssClass="btn-delete" CausesValidation="false"
                                    OnClientClick="return confirm('¿Está seguro de eliminar esta reparación?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
