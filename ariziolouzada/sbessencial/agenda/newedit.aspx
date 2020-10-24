<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.sbessencial.agenda.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/chosen/chosen.css" rel="stylesheet" />

    <link href="../css/animate.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />


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
                                <asp:Label ID="lblCabecalho" runat="server" Text="Adicionar Item na Agenda"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Cliente:</label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlCliente" runat="server" data-placeholder="Selecione um cliente..." class="chosen-select" Style="width: 300px;" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalAddCliente">
                                        <i class="fa fa-plus-square"></i>&nbsp;Cliente
                                    </button>
                                </div>

                                <label class="col-sm-2 col-md-2 control-label">Profissional:</label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="chosen-select" Style="width: 220px;" >                                      
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Data:</label>
                                <div class="col-sm-3 col-md-3">
                                    <input type="date" class="form-control" runat="server" id="txtData">
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Hora:</label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlHora" runat="server" class="chosen-select" Style="width: 150px;" TabIndex="2">
                                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="07">7</asp:ListItem>
                                        <asp:ListItem Value="08">8</asp:ListItem>
                                        <asp:ListItem Value="09">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                        <asp:ListItem Value="17">17</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                        <asp:ListItem Value="19">19</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Minuto:</label>
                                <div class="col-sm-2 col-md-2">
                                    <asp:DropDownList ID="ddlMinuto" runat="server" class="chosen-select" Style="width: 150px;" TabIndex="2">
                                        <asp:ListItem>Selecione...</asp:ListItem>
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Observação:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtObservacao" maxlength="155">
                                </div>

                                <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                    <label class="col-sm-2 col-md-2 control-label">Status:</label>
                                    <div class="col-sm-4 col-md-4">
                                        <%--AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"--%>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="chosen-select" Style="width: 200px;" TabIndex="2" onchange="selectDdlStatus()">
                                            <asp:ListItem Value="1">Agendado</asp:ListItem>
                                            <asp:ListItem Value="2">Confirmado</asp:ListItem>
                                            <asp:ListItem Value="3">Cancelado</asp:ListItem>
                                            <asp:ListItem Value="4">Executado</asp:ListItem>
                                            <asp:ListItem Value="5">Não Executado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>

                            </div>


                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 control-label"></label>
                                <asp:Panel ID="pnlFormaPgto" runat="server" Style="display: none;">
                                    <label class="col-sm-2 col-md-2 control-label">Forma Pgto:</label>
                                    <div class="col-sm-4 col-md-4">
                                        <%--class="chosen-select" Style="width: 200px;" TabIndex="3"--%>
                                        <asp:DropDownList ID="ddlFormaPgto" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Dinheiro</asp:ListItem>
                                            <asp:ListItem Value="2">Cartão Débito</asp:ListItem>
                                            <asp:ListItem Value="3">Cartão Crédito 30 dias</asp:ListItem>
                                            <asp:ListItem Value="4">Cartão Crédito Parcelado</asp:ListItem>
                                            <asp:ListItem Value="5">Cheque</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>

                            </div>

                            <asp:Panel ID="pnlServico" runat="server">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Serviço(s) a serem realizados 
                                    </div>
                                    <div class="panel-body">

                                        <div class="form-group">
                                            <label class="col-sm-2 col-md-2 control-label">Serviço:</label>
                                            <div class="col-sm-5 col-md-5">
                                                <%-- AutoPostBack="True" OnSelectedIndexChanged="ddlServico_SelectedIndexChanged--%>
                                                <asp:DropDownList ID="ddlServico" runat="server" class="chosen-select" Style="width: 350px;" TabIndex="2" onchange="selecaoDdlServico()">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfIdRegistroAgenda" runat="server" />
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

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label"></label>
                                <div class="col-md-4 col-sm-4 ">
                                    <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" OnClientClick="return verificaBtnSalvar()" />
                                </div>
                                <%--<label class="col-sm-2 col-md-2 control-label" style="color: #FF0000; font-weight: bold; font-size: 14pt;">TOTAL:</label>
                                <div class="col-md-2 col-sm-2 ">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtTotalCaixa" style="color: #FF0000; font-weight: bold; font-size: 14pt;">
                                </div>--%>
                                <label class="col-sm-7 col-md-7 control-label"></label>
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


        <div class="modal inmodal" id="modalAddCliente" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated fadeIn">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <i class="fa fa-clock-o modal-icon"></i>
                        <h4 class="modal-title">Cadastrar novo cliente</h4>
                        <small>Você poderá cadastrar aqui um novo cliente.</small>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label class="col-xs-4 col-sm-3 col-md-2 control-label">Nome:</label>
                            <div class="col-xs-8 col-sm-9 col-md-10">
                                <input type="text" class="form-control" runat="server" placeholder="Nome" id="txtNome">
                                <asp:HiddenField ID="hdfIdNovoCliente" runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" onclick="cadastrarNovoCliente()">Salvar</button>
                        <%--<asp:Button ID="btnSalvarNovoCliente" runat="server" Text="Salvar"  class="btn btn-primary" OnClick="btnSalvarNovoCliente_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <!-- Mainly scripts 
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>-->

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <!-- Chosen -->
    <script src="../js/plugins/chosen/chosen.jquery.js"></script>

    <script type="text/javascript">

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


        function verificaBtnSalvar() {
            debugger;
            if (document.getElementById("Conteudo_pnlFormaPgto").style.display == 'block') {

                var c = document.getElementById("Conteudo_ddlFormaPgto");
                var id = c.options[c.selectedIndex].value;

                if (id == "0") {
                    alert('Erro: Selecione a forma de pagamento!!');
                    document.getElementById("Conteudo_ddlFormaPgto").focus();
                    return false;
                }
            }

            return true;
        }

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

        function selectDdlStatus() {
            debugger;
            var c = document.getElementById("Conteudo_ddlStatus");//.selectedIndex;
            var idStatus = c.options[c.selectedIndex].value;

            if (idStatus == "4") {
                document.getElementById("Conteudo_pnlFormaPgto").style.display = 'block';
            } else {
                document.getElementById("Conteudo_pnlFormaPgto").style.display = 'none';
            }

        }

        function addServicoTabela() {
            debugger;
            var c = document.getElementById("Conteudo_ddlServico");
            var idServico = c.options[c.selectedIndex].value;

            if (idServico != '0') {

                var idFluxoCaixa = document.getElementById("Conteudo_hdfIdRegistroAgenda").value;
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

                    //atualizaTotalCaixa();

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

        function excluirIdSrvCx() {
            //debugger;

            var idFluxoCaixa = document.getElementById("Conteudo_hdfIdRegistroAgenda").value;

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

        function cadastrarNovoCliente() {

            var nome = document.getElementById("Conteudo_txtNome").value;
            if (nome != "") {

                window.PageMethods.CadastrarNovoCliente(nome, onSucess, onError);

                function onSucess(result) {
                    debugger;
                    if (result == "-1") {
                        alert('Erro: Nome do novo cliente já cadastrado!!');
                    }
                    else {
                        $("#modalAddCliente").modal('hide');
                        //$('#Conteudo_ddlCliente').append('<option value="' + result + '">' + nome.toUpperCase() + '</option>');

                        $('#Conteudo_ddlCliente').append(new Option(nome.toUpperCase(), result));
                        document.getElementById("Conteudo_ddlCliente").value = result;
                        $("#Conteudo_ddlCliente").val(result).change();
                        //$("#Conteudo_ddlCliente option[value='0']").remove();

                    }
                }

                function onError(result) {
                    alert('cadastrarNovoCliente-Erro: Contate o Administrador!!');
                }

            }
            else {
                alert('Erro: Digite o nome do novo cliente!!');
            }
        }

        //function atualizaTotalCaixa() {
        //    debugger;

        //    var idFluxoCaixa = document.getElementById("Conteudo_hdfIdProdutosFluxoCaixa").value;

        //    window.PageMethods.AtualizaTotalCaixa(idFluxoCaixa, onSucess, onError);

        //    function onSucess(result) {
        //        document.getElementById("Conteudo_txtTotalCaixa").value = result;
        //    }

        //    function onError(result) {
        //        alert('excluirIdProdCx-Erro: Contate o Administrador!!');
        //    }

        //}

    </script>

</asp:Content>


