<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/pages/colaborador/espacobelezacolaborador.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.colaborador._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <asp:Literal ID="ltlDataHoje" runat="server"></asp:Literal>
                    <%--<span class="label label-success pull-right">Monthly</span>
                    <h5>Hoje</h5>--%>
                </div>
                <div class="ibox-content">
                    <asp:Literal ID="ltlTotalHoje" runat="server"></asp:Literal>
                    <%--<h1 class="no-margins">386,200</h1>
                    <div class="stat-percent font-bold text-success">98% <i class="fa fa-bolt"></i></div>--%>
                    <small>Total Hoje</small>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <asp:Literal ID="ltlSemana" runat="server"></asp:Literal>
                    <%--<span class="label label-info pull-right">Annual</span>--%>
                    <h5>Semana</h5>
                </div>
                <div class="ibox-content">
                    <asp:Literal ID="ltlTotalSemana" runat="server"></asp:Literal>
                    <%--<h1 class="no-margins">80,800</h1>
                    <div class="stat-percent font-bold text-info">20% <i class="fa fa-level-up"></i></div>--%>
                    <small>Total Semana</small>
                </div>
            </div>
        </div>

        <div class="col-lg-4">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <span class="label label-primary pull-right">Today</span>
                    <h5>Comandas & Serviços</h5>
                </div>
                <div class="ibox-content">

                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="ltlComandas" runat="server"></asp:Literal>
                            <%--<h1 class="no-margins">406,42</h1>
                            <div class="font-bold text-navy">44% <i class="fa fa-level-up"></i><small>Rapid pace</small></div>--%>
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="ltlServicos" runat="server"></asp:Literal>
                            <%--<h1 class="no-margins">206,12</h1>
                            <div class="font-bold text-navy">22% <i class="fa fa-cog"></i><small>Serviços</small></div>--%>
                        </div>
                    </div>

                </div>
                <!--Fim ibox-content -->
            </div>
        </div>

    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <div class="col-lg-2">
                        <h5>Serviços realizados</h5>
                    </div>
                    <label class="col-sm-1 col-md-1 control-label">filtro:</label>
                    <div class="col-lg-2">
                        <form id="form2" runat="server">
                            <asp:DropDownList ID="ddlFiltro" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                                <asp:ListItem Value="1">Hoje</asp:ListItem>
                                <asp:ListItem Value="2">Da Semana</asp:ListItem>
                            </asp:DropDownList>
                        </form>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="table-responsive">
                        <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script src="../../js/jquery.dataTables/jquery.dataTables.min.js"></script>

    <script>

        $(document).ready(function () {
            //debugger;
            $('#tabelaClientes').DataTable({
                "ordering": false,
                "language": {
                    "lengthMenu": "Mostrando _MENU_ registros por página",
                    "zeroRecords": "Nada encontrado",
                    /* "info": "Mostrando página _PAGE_ de _PAGES_",*/
                    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                    "infoEmpty": "Nenhum registro disponível",
                    "infoFiltered": "(filtrado de _MAX_ registros no total)",
                    "sLoadingRecords": "Carregando...",
                    "sProcessing": "Processando...",
                    "sSearch": "Pesquisar",
                    "oPaginate": {
                        "sNext": "Próximo",
                        "sPrevious": "Anterior",
                        "sFirst": "Primeiro",
                        "sLast": "Último"
                    }
                }
            });
        });




    </script>

</asp:Content>
