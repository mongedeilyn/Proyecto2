<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tecnicos.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.Tecnicos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Técnicos - Sistema UH</title>
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header con título y botón de regreso -->
            <div class="page-header page-header-tecnicos">
                <h1><i class="fas fa-user-cog"></i> Gestión de Técnicos</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Sección del formulario -->
            <div class="form-section">
                <h2 class="border-tecnicos">
                    <asp:Label ID="lblTitulo" runat="server" Text="Agregar Nuevo Técnico"></asp:Label>
                </h2>

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="mensaje"></asp:Label>

                <!-- Campo oculto para el ID (usado en edición) -->
                <asp:HiddenField ID="hfTecnicoID" runat="server" Value="0" />

                <!-- Campo: Nombre Completo -->
                <div class="form-group">
                    <label>Nombre Completo *</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" 
                        placeholder="Ejemplo: Juan Pérez Gómez"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                        ControlToValidate="txtNombre" 
                        ErrorMessage="El nombre es obligatorio" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Campo: Especialidad -->
                <div class="form-group">
                    <label>Especialidad *</label>
                    <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" 
                        placeholder="Ejemplo: Hardware, Software, Redes"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" 
                        ControlToValidate="txtEspecialidad" 
                        ErrorMessage="La especialidad es obligatoria" 
                        Display="Dynamic" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <!-- Botones de acción -->
                <div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Técnico" 
                        CssClass="btn btn-primary btn-primary-tecnicos" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="btn btn-secondary" OnClick="btnCancelar_Click" 
                        CausesValidation="false" />
                </div>
            </div>

            <!-- Grid de técnicos -->
            <div class="grid-section">
                <h2>Lista de Técnicos Registrados</h2>
                <asp:GridView ID="gvTecnicos" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvTecnicos_RowCommand" 
                    DataKeyNames="TecnicoID">
                    <HeaderStyle CssClass="header-tecnicos" />
                    <Columns>
                        <asp:BoundField DataField="TecnicoID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre Completo" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" CommandArgument='<%# Eval("TecnicoID") %>' 
                                    CssClass="btn-edit" CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" CommandArgument='<%# Eval("TecnicoID") %>' 
                                    CssClass="btn-delete" CausesValidation="false"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este técnico?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
