<%@ Page Title="" Language="C#" MasterPageFile="~/ast/ast.Master" AutoEventWireup="true" CodeBehind="neweditservico.aspx.cs" Inherits="ariziolouzada.ast.epi.neweditservico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="utf-8">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet" />

    <%--<link href="../css/plugins/dataTables/datatables.min.css" rel="stylesheet">--%>

    <link href="../css/plugins/datapicker/datepicker3.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>
                                <asp:Label ID="lblCabecalho" runat="server" Text="Cadastrar Serviço"></asp:Label></h5>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group" id="data_1">
                                <label class="col-sm-2 control-label">Data:</label>
                                <div class="col-sm-4">
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <input type="text" class="form-control" id="txtData" runat="server">
                                    </div>
                                </div>

                                <label class="col-sm-2 control-label">Tipo de serviço:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTipo" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Serviço:</label>
                                <div class="col-sm-4">
                                    <%--<div id="selectServico">
                                        <select class="form-control" id="ddlServico" name="ddlServico"></select>
                                    </div>--%>
                                    <asp:DropDownList ID="ddlServico" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <label class="col-sm-2 control-label">Preço de custo (R$):</label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control" placeholder="" runat="server" disabled="" id="txtPrecoCusto">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-2 control-label">Qtde:</label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control" runat="server" id="txtQtd">
                                </div>

                                <label class="col-sm-2 control-label">Total (R$):</label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control" runat="server" disabled="" id="txtValorTotal">
                                </div>

                            </div>
                            <div class="hr-line-dashed"></div>



                            <div class="form-group">
                                <div class="col-sm-4 col-sm-offset-2">
                                    <asp:Button class="btn btn-white" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                </div>
                            </div>

                        </div>

                    </div>

                </div>

            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <!-- Mainly scripts -->
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <!-- Input Mask-->
    <script src="../js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Data picker -->
    <script src="../js/plugins/datapicker/bootstrap-datepicker.js"></script>


    <script type="text/javascript">

        //$(document).ready(function () {

        //    alert('Passei aki 1!!');
        //    // códigos jQuery a serem executados quando a página carregar

        //});

        //document.getElementById("Conteudo_txtQtd").disabled = true;

        $('#Conteudo_ddlTipo').change(function () {

            var tipoSelecionado = $(this).val();
            //if (tipoSelecionado != '0')
            carregaDllServico(tipoSelecionado);
            document.getElementById("Conteudo_txtPrecoCusto").value = '';
            document.getElementById("Conteudo_txtValorTotal").value = '';
            document.getElementById("Conteudo_txtQtd").value = '';
        });

        $('#Conteudo_ddlServico').change(function () {
            //alert('Passei aki 1!!');
            var srvSelecionado = $(this).val();
            //if (tipoSelecionado != '0')
            if (srvSelecionado != "0") {
                //document.getElementById("Conteudo_txtQtd").disabled = false;
                carregaPrecoCusto(srvSelecionado);

                document.getElementById("Conteudo_txtQtd").value = '';
                document.getElementById("Conteudo_txtValorTotal").value = '';
                //var qtde = $('#Conteudo_txtQtd').val();
                //if(qtde != ''){
                //    calculaPrecoTotal();
                //}

            }
            //else {
            //    document.getElementById("Conteudo_txtQtd").disabled = true;
            //}

        });

        function carregaDllServico(tipoSelecionado) {

            window.PageMethods.CarregaDllServico(tipoSelecionado, onSucess, onError);
            function onSucess(result) {
                document.getElementById("Conteudo_ddlServico").innerHTML = result;
            }
            function onError(result) {
                alert('Erro: Contate o Administrador!!');
            }
        }

        function carregaPrecoCusto(srvSelec) {
            window.PageMethods.CarregaPrecoCusto(srvSelec, onSucess, onError);
            function onSucess(result) {
                document.getElementById("Conteudo_txtPrecoCusto").value = result;
            }
            function onError(result) {
                alert('Erro: Contate o Administrador!!');
            }
        }


        function carregaPrecoTotal(qtde, pcoCusto) {

            window.PageMethods.CarregaPrecoTotal(qtde, pcoCusto, onSucess, onError);
            function onSucess(result) {
                document.getElementById("Conteudo_txtValorTotal").value = result;
            }
            function onError(result) {
                alert('Erro: Contate o Administrador!!');
            }
        }


        function calculaPrecoTotal() {
            var qtde = $('#Conteudo_txtQtd').val();
            var valorCusto = $('#Conteudo_txtPrecoCusto').val();
            carregaPrecoTotal(qtde, valorCusto);
        }


        $('#Conteudo_txtQtd').keyup(function () {
            // alert('passei aki 1!!');
            var qtde = $('#Conteudo_txtQtd').val();
            var valorCusto = $('#Conteudo_txtPrecoCusto').val();

            carregaPrecoTotal(qtde, valorCusto);
        });


        $('#data_1 .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy',
            //language: 'pt-BR',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior'
        });


    </script>

</asp:Content>
