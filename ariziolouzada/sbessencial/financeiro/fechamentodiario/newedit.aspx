<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.sbessencial.financeiro.fechamentodiario.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/plugins/dataTables/datatables.min.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text="CAIXA"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Tipo:</label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:DropDownList ID="ddlTipo" runat="server" class="form-control " AutoPostBack="True" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="0">Selecione...</asp:ListItem>--%>
                                        <asp:ListItem Value="1">Entrada</asp:ListItem>
                                        <asp:ListItem Value="2">Saída</asp:ListItem>
                                        <asp:ListItem Value="3">Depósito</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-md-2 control-label">Data:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="date" class="form-control" runat="server" id="txtData">
                                </div>

                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Cliente:</label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:DropDownList ID="ddlCliente" runat="server" class="form-control ">
                                    </asp:DropDownList>
                                </div>

                                <label class="col-sm-2 col-md-2 control-label">Forma Pgto:</label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:DropDownList ID="ddlFormaPgto" runat="server" class="form-control ">
                                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="1">Dinheiro</asp:ListItem>
                                        <asp:ListItem Value="2">Cartão Débito</asp:ListItem>
                                        <asp:ListItem Value="3">Cartão Crédito 30 dias</asp:ListItem>
                                        <asp:ListItem Value="4">Cartão Crédito Parcelado</asp:ListItem>
                                        <asp:ListItem Value="5">Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="hr-line-dashed"></div>

                            <asp:Panel ID="pnlTipoSaida" runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="col-sm-2 col-md-2 control-label">Saída:</label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:DropDownList ID="ddlTipoSaida" runat="server" class="form-control ">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-sm-2 col-md-2 control-label">Valor:</label>
                                    <div class="col-sm-2 col-md-2">
                                        <input type="text" class="form-control" runat="server" id="txtValorSaida">
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                            </asp:Panel>

                            <asp:Panel ID="pnlServico" runat="server">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Serviços realizados pelo Cliente 
                                    </div>
                                    <div class="panel-body">

                                        <div class="form-group">
                                            <label class="col-sm-2 col-md-2 control-label">Serviço:</label>
                                            <div class="col-sm-5 col-md-5">
                                                <%-- AutoPostBack="True" OnSelectedIndexChanged="ddlServico_SelectedIndexChanged--%>
                                                <asp:DropDownList ID="ddlServico" runat="server" class="form-control" onchange="selecaoDdlServico()">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfIdServicosFluxoCaixa" runat="server" />
                                            </div>
                                            <label class="col-sm-1 col-md-1 control-label">Valor:</label>
                                            <div class="col-sm-2 col-md-2">
                                                <input type="text" class="form-control" runat="server" id="txtValorServico">
                                            </div>
                                            <div class="col-sm-2 col-md-2">
                                                <button class="btn btn-primary" type="button" onclick="addServicoTabela()"><i class="fa fa-plus-square"></i>Adicionar</button>
                                            </div>
                                        </div>

                                        <div class="hr-line-dashed"></div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-md-2 control-label"></label>
                                            <div class="col-sm-10 col-md-10">
                                                <asp:Literal ID="ltltabelaServicos" runat="server"></asp:Literal>
                                                <div id="tabelaServicos"></div>
                                            </div>
                                        </div>

                                    </div>

                                    <!-- Fim panel-body -->
                                </div>
                                <div class="hr-line-dashed"></div>

                            </asp:Panel>


                            <asp:Panel ID="pnlProduto" runat="server">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        Produtos adquiridos pelo Cliente 
                                    </div>
                                    <div class="panel-body">

                                        <div class="form-group">
                                            <label class="col-sm-1 col-md-1 control-label">Produto:</label>
                                            <div class="col-sm-4 col-md-4">
                                                <%-- AutoPostBack="True" OnSelectedIndexChanged="ddlServico_SelectedIndexChanged--%>
                                                <asp:DropDownList ID="ddlProduto" runat="server" class="form-control" onchange="selecaoDdlProduto()">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfIdProdutosFluxoCaixa" runat="server" />
                                            </div>
                                            <label class="col-sm-1 col-md-1 control-label">Valor:</label>
                                            <div class="col-sm-2 col-md-2">
                                                <input type="text" class="form-control" runat="server" id="txtValorProduto">
                                            </div>

                                            <label class="col-sm-1 col-md-1 control-label">Qtde:</label>
                                            <div class="col-sm-1 col-md-1">
                                                <input type="text" class="form-control" runat="server" id="txtQtdeProduto" value="1">
                                            </div>

                                            <div class="col-sm-2 col-md-2">
                                                <button class="btn btn-success" type="button" onclick="addProdutoTabela()"><i class="fa fa-plus-square"></i>Adicionar</button>
                                            </div>
                                        </div>

                                        <div class="hr-line-dashed"></div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-md-2 control-label"></label>
                                            <div class="col-sm-10 col-md-10">
                                                <asp:Literal ID="ltlTabelaProdutos" runat="server"></asp:Literal>
                                                <div id="tabelaProdutos"></div>
                                            </div>
                                        </div>

                                    </div>

                                    <!-- Fim panel-body -->
                                </div>
                                <div class="hr-line-dashed"></div>

                            </asp:Panel>


                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Observação:</label>
                                <div class="col-sm-10 col-md-10">
                                    <input type="text" class="form-control" runat="server" id="txtObservacao" maxlength="50">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="col-sm-2 col-md-2 control-label">Status:</label>
                                    <div class="col-sm-10 col-md-10">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                            </asp:Panel>


                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label"></label>
                                <div class="col-md-4 col-sm-4 ">
                                    <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                </div>
                                <label class="col-sm-2 col-md-2 control-label" style="color: #FF0000; font-weight: bold; font-size: 14pt;">TOTAL CAIXA:</label>
                                <div class="col-md-2 col-sm-2 ">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtTotalCaixa" style="color: #FF0000; font-weight: bold; font-size: 14pt;">
                                </div>
                                <label class="col-sm-3 col-md-3 control-label"></label>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </div>


        <div class="modal inmodal" id="myModal4" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated fadeIn">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <i class="fa fa-clock-o modal-icon"></i>
                        <h4 class="modal-title">Excluir Registro</h4>
                        <small>.</small>
                    </div>
                    <div class="modal-body">
                        <p>Deseja realmente apagar este gegistro?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Não</button>
                        <%--<asp:Button ID="btnSimDelReg" runat="server" Text="Sim"  class="btn btn-primary" OnClick="btnSimDelReg_Click"/>--%>
                        <button type="button" class="btn btn-primary" onclick="excluirIdSrvCx()">Sim</button>
                    </div>
                </div>
            </div>
        </div>



    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">


        function selecaoDdlServico() {
            // debugger;
            var c = document.getElementById("Conteudo_ddlServico");//.selectedIndex;
            var idServico = c.options[c.selectedIndex].value;

            window.PageMethods.PesquisaValorServico(idServico, onSucess, onError);

            function onSucess(result) {
                document.getElementById("Conteudo_txtValorServico").value = result;
                //document.getElementById("ddlContatoEscolaDiv").innerHTML = result;
            }

            function onError(result) {
                alert('selecaoDdlServico-Erro: Contate o Administrador!!');
            }

        }

        function selecaoDdlProduto() {
            debugger;
            var c = document.getElementById("Conteudo_ddlProduto");//.selectedIndex;
            var idProduto = c.options[c.selectedIndex].value;

            window.PageMethods.PesquisaValorProduto(idProduto, onSucess, onError);

            function onSucess(result) {
                document.getElementById("Conteudo_txtValorProduto").value = result;
                //document.getElementById("ddlContatoEscolaDiv").innerHTML = result;
            }

            function onError(result) {
                alert('selecaoDdlProduto-Erro: Contate o Administrador!!');
            }

        }

        function addServicoTabela() {
            // debugger;
            var c = document.getElementById("Conteudo_ddlServico");
            var idServico = c.options[c.selectedIndex].value;

            if (idServico != '0') {

                var idFluxoCaixa = document.getElementById("Conteudo_hdfIdServicosFluxoCaixa").value;
                var valor = document.getElementById("Conteudo_txtValorServico").value;
                if (valor != '') {
                    valor = valor.replace('R$', '');
                } else {
                    alert('Campo Valor do serviço deve ser preenchido!!');
                    document.getElementById("Conteudo_txtValorServico").focus();

                    return;
                }

                window.PageMethods.AddServicoTabela(idFluxoCaixa, idServico, valor, onSucess, onError);
                function onSucess(result) {

                    atualizaTotalCaixa();

                    document.getElementById("tabelaServicos").innerHTML = result;

                    document.getElementById("Conteudo_ddlServico").selectedIndex = 0;
                    //c.options[c.selectedIndex].value = "Selecione...";
                    document.getElementById("Conteudo_txtValorServico").value = "";
                    // debugger;
                    //Limpa a tabela q já foi preenchida no literal ltlTabelaServicos
                    document.getElementById("ltlTabelaServicos").innerHTML = "";

                }

                function onError(result) {
                    alert('addServicoTabela-Erro: Contate o Administrador!!');
                }
            }

        }

        function addProdutoTabela() {
            debugger;
            var c = document.getElementById("Conteudo_ddlProduto");
            var idProduto = c.options[c.selectedIndex].value;

            if (idProduto != '0') {

                var idFluxoCaixa = document.getElementById("Conteudo_hdfIdProdutosFluxoCaixa").value;
                var valor = document.getElementById("Conteudo_txtValorProduto").value;
                if (valor != '') {
                    valor = valor.replace('R$', '');
                } else {
                    alert('Campo Valor do produto deve ser preenchido!!');
                    document.getElementById("Conteudo_txtValorProduto").focus();
                    return;
                }

                var qtde = document.getElementById("Conteudo_txtQtdeProduto").value;
                if (qtde == '') {
                    alert('Campo Valor do produto deve ser preenchido!!');
                    document.getElementById("Conteudo_txtQtdeProduto").focus();
                    return;
                }

                window.PageMethods.AddProdutoTabela(idFluxoCaixa, idProduto, valor, qtde, onSucess, onError);
                function onSucess(result) {

                    atualizaTotalCaixa();

                    document.getElementById("tabelaProdutos").innerHTML = result;

                    document.getElementById("Conteudo_ddlProduto").selectedIndex = 0;
                    //c.options[c.selectedIndex].value = "Selecione...";
                    document.getElementById("Conteudo_txtValorProduto").value = "";
                    document.getElementById("Conteudo_txtQtdeProduto").value = "";
                    // debugger;
                    //Limpa a tabela q já foi preenchida no literal ltlTabelaServicos
                    document.getElementById("ltlTabelaProdutos").innerHTML = "";

                }

                function onError(result) {
                    alert('addProdutoTabela-Erro: Contate o Administrador!!');
                }
            }

        }

        function capturarIdSrvExcluir(idSrvCxExcluir) {
            //debugger;
            window.PageMethods.CapturarIdSrvExcluir(idSrvCxExcluir, onSucess, onError);
            function onSucess(result) {
                // document.getElementById("tabelaServicos").innerHTML = result;
                excluirIdSrvCx();
            }

            function onError(result) {
                alert('capturarIdSrvExcluir-Erro: Contate o Administrador!!');
            }

        }

        function capturarIdProdExcluir(idProdCxExcluir) {
            //debugger;
            window.PageMethods.CapturarIdProdExcluir(idProdCxExcluir, onSucess, onError);
            function onSucess(result) {
                // document.getElementById("tabelaServicos").innerHTML = result;
                excluirIdProdCx();
            }

            function onError(result) {
                alert('capturarIdProdExcluir-Erro: Contate o Administrador!!');
            }

        }

        function excluirIdSrvCx() {
            //debugger;

            var idFluxoCaixa = document.getElementById("Conteudo_hdfIdServicosFluxoCaixa").value;

            window.PageMethods.ExcluirServicoCaixa(idFluxoCaixa, onSucess, onError);

            function onSucess(result) {

                atualizaTotalCaixa();

                document.getElementById("tabelaServicos").innerHTML = result;
                debugger;
                //Limpa a tabela q já foi preenchida no literal ltlTabelaServicos
                document.getElementById("ltlTabelaServicos").innerHTML = "";

                //$("#myModal4").modal('hide');
            }

            function onError(result) {
                alert('excluirIdSrvCx-Erro: Contate o Administrador!!');
            }

        }

        function excluirIdProdCx() {
            debugger;

            var idFluxoCaixa = document.getElementById("Conteudo_hdfIdProdutosFluxoCaixa").value;

            window.PageMethods.ExcluirProdutoCaixa(idFluxoCaixa, onSucess, onError);

            function onSucess(result) {

                atualizaTotalCaixa();

                document.getElementById("tabelaProdutos").innerHTML = result;
                debugger;
                //Limpa a tabela q já foi preenchida no literal ltlTabelaServicos
                document.getElementById("ltlTabelaProdutos").innerHTML = "";

                document.getElementById("Conteudo_txtValorProduto").value = "";
                document.getElementById("Conteudo_txtQtdeProduto").value = "1";
            }

            function onError(result) {
                alert('excluirIdProdCx-Erro: Contate o Administrador!!');
            }

        }


        function atualizaTotalCaixa() {
            debugger;

            var idFluxoCaixa = document.getElementById("Conteudo_hdfIdProdutosFluxoCaixa").value;

            window.PageMethods.AtualizaTotalCaixa(idFluxoCaixa, onSucess, onError);

            function onSucess(result) {
                document.getElementById("Conteudo_txtTotalCaixa").value = result;                
            }

            function onError(result) {
                alert('excluirIdProdCx-Erro: Contate o Administrador!!');
            }

        }

    </script>

</asp:Content>

