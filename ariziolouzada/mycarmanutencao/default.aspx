<%@ Page Title="" Language="C#" MasterPageFile="~/mycarmanutencao/mycar.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.mycarmanutencao._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/adminlte.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>My Car Manutenção</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <!-- Default box -->
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Dashboard</h3>
                        </div>
                        <div class="card-body">
                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Marca / Modelo</label>
                                        <asp:DropDownList ID="ddlCarro" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCarro_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <section class="content">
                                <div class="container-fluid">
                                    <!-- Info boxes -->
                                    <div class="row">
                                        <div class="col-12 col-sm-6 col-md-4">
                                            <div class="info-box mb-4">
                                                <span class="info-box-icon bg-success elevation-1"><i class="fas fa-shopping-cart"></i></span>
                                                <div class="info-box-content">
                                                    <span class="info-box-text">Valor de Compra</span>
                                                    <span class="info-box-number">
                                                        <asp:Label ID="lblValorCompra" runat="server" Text="R$0,00"></asp:Label>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                            <!-- /.info-box -->
                                        </div>
                                        <!-- /.col -->
                                        <div class="col-12 col-sm-6 col-md-4">
                                            <div class="info-box">
                                                <span class="info-box-icon bg-info elevation-1"><i class="fas fa-cog"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">Gasto Manutenção</span>
                                                    <span class="info-box-number">
                                                        <asp:Label ID="lblGastoManutencao" runat="server" Text="R$0,00"></asp:Label>
                                            <%--<small>%</small>--%>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                            <!-- /.info-box -->
                                        </div>
                                        <!-- /.col -->
                                        <div class="col-12 col-sm-6 col-md-4">
                                            <div class="info-box mb-4">
                                                <span class="info-box-icon bg-danger elevation-1"><i class="fa fa-clock-o" aria-hidden="true"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">Tempo de Propriedade</span>
                                                    <span class="info-box-number">
                                                        <asp:Label ID="lblTempoPropriedade" runat="server" Text="0"></asp:Label>
                                                    </span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                            <!-- /.info-box -->
                                        </div>
                                        <!-- /.col -->

