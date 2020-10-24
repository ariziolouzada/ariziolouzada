<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/pages/colaborador/espacobelezacolaborador.Master" AutoEventWireup="true" CodeBehind="comanda.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.colaborador.colaborador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <h2>Comanda</h2>
                    <asp:Literal ID="ltlNomeProfissional" runat="server"></asp:Literal>
                    <%--<small><b>nome do Colaborador</b></small>--%>
                </div>
                <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2">
                    <%--<a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>--%>
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <asp:Literal ID="ltlCabecalho" runat="server"></asp:Literal>
                                </div>
                                <!-- end ibox title -->
                                <div class="ibox-content">
                                    <div class="form-group">
                                        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                    </div>
                                    <div class="form-group">
                                        <label>Data</label>
                                        <input id="txtData" runat="server" class="form-control input-lg m-b" maxlength="10" type="date" />
                                    </div>
                                    <div class="hr-line-dashed"></div>

                                    <div class="form-group">
                                        <label>Cliente</label>
                                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control input-lg m-b"></asp:DropDownList>
                                    </div>
                                    <div class="hr-line-dashed"></div>

                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <i class="fa fa-cog fa-2x"></i><span style="font-size: 12pt;">&nbsp;&nbsp;Serviços</span>
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-lg-8">
                                                <div class="form-group">
                                                    <label>Serviço</label>
                                                    <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control input-lg m-b" onchange="carregaValorServico();"></asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlServico_SelectedIndexChanged" AutoPostBack="true"--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Valor</label>
                                                    <input id="txtValor" runat="server" class="form-control input-lg m-b" type="text" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Novo</label>
                                                    <button class="btn btn-primary btn-lg btn-rounded" onclick="adicionarItemComanda()" type="button"><i class="fa fa-plus-square"></i>&nbsp;Adicionar</button>
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="table-responsive">
                                                        <div id="tabelaItensComanda"></div>
                                                        <asp:Literal ID="ltlServicosComanda" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <!-- end panel-body -->
                                    </div>
                                    <!-- end panel-info -->

                                    <div class="hr-line-dashed"></div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-1">
                                            <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button class="btn btn-outline btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button class="btn btn-outline btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClientClick="return verificaBtnSalvar();" OnClick="btnSalvar_Click" />

                                            <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                                            <asp:HiddenField ID="hdfIdComandaTemp" runat="server" />
                                            <asp:HiddenField ID="hdfIdEmpresaContratante" runat="server" />
                                            <asp:HiddenField ID="hdfAuxiliar" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <!-- end ibox-content -->
                            </div>
                            <!-- end ibox float-e-margins -->
                        </div>
                        <!-- end col-lg-12 -->
                    </div>
                    <!-- end row -->



                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlProfissao" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>


        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">


    <script>

        function msnErro(texto) {
            debugger;
            swal({
                title: "Erro!",
                text: texto,
                type: "error"
            });

        }

        function verificaBtnSalvar() {

            var select = document.getElementById("Conteudo_ddlCliente");
            var idCliente = select.options[select.selectedIndex].value;
            if (idCliente == 0) {
                alert('Selecione o Cliente!!');
                document.getElementById("Conteudo_ddlCliente").focus();
                return false;
            }

            verificaItemComanda();
            debugger;
            if (document.getElementById("<%=hdfAuxiliar.ClientID%>").value == '0') {
                alert('Adicione ao menos um serviço a comanda!!');
                //document.getElementById("Conteudo_ddlProfissão").focus();
                return false;
            }

            return true;
        }

        function verificaItemComanda() {
            //var resultado = '0';
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.VerificaItemComanda(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("<%=hdfAuxiliar.ClientID%>").value = result;
            }
            function onError(result) {
                alert('Erro verificaItemComanda: Contate o Administrador!!');
            }
        }

        function verificaBtnAdicionarItemComanda() {

            select = document.getElementById("Conteudo_ddlServico");
            var idServico = select.options[select.selectedIndex].value;
            if (idServico == 0) {
                alert('Selecione o serviço!!');
                return false;
            }

            if (document.getElementById("<%=txtValor.ClientID%>").value == '') {
                alert('Digite o valor do serviço!!');
                return false;
            }

            return true;
        }

        function adicionarItemComanda() {

            if (verificaBtnAdicionarItemComanda()) {
                debugger;
                var select = document.getElementById("Conteudo_ddlServico");
                var idServico = select.options[select.selectedIndex].value;

                var valor = document.getElementById("<%=txtValor.ClientID%>").value;
                var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;
                var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;
                var qtde = 1;

                window.PageMethods.AdicionarItemComanda(idComanda, idProfissional, idServico, qtde, valor, onSucess, onError);

                function onSucess(result) {
                    debugger;
                    if (document.getElementById("ltlTabelaComandas") != null)
                        document.getElementById("ltlTabelaComandas").innerHTML = '';

                    document.getElementById("tabelaItensComanda").innerHTML = result;

                    select = document.getElementById("Conteudo_ddlServico");
                    select.selectedIndex = 0;

                    document.getElementById("<%=txtValor.ClientID%>").value = '';
                }
                function onError(result) {
                    alert('Erro adicionatItemComanda: Contate o Administrador!!');
                }

            }
        }

        function exclirItemComanda(idItemCda) {
            debugger;
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.ExclirItemComanda(idItemCda, idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (document.getElementById("ltlTabelaComandas") != null)
                    document.getElementById("ltlTabelaComandas").innerHTML = '';

                document.getElementById("tabelaItensComanda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro exclirItemComanda: Contate o Administrador!!');
            }
        }

        function carregaItensComanda() {
            debugger;
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.carregaItensComanda(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("tabelaItensComanda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaItensComanda: Contate o Administrador!!');
            }
        }


        function carregaValorServico() {
            debugger;
            var select = document.getElementById("Conteudo_ddlServico");
            var idServico = select.options[select.selectedIndex].value;

            window.PageMethods.CarregaValorServico(idServico, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("<%=txtValor.ClientID%>").value = result;
            }
            function onError(result) {
                alert('Erro carregaItensComanda: Contate o Administrador!!');
            }
        }

        //$(document).ready(function () {

        //    $(".select2_demo_2").select2();

        //});

        //var config = {
        //    '.chosen-select': {},
        //    '.chosen-select-deselect': { allow_single_deselect: true },
        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
        //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        //    '.chosen-select-width': { width: "95%" }
        //}
        //for (var selector in config) {
        //    $(selector).chosen(config[selector]);
        //}



    </script>


</asp:Content>
