<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Asignaciones.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.Asignaciones" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Asignaciones - Sistema UH</title>
    
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header de la página -->
            <div class="page-header page-header-asignaciones">
                <h1><i class="fas fa-user-check"></i> Gestión de Asignaciones</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Formulario de Captura -->
            <div class="form-section">
                <h2 class="border-asignaciones">
                    <asp:Label ID="lblTitulo" runat="server" Text="Nueva Asignación de Técnico"></asp:Label>
                </h2>

                <!-- Campo oculto para el ID -->
                <asp:HiddenField ID="hfAsignacionID" runat="server" Value="0" />

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" Visible="false"></asp:Label>

                <div class="form-group">
                    <label for="ddlReparacion">Reparación *</label>
                    <asp:DropDownList ID="ddlReparacion" runat="server" CssClass="form-control focus-asignaciones">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvReparacion" runat="server" 
                        ControlToValidate="ddlReparacion" 
                        InitialValue="0"
                        ErrorMessage="Debe seleccionar una reparación" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="ddlTecnico">Técnico *</label>
                    <asp:DropDownList ID="ddlTecnico" runat="server" CssClass="form-control focus-asignaciones">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTecnico" runat="server" 
                        ControlToValidate="ddlTecnico" 
                        InitialValue="0"
                        ErrorMessage="Debe seleccionar un técnico" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="txtFechaAsignacion">Fecha de Asignación *</label>
                    <asp:TextBox ID="txtFechaAsignacion" runat="server" CssClass="form-control focus-asignaciones" 
                        TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFechaAsignacion" runat="server" 
                        ControlToValidate="txtFechaAsignacion" 
                        ErrorMessage="La fecha de asignación es obligatoria" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Asignación" 
                    CssClass="btn btn-primary btn-primary-asignaciones" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                    CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
            </div>

            <!-- Grid de Asignaciones -->
            <div class="grid-section">
                <h2>Listado de Asignaciones</h2>
                <asp:GridView ID="gvAsignaciones" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvAsignaciones_RowCommand">
                    <HeaderStyle CssClass="header-asignaciones" />
                    <Columns>
                        <asp:BoundField DataField="AsignacionID" HeaderText="ID" />
                        <asp:BoundField DataField="ReparacionID" HeaderText="ID Reparación" />
                        <asp:BoundField DataField="NombreTecnico" HeaderText="Técnico" />
                        <asp:BoundField DataField="FechaAsignacion" HeaderText="Fecha Asignación" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" 
                                    CommandArgument='<%# Eval("AsignacionID") %>' 
                                    CssClass="btn-edit" 
                                    CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" 
                                    CommandArgument='<%# Eval("AsignacionID") %>' 
                                    CssClass="btn-delete" 
                                    OnClientClick="return confirm('¿Está seguro de eliminar esta asignación?');" 
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
