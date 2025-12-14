<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="EjemploLogin.Principal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
<title>Sistema de Gestión de Reparaciones - UH</title>
<link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css" />
<link href="https://fonts.googleapis.com/css?family=Lato:400,700,400italic,700italic" rel="stylesheet" type="text/css" />
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
<link href="css/dashboard.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Navegación -->
        <nav class="navbar">
            <div class="container">
                <a class="navbar-brand" href="Principal.aspx">Sistema de Reparaciones</a>
                <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn-logout" OnClick="btnLogout_Click">
                    <i class="fas fa-sign-out-alt"></i> Cerrar Sesión
                </asp:LinkButton>
            </div>
        </nav>

        <!-- Masthead -->
        <header class="masthead">
            <div class="container">
                <div class="masthead-heading">
                    <i class="fas fa-tools"></i> Sistema de Gestión
                </div>
                <div class="divider-custom">
                    <div class="divider-custom-line"></div>
                    <div class="divider-custom-icon"><i class="fas fa-star"></i></div>
                    <div class="divider-custom-line"></div>
                </div>
                <p class="masthead-subheading">Universidad Hispanoamericana</p>
                <div class="welcome-name">
                    Bienvenido/a: <asp:Label ID="Lbnombre" runat="server"></asp:Label>
                </div>
            </div>
        </header>

        <!-- Sección del Portfolio / Módulos -->
        <section class="page-section" id="portfolio">
            <div class="container">
                <h2 class="page-section-heading">Módulos del Sistema</h2>
                <div class="divider-custom">
                    <div class="divider-custom-line" style="background-color: var(--bs-secondary);"></div>
                    <div class="divider-custom-icon" style="color: var(--bs-secondary);">
                        <i class="fas fa-star"></i>
                    </div>
                    <div class="divider-custom-line" style="background-color: var(--bs-secondary);"></div>
                </div>

                <div class="row">
                    <!-- Módulo Usuarios -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/Usuarios.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-users"></i>
                                    </div>
                                    <div class="portfolio-item-title">Usuarios</div>
                                    <div class="portfolio-item-description">Gestionar clientes del sistema</div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <!-- Módulo Técnicos -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/Tecnicos.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-user-cog"></i>
                                    </div>
                                    <div class="portfolio-item-title">Técnicos</div>
                                    <div class="portfolio-item-description">Administrar personal técnico</div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <!-- Módulo Equipos -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/Equipos.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-laptop"></i>
                                    </div>
                                    <div class="portfolio-item-title">Equipos</div>
                                    <div class="portfolio-item-description">Gestionar equipos registrados</div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <!-- Módulo Reparaciones -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/Reparaciones.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-tools"></i>
                                    </div>
                                    <div class="portfolio-item-title">Reparaciones</div>
                                    <div class="portfolio-item-description">Administrar reparaciones</div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <!-- Módulo Detalles de Reparación -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/DetallesReparacion.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-clipboard-list"></i>
                                    </div>
                                    <div class="portfolio-item-title">Detalles</div>
                                    <div class="portfolio-item-description">Detalles de reparaciones</div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <!-- Módulo Asignaciones -->
                    <div class="col-lg-4 col-md-6">
                        <a href="CapaPresentacion/Asignaciones.aspx" style="text-decoration: none;">
                            <div class="portfolio-item">
                                <div class="portfolio-item-caption">
                                    <div class="portfolio-item-icon">
                                        <i class="fas fa-user-check"></i>
                                    </div>
                                    <div class="portfolio-item-title">Asignaciones</div>
                                    <div class="portfolio-item-description">Asignar técnicos a reparaciones</div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </section>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="footer-text">
                    <p>Sistema de Gestión de Reparaciones &copy; 2025 Universidad Hispanoamericana</p>
                </div>
            </div>
        </footer>

    </form>
</body>
</html>
