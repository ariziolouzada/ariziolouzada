<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="pgtocomanda.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.financeiro.pgtocomanda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/plugins/select2/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <asp:Literal ID="ltlCabecalho" runat="server"></asp:Literal>
                    <h2>Pagamento de Comanda</h2>
                </div>
                <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2">
                    <%--<a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>--%>
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Comanda</h5>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Número:</label>
                                <div class="col-xs-1 col-sm-1  col-md-1">
                                    <input type="text" class="form-control" runat="server" id="txtNumero" maxlength="3" onkeypress="mascara(this, mcpf);" />
                                </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Data:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10" />
                                </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Cliente:</label>
                                <div class="col-xs-6 col-sm-6  col-md-6">
                                    <input type="text" class="form-control" runat="server" id="txtCliente" />
                                </div>
                            </div>
                            <br />
                            <div class="hr-line-dashed"></div>
                            
                            
                            <div class="form-group">
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Tipo:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlTipoProf" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Profissional:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Servico:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Servico:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="text" class="form-control" runat="server" id="Text2" />
                                </div>
                            </div>
                            <br />
                            <div class="hr-line-dashed"></div>
                            
                                                       

                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12 col-lg-12">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../../js/mascaras.js"></script>
    <script src="../../js/plugins/select2/select2.full.min.js"></script>

    <script>


        $(document).ready(function () {

            $(".select2_demo_2").select2();

        });

        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

    </script>

</asp:Content>
