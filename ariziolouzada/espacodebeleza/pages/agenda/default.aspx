<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.agenda._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />

    <style>
        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #1f5688; /* Blue */
            border-radius: 50%;
            width: 60px;
            height: 60px;
            animation: spin 2s linear infinite;
            margin: auto;
            padding: 10px;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .centro {
            text-align: center;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <asp:Literal ID="ltlCabecalho" runat="server"></asp:Literal>
                    <%--<h2>Agenda</h2>--%>
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
                        <%--<div class="ibox-title">
                            <h5>Lista de Clientes</h5>
                        </div>--%>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Data:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10" />
                                </div>
                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Tipo Profis.:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlProfissao" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <label class="col-xs-2 col-sm-2 col-md-2 control-label">Profissional:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="text" class="form-control" runat="server" id="txtPesqNomeProf" maxlength="10">
                                </div>
                                <div class="col-xs-1 col-sm-1 col-md-1">
                                    <%--<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-success btn-rounded" OnClick="btnFiltrar_Click" />  &nbsp;Carregar &nbsp;Criar--%>
                                    <a class="btn btn-success btn-rounded btn-outline" href="#" onclick="carregarAgenda();" title="Carregar Agenda."><i class="fa fa-play fa-2x"></i></a>

                                </div>
                                <div class="col-xs-1 col-sm-1 col-md-1">
                                    <a class="btn btn-primary btn-rounded btn-outline" href="#" data-toggle="modal" data-target="#myModal6" onclick="criarAgenda();" title="Criar Agenda."><i class="fa fa-plus fa-2x"></i></a>
                                </div>

                            </div>
                            <br />
                            <div class="hr-line-dashed"></div>

                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                <div id="tabelaAgenda"></div>
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


        <div class="modal inmodal" id="myModal6" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated flipInY">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>--%>
                        <h4 class="modal-title">Agenda</h4>
                    </div>
                    <div class="modal-body">
                        <div id="dadosAgenda"></div>
                        <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                        <asp:HiddenField ID="hdfIdHoraInicial" runat="server" />
                        <asp:HiddenField ID="hdfIdServico" runat="server" />

                        <%--<div class="form-group">
                            <label class="control-label">Profissional </label>
                            <input type="text" class="form-control" id="txtProfAgenda" disabled="" value="" />
                        </div>
                        <div class="form-group">
                            <label class="control-label">Horário Inicial </label>
                            <input type="text" class="form-control" id="txtHoraInicialAgenda" disabled="" value="" />
                        </div>
                        <div class="form-group">
                            <label class="control-label">Horário Final </label>
                            <select class="form-control" id="ddlHoraFinal">
                                <option value="0">option 1</option>
                                <option value="0">option 2</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Serviço </label>
                            <select class="form-control" id="ddlServico">
                                <option value="0">option 1</option>
                                <option value="0">option 2</option>
                            </select>
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <a href="../cliente/newedit.aspx?id=X5A1oqTnjBE=" class="btn btn-warning"><i class="fa fa-plus-square"></i>&nbsp;Cliente</a>
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <button type="button" class="btn btn-primary" onclick="salvarDadosAgenda();">Salvar</button>
                        <%--<asp:Button ID="btnSalvarComissao" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvarComissao_Click" />--%>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal fade" id="myModal7" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>--%>
                        <h4 class="modal-title">Agenda</h4>
                    </div>
                    <div class="modal-body">
                        <div id="dadosAgenda2"></div>
                        <br />
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-outline" data-dismiss="modal" title="Fechar"><i class="fa fa-times fa-2x"></i></button>
                        <%--<div id="pnlBtnsModal" style="display:block;">   demo3   --%>
                        <button id="btnModalApagar" class="btn btn-outline btn-warning " onclick="apagarDadosAgenda();" title="Apagar" ><i class="fa fa-eraser fa-2x"></i></button>
                        <button id="btnModalFinalizar" type="button" class="btn btn-outline btn-success" onclick="finalizarAgenda();" title="Confirmar"><i class="fa fa-check fa-2x"></i></button>
                        <%--</div>--%>
                        <asp:HiddenField ID="hdfIdAgendaApagar" runat="server" />
                        <%--<button type="button" class="btn btn-danger" onclick="salvarDadosAgenda();">Cancelar</button>
                        <asp:Button ID="btnSalvarComissao" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvarComissao_Click" />--%>
                    </div>
                </div>
            </div>
        </div>


        <div id="loader" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<h4 class="modal-title"> </h4>--%>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">

                        <div class="ibox ">
                            <div class="ibox-title">
                                <h2>Aguarde ...</h2>
                            </div>
                            <div class="ibox-content">
                                <div class="spiner-example">
                                    <div class="sk-spinner sk-spinner-wave">
                                        <div class="sk-rect1"></div>
                                        <div class="sk-rect2"></div>
                                        <div class="sk-rect3"></div>
                                        <div class="sk-rect4"></div>
                                        <div class="sk-rect5"></div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--<div class="loader centered"></div>--%>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../../js/plugins/sweetalert/sweetalert.min.js"></script>

    <script>


        $(document).ready(function () {


            $('.demo3').click(function () {
                swal({
                    title: "Apagar Agenda!!",
                    text: "Deseja realmente cancelar o registro desta agenda?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "SIM",
                    closeOnConfirm: false
                }, function () {

                    apagarDadosAgenda();
                    //swal("Apagado!", "Registro apagado com sucesso.", "success");


                });
            });

        });

        function finalizarAgenda() {

            debugger;
            var idAgendaApagar = document.getElementById("<%=hdfIdAgendaApagar.ClientID%>").value;

            window.PageMethods.FinalizarAgenda(idAgendaApagar, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (result != '') {
                    window.location.href = '../comanda/newedit.aspx?id=g09Z8YbByW8=&idag=' + result;
                    //<a href="../financeiro/pgtocomanda.aspx"></a>
                }
                else {
                    document.getElementById("dadosAgenda2").innerHTML = result;
                }
            }
            function onError(result) {
                alert('Erro finalizarAgenda: Contate o Administrador!!');
            }

        }

        function carregarAgenda() {
            debugger;
            $('#loader').modal('show');
            var dataAgenda = document.getElementById("Conteudo_txtData").value;

            var select = document.getElementById("Conteudo_ddlProfissao");
            var idProfissao = select.options[select.selectedIndex].value;

            var txtPesqNomeProf = document.getElementById("Conteudo_txtPesqNomeProf").value;


            window.PageMethods.CarregarAgenda(dataAgenda, idProfissao, txtPesqNomeProf, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("tabelaAgenda").innerHTML = result;
                $('#loader').modal('hide');
            }
            function onError(result) {
                alert('Erro criarAgenda: Contate o Administrador!!');
            }
        }

        function apagarDadosAgenda() {

            debugger;
            var idAgendaApagar = document.getElementById("<%=hdfIdAgendaApagar.ClientID%>").value;

            window.PageMethods.ApagarDadosAgenda(idAgendaApagar, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (result == '') {
                    var dataAgenda = document.getElementById("Conteudo_txtData").value;
                    swal("Apagado!", "Registro apagado com sucesso.", "success");
                    $('#myModal6').modal('hide');
                    carregarAgenda();
                    //window.location.href = 'default.aspx?data=' + dataAgenda;

                }
                else {
                    document.getElementById("dadosAgenda2").innerHTML = result;
                }
            }
            function onError(result) {
                alert('Erro apagarDadosAgenda: Contate o Administrador!!');
            }
        }

        function verificaBtnSalvarDadosAgenda() {
            debugger;

            //if (document.getElementById("txtdataAgenda").value == '') {
            //    alert('Digite/Selecione a Data da agenda!!');
            //    document.getElementById("txtdataAgenda").focus();
            //    return false;
            //}

            var select = document.getElementById("ddlCliente");
            var idCliente = select.options[select.selectedIndex].value;
            if (idCliente == 0) {
                alert('Selecione o Cliente!!');
                return false;
            }

            select = document.getElementById("ddlProfissional");
            var idProfissional = select.options[select.selectedIndex].value;
            if (idProfissional == 0) {
                alert('Selecione o Profisional!!');
                return false;
            }

            select = document.getElementById("sltServico");
            var idServico = select.options[select.selectedIndex].value;
            if (idServico == 0) {
                alert('Selecione o serviço!!');
                return false;
            }

            select = document.getElementById("ddlHoraInicial");
            var idHora = select.options[select.selectedIndex].value;
            if (idHora == 0) {
                alert('Selecione a Hora Inicial!!');
                return false;
            }

            select = document.getElementById("sltHoraFinal");
            idHora = select.options[select.selectedIndex].value;
            if (idHora == 0) {
                alert('Selecione a Hora Final!!');
                return false;
            }

            return true;
        }

        function capturaDadosAgenda(idAgenda, enableBtns) {
            debugger;

            if (enableBtns == '1') {
                document.getElementById("btnModalApagar").style.visibility = "visible";
                document.getElementById("btnModalFinalizar").style.visibility = "visible";
            } else {
                document.getElementById("btnModalApagar").style.visibility = "hidden";
                document.getElementById("btnModalFinalizar").style.visibility = "hidden";
            }

            window.PageMethods.CapturaDadosAgenda(idAgenda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("<%=hdfIdAgendaApagar.ClientID%>").value = idAgenda;
                document.getElementById("dadosAgenda2").innerHTML = result;
            }
            function onError(result) {
                alert('Erro capturaDadosAgenda: Contate o Administrador!!');
            }
        }

        function criarAgenda() {
            //debugger;
            var dataAgenda = document.getElementById("Conteudo_txtData").value;
            window.PageMethods.CriarAgenda(dataAgenda, onSucess, onError);
            function onSucess(result) {
                //debugger;
                //document.getElementById("<%=hdfIdAgendaApagar.ClientID%>").value = idAgenda;
                document.getElementById("dadosAgenda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro criarAgenda: Contate o Administrador!!');
            }
        }

        function carregaDdlServico() {
            //debugger;
            var select = document.getElementById("ddlProfissional");
            var idProfissional = select.options[select.selectedIndex].value;

            window.PageMethods.CarregaDdlServico(idProfissional, onSucess, onError);
            function onSucess(result) {
                //debugger;
                document.getElementById("ddlServico").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaDdlServico: Contate o Administrador!!');
            }
        }

        function carregaDdlHorarioFinal() {
            var select = document.getElementById("ddlHoraInicial");
            var idHoraInicial = select.options[select.selectedIndex].value;

            window.PageMethods.CarregaDdlHorarioFinal(idHoraInicial, onSucess, onError);
            function onSucess(result) {
                //debugger;
                document.getElementById("ddlHoraFinal").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaDdlHorarioFinal: Contate o Administrador!!');
            }
        }

        function capturaHoraInicialAgenda(idHoraInicioAgenda, idProf) {
            debugger;

            document.getElementById("<%=hdfIdHoraInicial.ClientID%>").value = idHoraInicioAgenda;
            document.getElementById("<%=hdfIdProfissional.ClientID%>").value = idProf;

            window.PageMethods.CapturaHoraInicialAgenda(idHoraInicioAgenda, idProf, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("dadosAgenda").innerHTML = result;
            }
            function onError(result) {
                alert('Erro capturaHoraInicialAgenda: Contate o Administrador!!');
            }

        }

        function salvarDadosAgenda() {
            debugger;
            if (verificaBtnSalvarDadosAgenda()) {
                debugger;
                $('#loader').modal('show');

                var dataAgenda = document.getElementById("Conteudo_txtData").value;

                var select = document.getElementById("ddlHoraInicial");
                var idHoraInicial = select.options[select.selectedIndex].value;

                select = document.getElementById("sltHoraFinal");
                var idHoraFinal = select.options[select.selectedIndex].value;

                select = document.getElementById("ddlProfissional");
                var idProf = select.options[select.selectedIndex].value;

                select = document.getElementById("sltServico");
                var idServico = select.options[select.selectedIndex].value;

                select = document.getElementById("ddlCliente");
                var idCliente = select.options[select.selectedIndex].value;

                window.PageMethods.SalvarDadosAgenda(dataAgenda, idHoraInicial, idHoraFinal, idProf, idServico, idCliente, onSucess, onError);
                function onSucess(result) {
                    debugger;
                    if (result == '') {
                        $('#loader').modal('hide');
                        carregarAgenda();
                        $('#myModal6').modal('hide');
                        //window.location.href = 'default.aspx?data=' + dataAgenda;
                    }
                    else {
                        document.getElementById("dadosAgenda").innerHTML = result;
                    }

                }
                function onError(result) {
                    alert('Erro salvarDadosAgenda: Contate o Administrador!!');
                }
            }
        }


    </script>

</asp:Content>
