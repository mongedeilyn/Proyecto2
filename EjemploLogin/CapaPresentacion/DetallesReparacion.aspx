<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesReparacion.aspx.cs" Inherits="EjemploLogin.CapaPresentacion.DetallesReparacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Gestión de Detalles de Reparación - Sistema UH</title>
    
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <link href="../css/modules.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Header de la página -->
            <div class="page-header page-header-detalles">
                <h1><i class="fas fa-clipboard-list"></i> Gestión de Detalles de Reparación</h1>
                <a href="../Principal.aspx" class="btn-back">
                    <i class="fas fa-arrow-left"></i> Volver al Menú Principal
                </a>
            </div>

            <!-- Formulario de Captura -->
            <div class="form-section">
                <h2 class="border-detalles">
                    <asp:Label ID="lblTitulo" runat="server" Text="Registrar Nuevo Detalle"></asp:Label>
                </h2>

                <!-- Campo oculto para el ID -->
                <asp:HiddenField ID="hfDetalleID" runat="server" Value="0" />

                <!-- Mensaje de éxito/error -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" Visible="false"></asp:Label>

                <div class="form-group">
                    <label for="ddlReparacion">Reparación *</label>
                    <asp:DropDownList ID="ddlReparacion" runat="server" CssClass="form-control focus-detalles">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvReparacion" runat="server" 
                        ControlToValidate="ddlReparacion" 
                        InitialValue="0"
                        ErrorMessage="Debe seleccionar una reparación" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="txtDescripcion">Descripción *</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control focus-detalles" 
                        TextMode="MultiLine" Rows="4" MaxLength="500" placeholder="Describa el trabajo realizado..."></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                        ControlToValidate="txtDescripcion" 
                        ErrorMessage="La descripción es obligatoria" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="txtFechaInicio">Fecha de Inicio *</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control focus-detalles" 
                        TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" 
                        ControlToValidate="txtFechaInicio" 
                        ErrorMessage="La fecha de inicio es obligatoria" 
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="txtFechaFin">Fecha de Fin (Opcional)</label>
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control focus-detalles" 
                        TextMode="Date"></asp:TextBox>
                </div>

                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Detalle" 
                    CssClass="btn btn-primary btn-primary-detalles" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                    CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
            </div>

            <!-- Grid de Detalles -->
            <div class="grid-section">
                <h2>Listado de Detalles de Reparación</h2>
                <asp:GridView ID="gvDetalles" runat="server" CssClass="custom-grid" 
                    AutoGenerateColumns="False" OnRowCommand="gvDetalles_RowCommand">
                    <HeaderStyle CssClass="header-detalles" />
                    <Columns>
                        <asp:BoundField DataField="DetalleID" HeaderText="ID" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="-" />
                        <asp:BoundField DataField="ReparacionID" HeaderText="ID Reparación" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                                    CommandName="Editar" 
                                    CommandArgument='<%# Eval("DetalleID") %>' 
                                    CssClass="btn-edit" 
                                    CausesValidation="false" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="Eliminar" 
                                    CommandArgument='<%# Eval("DetalleID") %>' 
                                    CssClass="btn-delete" 
                                    OnClientClick="return confirm('¿Está seguro de eliminar este detalle?');" 
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