<%--                                        <!-- fix for small devices only -->
                                        <div class="clearfix hidden-md-up"></div>

                                        <div class="col-12 col-sm-6 col-md-3">
                                            <div class="info-box mb-3">
                                                <span class="info-box-icon bg-warning elevation-1"><i class="fas fa-users"></i></span>

                                                <div class="info-box-content">
                                                    <span class="info-box-text">New Members</span>
                                                    <span class="info-box-number">2,000</span>
                                                </div>
                                                <!-- /.info-box-content -->
                                            </div>
                                            <!-- /.info-box -->
                                        </div>
                                        <!-- /.col -->--%>

                                    </div>
                                    <!-- /.row -->

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="card">
                                                <div class="card-header">
                                                    <h5 class="card-title">Detalhamento das Manutenções</h5>

                                                    <div class="card-tools">
                                                        <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                            <i class="fas fa-minus"></i>
                                                        </button>
                                                       <%-- <div class="btn-group">
                                                            <button type="button" class="btn btn-tool dropdown-toggle" data-toggle="dropdown">
                                                                <i class="fas fa-wrench"></i>
                                                            </button>
                                                            <div class="dropdown-menu dropdown-menu-right" role="menu">
                                                                <a href="#" class="dropdown-item">Action</a>
                                                                <a href="#" class="dropdown-item">Another action</a>
                                                                <a href="#" class="dropdown-item">Something else here</a>
                                                                <a class="dropdown-divider"></a>
                                                                <a href="#" class="dropdown-item">Separated link</a>
                                                            </div>
                                                        </div>
                                                        <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                            <i class="fas fa-times"></i>
                                                        </button>--%>
                                                    </div>
                                                </div>
                                                <!-- /.card-header -->
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <p class="text-center">
                                                                <strong>Sales: 1 Jan, 2014 - 30 Jul, 2014</strong>
                                                            </p>

                                                            <div class="chart">
                                                                <!-- Sales Chart Canvas -->
                                                                <canvas id="salesChart" height="180" style="height: 180px;"></canvas>
                                                            </div>
                                                            <!-- /.chart-responsive -->
                                                        </div>
                                                        <!-- /.col -->
                                                        <div class="col-md-4">
                                                            <p class="text-center">
                                                                <strong>Goal Completion</strong>
                                                            </p>

                                                            <div class="progress-group">
                                                                Add Products to Cart
                     
                                                    <span class="float-right"><b>160</b>/200</span>
                                                                <div class="progress progress-sm">
                                                                    <div class="progress-bar bg-primary" style="width: 80%"></div>
                                                                </div>
                                                            </div>
                                                            <!-- /.progress-group -->

                                                            <div class="progress-group">
                                                                Complete Purchase
                     
                                                    <span class="float-right"><b>310</b>/400</span>
                                                                <div class="progress progress-sm">
                                                                    <div class="progress-bar bg-danger" style="width: 75%"></div>
                                                                </div>
                                                            </div>

                                                            <!-- /.progress-group -->
                                                            <div class="progress-group">
                                                                <span class="progress-text">Visit Premium Page</span>
                                                                <span class="float-right"><b>480</b>/800</span>
                                                                <div class="progress progress-sm">
                                                                    <div class="progress-bar bg-success" style="width: 60%"></div>
                                                                </div>
                                                            </div>

                                                            <!-- /.progress-group -->
                                                            <div class="progress-group">
                                                                Send Inquiries
                     
                                                    <span class="float-right"><b>250</b>/500</span>
                                                                <div class="progress progress-sm">
                                                                    <div class="progress-bar bg-warning" style="width: 50%"></div>
                                                                </div>
                                                            </div>
                                                            <!-- /.progress-group -->
                                                        </div>
                                                        <!-- /.col -->
                                                    </div>
                                                    <!-- /.row -->
                                                </div>
                                                <!-- ./card-body -->
                                                <div class="card-footer">
                                                    <div class="row">
                                                        <div class="col-sm-3 col-6">
                                                            <div class="description-block border-right">
                                                                <span class="description-percentage text-success"><i class="fas fa-caret-up"></i>
                                                                    <asp:Label ID="lblPorcentagemVenda" runat="server" Text="0%"></asp:Label>
                                                                    </span>
                                                                <h5 class="description-header">
                                                                    <asp:Label ID="lblValorVenda" runat="server" Text="R$0,00"></asp:Label>
                                                                </h5>
                                                                <span class="description-text">VALOR DA VENDA</span>
                                                            </div>
                                                            <!-- /.description-block -->
                                                        </div>
                                                        <!-- /.col -->
                                                        <div class="col-sm-3 col-6">
                                                            <div class="description-block border-right">
                                                                <span class="description-percentage text-warning"><i class="fas fa-caret-left"></i>0%</span>
                                                                <h5 class="description-header">$10,390.90</h5>
                                                                <span class="description-text">TOTAL COST</span>
                                                            </div>
                                                            <!-- /.description-block -->
                                                        </div>
                                                        <!-- /.col -->
                                                        <div class="col-sm-3 col-6">
                                                            <div class="description-block border-right">
                                                                <span class="description-percentage text-success"><i class="fas fa-caret-up"></i>20%</span>
                                                                <h5 class="description-header">$24,813.53</h5>
                                                                <span class="description-text">TOTAL PROFIT</span>
                                                            </div>
                                                            <!-- /.description-block -->
                                                        </div>
                                                        <!-- /.col -->
                                                        <div class="col-sm-3 col-6">
                                                            <div class="description-block">
                                                                <span class="description-percentage text-danger"><i class="fas fa-caret-down"></i>18%</span>
                                                                <h5 class="description-header">1200</h5>
                                                                <span class="description-text">GOAL COMPLETIONS</span>
                                                            </div>
                                                            <!-- /.description-block -->
                                                        </div>
                                                    </div>
                                                    <!-- /.row -->
                                                </div>
                                                <!-- /.card-footer -->
                                            </div>
                                            <!-- /.card -->
                                        </div>
                                        <!-- /.col -->
                                    </div>
                                    <!-- /.row -->
                                </div>

                            </section>

                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            Footer       
                        </div>
                        <!-- /.card-footer-->
                    </div>
                    <!-- /.card -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_scripts" runat="server">
    
    <!-- ChartJS -->
    <script src="<%= ResolveUrl("plugins/chart.js/Chart.min.js")%>"></script>
    <script src="<%= ResolveUrl("dist/js/pages/dashboard2.js")%>"></script>

</asp:Content>
