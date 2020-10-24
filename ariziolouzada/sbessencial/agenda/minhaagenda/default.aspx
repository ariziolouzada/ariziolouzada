<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.sbessencial.agenda.minhaagenda._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/plugins/chosen/chosen.css" rel="stylesheet" />
    <link href="../../css/animate.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">

    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-12">
                <h2>Agenda Web Salão Essencial</h2>
            </div>
        </div>

        <div class="wrapper wrapper-content">
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox-content">
                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">DIA:</label>
                                <div class="col-sm-3 col-md-3">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">PROFISSIONAL:</label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="chosen-select" Style="width: 220px;">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Button class="btn btn-primary" ID="btnCarregar" runat="server" Text="Carregar" OnClick="btnCarregar_Click" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-12 col-sm-12 ">
                                    <asp:Literal ID="ltlTabelaHorarios" runat="server"></asp:Literal>
                                    <%--<asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    --%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal inmodal" id="myModalConfirmar" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated bounceInRight">
                    <div class="modal-header">
                        <%-- <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <i class="fa fa-laptop modal-icon"></i>
                        <h4 class="modal-title">Confirmar Horário Agendado</h4>
                        <small class="font-bold">
                            <asp:Label ID="lblHorarioConformar" runat="server" Text="Horário Escolhido" Style="font-weight: 700; color: #FF0000"></asp:Label>
                        </small>
                    </div>
                    <div class="modal-body">
                        <p>
                            <strong>Atenção</strong>
                            Você confirma o agendamento para o cliente??
                        </p>

                        <asp:HiddenField ID="hdfIdAgdTemp" runat="server" />

                        <div class="form-group">
                            <label class="col-sm-2 col-md-2 control-label">Cliente:</label>
                            <div class="col-sm-10 col-md-10">
                                <asp:DropDownList ID="ddlCliente" runat="server" class="form-control ">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Não</button>
                        <button type="button" id="btnConfirmarAgendar" class="btn btn-primary" onclick="acaoBtnConfirmarAgenda()">Sim</button>
                        <%--<asp:Button ID="btnAgendar" runat="server" Text="Agendar" CssClass="btn btn-primary" OnClick="btnAgendar_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal" id="myModalExcluir" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated bounceInRight">
                    <div class="modal-header">
                        <%-- <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <i class="fa fa-laptop modal-icon"></i>
                        <h4 class="modal-title">Excluir Horário Agendado</h4>
                        <small class="font-bold">
                            <asp:Label ID="lblHorarioExcluir" runat="server" Text="Horário Escolhido" Style="font-weight: 700; color: #FF0000"></asp:Label>
                        </small>
                    </div>
                    <div class="modal-body">
                        <p>
                            <strong>Atenção</strong>
                            Você confirma a exclusão do agendamento??
                        </p>
                        <asp:HiddenField ID="hdfHoraSelecionada" runat="server" />

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Não</button>
                        <button type="button" id="btnExcluirAgendar" class="btn btn-primary" onclick="acaoBtnExcluirAgenda()">Sim</button>
                        <%--<asp:Button ID="btnAgendar" runat="server" Text="Agendar" CssClass="btn btn-primary" OnClick="btnAgendar_Click"/>--%>
                    </div>
                </div>
            </div>
        </div>


    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <!-- Mainly scripts -->
    <script src="../../js/jquery-2.1.1.js"></script>
    <script src="../../js/bootstrap.min.js"></script>
    <script src="../../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../../js/plugins/metisMenu/jquery.metisMenu.js"></script>


    <!-- Custom and plugin javascript -->
    <script src="../../js/inspinia.js"></script>
    <script src="../../js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="../../js/plugins/chosen/chosen.jquery.js"></script>

    <script src="../../js/mascaras.js"></script>

    <script type="text/javascript">

        //$("#Conteudo_ddlProfissional").chosen({
        //    disable_search_threshold: 10,
        //    allow_single_deselect: true,
        //    no_results_text: "Nenhum Profissional Encontrado!",
        //    placeholder_text_multiple: "Selecione um Profissional...",
        //    width: "95%"
        //});


        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Nenhum Profissional Encontrado!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }


        function capturaHorarioAgenda(hora) {
            debugger;
            //document.getElementById("Conteudo_hdfHoraSelecionada").value = hora;
            var data = document.getElementById("Conteudo_txtData").value;

            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;
            var url = "agendar.aspx?dt=" + data + "&hr=" + hora + "&idp=" + idProfissional;
            window.location.href = url;

            //window.PageMethods.CapturaHorarioAgenda (data, hora, idProfissional, onSucess, onError);

            //function onSucess(result) {
            //    document.getElementById("ltlTabelaHorarios").innerHTML = result;
            //}

            //function onError(result) {
            //    alert('acaoBtnAgendar-Erro: Contate o Administrador!!');
            //}

            //document.getElementById("Conteudo_lblHorarioEscolhido").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";
            //document.getElementById("Conteudo_lblHorarioExcluir").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";

            //document.getElementById("Conteudo_txtNome").value = "";
            //document.getElementById("Conteudo_txtTelefone").value = "";
        }

        function capturaHorarioAgendaExcluir(hora) {
            debugger;

            document.getElementById("Conteudo_hdfHoraSelecionada").value = hora;
            document.getElementById("Conteudo_lblHorarioExcluir").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";

        }

        function acaoBtnAgendar() {
            debugger;
            var hora = document.getElementById("Conteudo_hdfHoraSelecionada").value;
            var data = document.getElementById("Conteudo_txtData").value;
            var nome = document.getElementById("Conteudo_txtNome").value;
            var tel = document.getElementById("Conteudo_txtTelefone").value;

            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;

            window.PageMethods.AcaoBtnAgendar(data, hora, idProfissional, nome, tel, onSucess, onError);

            function onSucess(result) {
                document.getElementById("ltlTabelaHorarios").innerHTML = result;

                $('#myModal').modal('hide');
            }

            function onError(result) {
                alert('acaoBtnAgendar-Erro: Contate o Administrador!!');
            }

        }

        function acaoBtnExcluirAgenda() {
            debugger;
            var hora = document.getElementById("Conteudo_hdfHoraSelecionada").value;
            var data = document.getElementById("Conteudo_txtData").value;

            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;

            window.PageMethods.AcaoBtnExcluirAgenda(data, hora, idProfissional, onSucess, onError);

            function onSucess(result) {
                document.getElementById("ltlTabelaHorarios").innerHTML = result;

                $('#myModalExcluir').modal('hide');
            }

            function onError(result) {
                alert('acaoBtnExcluirAgenda-Erro: Contate o Administrador!!');
            }

        }


        function capturaIdAgendaTemp(idAgdTmp, hora, idCliente) {
            debugger;
            document.getElementById("Conteudo_hdfIdAgdTemp").value = idAgdTmp;

            var c = document.getElementById("Conteudo_ddlCliente");
            //c.options[c.selectedIndex].value = idCliente;

            for (var i = 0; i < c.options.length; i++) {
                if (c.options[i].value == idCliente) {
                    debugger;
                    c.options[i].selected = true;
                    break;
                }
            }

            //var hora = document.getElementById("Conteudo_hdfHoraSelecionada").value;
            document.getElementById("Conteudo_lblHorarioConformar").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";

        }


        function fecharAgenda(hora) {
            debugger;
            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;

            var data = document.getElementById("Conteudo_txtData").value;

            window.PageMethods.FecharAgenda(data, hora, idProfissional, onSucess, onError);

            function onSucess(result) {
                document.getElementById("ltlTabelaHorarios").innerHTML = result;

                var url = "default.aspx?dt=" + data + "&idp=" + idProfissional;
                window.location.href = url;
            }

            function onError(result) {
                alert('fecharAgenda-Erro: Contate o Administrador!!');
            }

        }

        function abrirAgenda(hora) {
            debugger;
            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;

            var data = document.getElementById("Conteudo_txtData").value;

            window.PageMethods.AbrirAgenda(data, hora, idProfissional, onSucess, onError);

            function onSucess(result) {
                document.getElementById("ltlTabelaHorarios").innerHTML = result;

                var url = "default.aspx?dt=" + data + "&idp=" + idProfissional;
                window.location.href = url;
            }

            function onError(result) {
                alert('fecharAgenda-Erro: Contate o Administrador!!');
            }

        }

        function acaoBtnConfirmarAgenda() {
            debugger;
            var clt = document.getElementById("Conteudo_ddlCliente");
            var idCliente = clt.options[clt.selectedIndex].value;

            var idAgdTmp = document.getElementById("Conteudo_hdfIdAgdTemp").value;

            window.PageMethods.ConfirmarAgenda(idAgdTmp, idCliente, onSucess, onError);

            function onSucess(result) {
                if (result == '') {
                    var data = document.getElementById("Conteudo_txtData").value;

                    var c = document.getElementById("Conteudo_ddlProfissional");
                    var idProfissional = c.options[c.selectedIndex].value;

                    var url = "default.aspx?dt=" + data + "&idp=" + idProfissional;
                    window.location.href = url;
                }

                document.getElementById("ltlMsn").innerHTML = result;
            }

            function onError(result) {
                alert('confirmarAgenda-Erro: Contate o Administrador!!');
            }

        }




    </script>

</asp:Content>
