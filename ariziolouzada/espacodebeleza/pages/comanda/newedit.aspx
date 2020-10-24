<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.comanda.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/plugins/select2/select2.min.css" rel="stylesheet" />
    <link href="../../css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <asp:Literal ID="ltlCabecalho" runat="server"></asp:Literal>
                    <h2>Comanda</h2>
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
                                    <div class="form-group">
                                        <div class="col-xs-4 col-sm-3  col-md-3">
                                            <h5>Comanda Nº 
                                                <asp:Literal ID="ltlnumeroComanda" runat="server"></asp:Literal>
                                            </h5>
                                        </div>
                                        <%--<label class="col-xs-2 col-sm-2 col-md-2 control-label">Situação:</label>--%>
                                        <div class="col-xs-4 col-sm-3  col-md-3">
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="1">EM ABERTA</asp:ListItem>
                                                <asp:ListItem Value="2">FECHADA</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4 col-sm-3  col-md-3">
                                            <asp:Literal ID="ltlTotalCmda" runat="server"></asp:Literal>
                                            <%--<span style="font-size: 16pt; font-weight: bold; color: red;">TOTAL COMANDA R$ 000,00</span>--%>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>

                                    <asp:HiddenField ID="hdfIdComandaTemp" runat="server" />
                                    <div class="form-group">
                                        <%--<label class="col-xs-1 col-sm-1 col-md-1 control-label">Número:</label>
                                <div class="col-xs-1 col-sm-1  col-md-1">
                                    <input type="text" class="form-control" runat="server" id="txtNumero" maxlength="3" onkeypress="mascara(this, mcpf);" />
                                </div>--%>
                                        <label class="col-xs-1 col-sm-1 col-md-1 control-label">Data:</label>
                                        <div class="col-xs-2 col-sm-2  col-md-2">
                                            <input type="date" class="form-control" runat="server" id="txtData" maxlength="10" />
                                        </div>
                                        <label class="col-xs-1 col-sm-1 col-md-1 control-label">Cliente:</label>
                                        <div class="col-xs-4 col-sm-4  col-md-4">
                                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <label class="col-xs-1 col-sm-1 col-md-1 control-label">Pgto:</label>
                                        <div class="col-xs-3 col-sm-3 col-md-3">
                                            <asp:DropDownList ID="ddlFormaPgto" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPgto_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                                <asp:ListItem Value="1">Dinheiro</asp:ListItem>
                                                <asp:ListItem Value="2">Cartão Débito</asp:ListItem>
                                                <asp:ListItem Value="3">Cartão Crédito 30 dias</asp:ListItem>
                                                <asp:ListItem Value="4">Cartão Crédito Parcelado</asp:ListItem>
                                                <asp:ListItem Value="5">Cheque</asp:ListItem>
                                                <asp:ListItem Value="6">Mais de uma forma de pgto</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="hr-line-dashed"></div>

                                    <asp:Panel ID="pnlFormasDePgto" runat="server" Visible="false">

                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <i class="fa fa-money fa-2x"></i><span style="font-size: 12pt;">Formas de Pagamento</span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group">
                                                    <label class="col-xs-2 col-sm-2 col-md-2 control-label">Forma de pgto:</label>
                                                    <div class="col-xs-4 col-sm-4  col-md-4">
                                                        <asp:DropDownList ID="ddlFormasPgtoPnl" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPgto_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                                            <asp:ListItem Value="1">Dinheiro</asp:ListItem>
                                                            <asp:ListItem Value="2">Cartão Débito</asp:ListItem>
                                                            <asp:ListItem Value="3">Cartão Crédito 30 dias</asp:ListItem>
                                                            <asp:ListItem Value="4">Cartão Crédito Parcelado</asp:ListItem>
                                                            <asp:ListItem Value="5">Cheque</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-xs-1 col-sm-1 col-md-1 control-label">Valor:</label>
                                                    <div class="col-xs-2 col-sm-2  col-md-2">
                                                        <input type="text" class="form-control" runat="server" id="txtValorPgto" />
                                                    </div>
                                                    <div class="col-xs-1 col-sm-1  col-md-1">
                                                        <button class="btn btn-primary" type="button" onclick="adicionarFormaPgto()"><i class="fa fa-plus-square"></i>&nbsp;Adicionar</button>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="hr-line-dashed"></div>

                                                <div class="table-responsive">
                                                    <div id="tabelaFormasPgtoCmda"></div>
                                                    <asp:Literal ID="ltlTabelaFormasPgtoCmda" runat="server"></asp:Literal>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="hr-line-dashed"></div>
                                    </asp:Panel>

                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <i class="fa fa-cog fa-2x"></i><span style="font-size: 12pt;">Serviços</span>
                                        </div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Tipo:</label>
                                                <div class="col-xs-3 col-sm-3  col-md-3">
                                                    <asp:DropDownList ID="ddlProfissao" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProfissao_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Profissional:</label>
                                                <div class="col-xs-3 col-sm-3  col-md-3">
                                                    <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <label class="col-xs-4 col-sm-4 col-md-4 control-label"></label>
                                            </div>
                                            <br />
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Servico:</label>
                                                <div class="col-xs-3 col-sm-3  col-md-3">
                                                    <asp:DropDownList ID="ddlServico" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlServico_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Valor:</label>
                                                <div class="col-xs-2 col-sm-2  col-md-2">
                                                    <input type="text" class="form-control" runat="server" id="txtValor" />
                                                </div>
                                                <div class="col-xs-1 col-sm-1  col-md-1">
                                                    <button class="btn btn-primary" type="button" onclick="adicionarItemComanda()"><i class="fa fa-plus-square"></i>&nbsp;Adicionar</button>
                                                </div>
                                                <label class="col-xs-4 col-sm-4 col-md-4 control-label"></label>
                                            </div>
                                            <br />
                                            <div class="hr-line-dashed"></div>

                                            <div class="table-responsive">
                                                <div id="tabelaItensComanda"></div>
                                                <asp:Literal ID="ltlTabelaComandas" runat="server"></asp:Literal>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="hr-line-dashed"></div>

                                    <div class="panel panel-warning">
                                        <div class="panel-heading">
                                            <i class="fa fa-paypal fa-2x"></i><span style="font-size: 12pt;">Produtos</span>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Produto:</label>
                                                <div class="col-xs-5 col-sm-5  col-md-5">
                                                    <%-- OnSelectedIndexChanged="ddlProdutos_SelectedIndexChanged" AutoPostBack="true"--%>
                                                    <asp:DropDownList ID="ddlProduto" runat="server" CssClass="form-control" onchange="carregaValorProduto()"></asp:DropDownList>
                                                </div>
                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Valor:</label>
                                                <div class="col-xs-2 col-sm-2  col-md-2">
                                                    <input type="text" class="form-control" runat="server" id="txtValorProd" />
                                                </div>

                                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Qtde:</label>
                                                <div class="col-xs-1 col-sm-1  col-md-1">
                                                    <input type="number" class="form-control" runat="server" id="txtQtdeProd" value="1" />
                                                </div>
                                                <div class="col-xs-1 col-sm-1  col-md-1">
                                                    <button class="btn btn-primary" type="button" onclick="adicionarProdutoComanda()"><i class="fa fa-plus-square"></i>&nbsp;Adicionar</button>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="hr-line-dashed"></div>

                                            <div class="table-responsive">
                                                <div id="tabelaItensProdutos"></div>
                                                <asp:Literal ID="ltlTabelaProdutos" runat="server"></asp:Literal>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 col-md-2 control-label"></label>
                                        <div class="col-md-9 col-sm-10 ">

                                            <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button class="btn btn-outline btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button class="btn btn-outline btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" OnClientClick="return verificaBtnSalvar();" />
                                            <asp:HiddenField ID="hdfIdAgenda" runat="server" />
                                            <asp:HiddenField ID="hdfIdEmpresaContratante" runat="server" />
                                            <asp:HiddenField ID="hdfAuxiliar" runat="server" />
                                            <asp:HiddenField ID="hdfIdFormaPgto" runat="server" />
                                        </div>
                                        <br />
                                        <br />

                                    </div>

                                </div>


                            </div>

                        </div>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlProfissao" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlFormaPgto" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>


        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../../js/mascaras.js"></script>
    <script src="../../js/plugins/select2/select2.full.min.js"></script>
    <script src="../../js/plugins/sweetalert/sweetalert.min.js"></script>

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
            var select = document.getElementById("Conteudo_ddlProfissao");
            var idHoraFinal = select.options[select.selectedIndex].value;
            if (idHoraFinal == 0) {
                alert('Selecione o Tipo!!');
                return false;
            }

            select = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = select.options[select.selectedIndex].value;
            if (idProfissional == 0) {
                alert('Selecione o Profissional!!');
                return false;
            }
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
                //debugger;
                var select = document.getElementById("Conteudo_ddlProfissional");
                var idProfissional = select.options[select.selectedIndex].value;

                select = document.getElementById("Conteudo_ddlServico");
                var idServico = select.options[select.selectedIndex].value;

                var valor = document.getElementById("<%=txtValor.ClientID%>").value;
                var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;
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

        function atualizaTotalComanda() {
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;
            window.PageMethods.AtualizaTotalComanda(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("lblTotalCmda").innerHTML = result;
                //document.getElementById("<%=ltlTotalCmda.ClientID%>").innerHTML = result;
            }
            function onError(result) {
                alert('Erro atualizaTotalComanda: Contate o Administrador!!');
            }
        }

        function exclirItemComanda(idItemCda) {
            debugger;
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.ExclirItemComanda(idItemCda, idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("ltlTabelaComandas").innerHTML = '';
                document.getElementById("tabelaItensComanda").innerHTML = result;

                atualizaTotalComanda();
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


        function verificaBtnAdicionarProdutoComanda() {
            var select = document.getElementById("Conteudo_ddlProduto");
            var idProduto = select.options[select.selectedIndex].value;
            if (idProduto == 0) {
                alert('Selecione o Produto!!');
                return false;
            }

            if (document.getElementById("<%=txtValorProd.ClientID%>").value == '') {
                alert('Digite o valor do Protudo!!');
                return false;
            }

            if (document.getElementById("<%=txtQtdeProd.ClientID%>").value == '') {
                alert('Digite a Quantidade do Protudo!!');
                return false;
            }

            return true;
        }

        function carregaValorProduto() {
            debugger;
            var idProduto = document.getElementById("<%=ddlProduto.ClientID%>").value;

            window.PageMethods.CarregaValorProduto(idProduto, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("<%=txtValorProd.ClientID%>").value = result;
                //document.getElementById("tabelaItensComanda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaItensComanda: Contate o Administrador!!');
            }
        }

        function adicionarProdutoComanda() {

            if (verificaBtnAdicionarProdutoComanda()) {
                debugger;
                var select = document.getElementById("Conteudo_ddlProduto");
                var idProduto = select.options[select.selectedIndex].value;

                var valor = document.getElementById("<%=txtValorProd.ClientID%>").value;
                var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;
                var qtde = document.getElementById("<%=txtQtdeProd.ClientID%>").value;

                window.PageMethods.AdicionarProdutoComanda(idComanda, idProduto, qtde, valor, onSucess, onError);

                function onSucess(result) {
                    debugger;
                    if (document.getElementById("ltlTabelaProdutos") != null)
                        document.getElementById("ltlTabelaProdutos").innerHTML = '';

                    document.getElementById("tabelaItensProdutos").innerHTML = result;

                    select = document.getElementById("Conteudo_ddlProduto");
                    select.selectedIndex = 0;

                    document.getElementById("<%=txtValorProd.ClientID%>").value = '';
                    document.getElementById("<%=txtQtdeProd.ClientID%>").value = '1';
                }
                function onError(result) {
                    alert('Erro adicionarProdutoComanda: Contate o Administrador!!');
                }

            }
        }


        function exclirItemProdutoComanda(idItemCda) {
            debugger;
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.ExclirItemProdutoComanda(idItemCda, idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (document.getElementById("ltlTabelaProdutos") != null)
                    document.getElementById("ltlTabelaProdutos").innerHTML = '';

                document.getElementById("tabelaItensProdutos").innerHTML = result;

                atualizaTotalComanda();
            }
            function onError(result) {
                alert('Erro exclirItemProdutoComanda: Contate o Administrador!!');
            }
        }

        function verificaBtnAdicionarFormaPgtoComanda() {
            var select = document.getElementById("Conteudo_ddlFormasPgtoPnl");
            var idPgto = select.options[select.selectedIndex].value;
            if (idPgto == 0) {
                alert('Selecione a Forma de Pgto!!');
                return false;
            }

            if (document.getElementById("<%=txtValorPgto.ClientID%>").value == '') {
                alert('Digite o valor do Pagto!!');
                return false;
            }

            return true;
        }

        function adicionarFormaPgto() {

            if (verificaBtnAdicionarFormaPgtoComanda()) {
                debugger;
                //var select = document.getElementById("Conteudo_ddlFormasPgtoPnl");
                var idFormaPgto = document.getElementById("Conteudo_ddlFormasPgtoPnl").value;
                //= select.options[select.selectedIndex].value;

                var valor = document.getElementById("<%=txtValorPgto.ClientID%>").value;
                var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

                window.PageMethods.AdicionarFormaPgtoComanda(idComanda, idFormaPgto, valor, onSucess, onError);

                function onSucess(result) {
                    debugger;
                    if (document.getElementById("ltlTabelaFormasPgtoCmda") != null)
                        document.getElementById("ltlTabelaFormasPgtoCmda").innerHTML = '';

                    document.getElementById("tabelaFormasPgtoCmda").innerHTML = result;

                    select = document.getElementById("Conteudo_ddlFormasPgtoPnl");
                    select.selectedIndex = 0;

                    document.getElementById("<%=txtValorPgto.ClientID%>").value = '';
                }
                function onError(result) {
                    alert('Erro adicionarFormaPgtoComanda: Contate o Administrador!!');
                }

            }
        }

        function exclirItemFormaPgtoComanda(idItemCda) {
            debugger;
            var idComanda = document.getElementById("<%=hdfIdComandaTemp.ClientID%>").value;

            window.PageMethods.ExclirItemFormaPgtoComanda(idItemCda, idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (document.getElementById("ltlTabelaFormasPgtoCmda") != null)
                    document.getElementById("ltlTabelaFormasPgtoCmda").innerHTML = '';

                document.getElementById("tabelaFormasPgtoCmda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro exclirItemFormaPgtoComanda: Contate o Administrador!!');
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

