<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vendedor.aspx.cs" Inherits="ariziolouzada.acaoentreamigos.vendedor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="Arizio Aguilar Oliveira Louzada" />

    <title>Ação Entre Amigos</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />

    <link href="css/plugins/select2/select2.min.css" rel="stylesheet" />
    <!-- sweetalert2 -->
    <link href="js/sweetalert/sweetalert.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style>
        .ddlNum {
        }
    </style>

</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        <div class=" animated fadeInDown">
            <div class="text-center">
                <h1><b>Ação Entre Amigos</b></h1>
                <%--<a href="login.aspx" ">Sair</a>--%>
                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Nome Cliente</label>
                    <div class="col-sm-10">
                        <input type="text" id="txtNome" class="form-control" placeholder="Nome do cliente" runat="server" required="" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Vendedor</label>
                    <div class="col-sm-10">
                        <%--style="width: 200px;"--%>
                        <select data-placeholder="Escolha o vendedor" class="chosen-select ddlNum form-control" id="ddlVendedor" runat="server">
                        </select>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Telefone(s)</label>
                    <div class="col-sm-10">
                        <input type="text" id="txtTelefone" class="form-control" placeholder="Telefone(s) do cliente" runat="server" required="" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Email</label>
                    <div class="col-sm-10">
                        <input type="text" id="txtEmail" class="form-control" placeholder="Email do cliente" runat="server" required="" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Número</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                            <input type="number" id="txtNumero" class="form-control" runat="server" required="" maxlength="5" />
                            <span class="input-group-btn">
                                <button type="button" class="btn btn-outline btn-success" onclick="acaoBtnAdicionarNumero();"><i class="fa fa-plus"></i>&nbsp;Adicionar</button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <asp:HiddenField ID="hdfIdVenda" runat="server" />
                    <label class="col-sm-2 control-label text-right">Número(s) Adicionado(s)</label>
                    <div class="col-sm-4" id="divNumAdicionados">
                    </div>
                    <label class="col-sm-6 control-label"></label>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-6 col-sm-offset-2">
                        <button type="button" class="btn btn-outline btn-warning" onclick="acaoBtnCancelar();"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                        <button type="button" class="btn btn-outline btn-primary" onclick="acaoBtnConfirmar();"><i class="fa fa-money"></i>&nbsp;Confirmar Venda</button>
                        <a href="login.aspx" class="btn btn-outline btn-danger"><i class="fa fa-sign-out"></i>&nbsp;Sair</a>
                    </div>
                </div>
            </div>
        </div>

    </form>


    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <!-- Select2 -->
    <script src="js/plugins/select2/select2.full.min.js"></script>
    <script src="js/sweetalert/sweetalert.min.js"></script>

    <script>

        $(document).ready(function () {
            $(".ddlNum").select2();
        });
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, não encontrado!' },
            '.chosen-select-width': { width: "95%" }
        }
        //for (var selector in config) {
        //    $(selector).chosen(config[selector]);
        //}

        function msn(tipo, texto) {
            //debugger;
            if (tipo == 0) { //men de Erro
                swal({
                    title: "Erro!",
                    text: texto,
                    type: "error"
                });
            }
            if (tipo == 1) { //men de Sucesso
                swal({
                    title: "Successo!",
                    text: texto,
                    type: "success"
                });
            }
        }

        function acaoBtnAdicionarNumero() {
            debugger;
            var numero = document.getElementById("<%=txtNumero.ClientID%>").value;

            if (numero != '') {

                if (verificaNumero(numero)) {

                    //debugger;
                    //adicionaNumero(numero);
                }

            } else {
                //Mensagem de erro
                msn(0, 'Digite o número a ser vendido!!');
                document.getElementById("<%=txtNumero.ClientID%>").focus();
            }

        }

        function verificaNumero(numero) {
            debugger;
            var idVenda = document.getElementById("<%=hdfIdVenda.ClientID%>").value;

            window.PageMethods.VerificaNumero(numero, idVenda, onSucess, onError);
            function onSucess(result) {
                debugger;

                if (result === 'NumOk') {
                    //return true;
                    //debugger;
                    adicionaNumero(numero);
                }
                else {
                    msn(0, result);
                    //return false;
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo verificaNumero - Contate o Administrador!!');
            }
        }

        function adicionaNumero(numero) {
            //debugger;
            var idVenda = document.getElementById("<%=hdfIdVenda.ClientID%>").value;

            window.PageMethods.AdicionaNumero(numero, idVenda, onSucess, onError);
            function onSucess(result) {
                if (result == 'NumAddSuccess') {
                    document.getElementById("<%=txtNumero.ClientID%>").value = '';
                    carregaTabelaNumAdd(idVenda);
                } else {
                    msn(0, result);
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo adicionaNumero - Contate o Administrador!!');
            }

        }

        function acaoBtnApagarNumero(numero, idVenda) {
            //debugger;
            window.PageMethods.ApagarNumero(numero, idVenda, onSucess, onError);
            function onSucess(result) {

                debugger;

                if (result == 'NumDelSuccess') {
                    carregaTabelaNumAdd(idVenda);
                } else {
                    msn(0, result);
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo acaoBtnApagarNumero - Contate o Administrador!!');
            }
        }

        function carregaTabelaNumAdd(idVenda) {

            window.PageMethods.CarregaTabelaNumAdd(idVenda, onSucess, onError);
            function onSucess(result) {
                document.getElementById("divNumAdicionados").innerHTML = result;
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo carregaTabelaNumAdd - Contate o Administrador!!');
            }
        }

        function acaoBtnCancelar() {
            //debugger;
            var idVenda = document.getElementById("<%=hdfIdVenda.ClientID%>").value;
            window.PageMethods.AcaoBtnCancelar(idVenda, onSucess, onError);
            function onSucess(result) {

                debugger;

                if (result == 'CancelSuccess') {

                    window.location = 'vendedor.aspx';

                } else {
                    msn(0, result);
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo acaoBtnApagarNumero - Contate o Administrador!!');
            }
        }

        function acaoBtnConfirmar() {

            if (verificaBtnConfirmar()) {

                salvarVenda();
                //msn(1, 'venda OK!!')

            }

        }

        function verificaBtnConfirmar() {
            //debugger;
            if (document.getElementById("<%=txtNome.ClientID%>").value == '') {
                msn(0, 'Campo NOME de preenchimento Obrigatório!!');
                document.getElementById("<%=txtNome.ClientID%>").focus();
                return false;
            }

            var select = document.getElementById("<%=ddlVendedor.ClientID%>");
            if (select.options[select.selectedIndex].value == 0) {
                msn(0, 'Campo VENDEDOR de seleção Obrigatório!!');
                document.getElementById("<%=txtTelefone.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtTelefone.ClientID%>").value == '') {
                msn(0, 'Campo TELEFONE de preenchimento Obrigatório!!');
                document.getElementById("<%=txtTelefone.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == '') {
                msn(0, 'Campo EMAIL de preenchimento Obrigatório!!');
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function salvarVenda() {

            debugger;
            var idVenda = document.getElementById("<%=hdfIdVenda.ClientID%>").value;
            var nome = document.getElementById("<%=txtNome.ClientID%>").value;
            var select = document.getElementById("<%=ddlVendedor.ClientID%>");
            var idVendedor = select.options[select.selectedIndex].value;
            var tel = document.getElementById("<%=txtTelefone.ClientID%>").value;
            var email = document.getElementById("<%=txtEmail.ClientID%>").value;

            window.PageMethods.SalvarVenda(idVenda, nome, idVendedor, tel, email, onSucess, onError);

            function onSucess(result) {

                debugger;

                if (result == 'VendaSuccess') {

                    swal({
                        title: "PARABÉNS",
                        text: "Venda realizada com sucesso!",
                        type: "success"
                    },
                        function () {
                            window.location = 'vendedor.aspx';
                        });


                } else {
                    msn(0, result);
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo acaoBtnApagarNumero - Contate o Administrador!!');
            }

        }

    </script>

</body>
</html>

