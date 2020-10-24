<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/minhagenda/minhagenda.Master" AutoEventWireup="true" CodeBehind="agendar.aspx.cs" Inherits="ariziolouzada.sbessencial.minhagenda.agendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/chosen/chosen.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-12">
                <h2>Agenda Salão Essencial</h2>
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
                                <%--<label class="col-sm-2 col-md-2 control-label"></label>--%>
                                <div class="col-sm-6 col-md-6 ibox-title" style="font-size: 12pt;">

                                    <asp:Label ID="lblHorarioEscolhido" runat="server" Text="Horário Escolhido" Style="font-weight: 700; color: #FF0000"></asp:Label>

                                    <br />
                                    <br />
                                    <p>
                                        <strong>Obrigado</strong><br />
                                        Você deve digitar seu NOME, TELEFONE de contato e selecionar o(s) serviço(s) que deseja agendar
                                        em breve retornaremos para confirmar o agendamento.
                                    </p>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Nome:</label>
                                <div class="col-sm-5 col-md-5">
                                    <input id="txtNome" runat="server" type="text" placeholder="Digite seu Nome" maxlength="20" class="form-control">
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Telefone:</label>
                                <div class="col-sm-5 col-md-5">
                                    <input id="txtTelefone" runat="server" type="text" placeholder="Telefone (XX) XXXXX-XXXX" maxlength="15" class="form-control" onkeypress="mascara(this, mtel);">
                                    <asp:HiddenField ID="hdfHoraSelecionada" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Serviço:</label>
                                <div class="col-sm-5 col-md-5">
                                    <asp:DropDownList ID="ddlServico" runat="server" class="chosen-select" multiple Style="width: 320px;" onchange="selecaoServico()">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdfSvcSelecionados" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label"></label>
                                <div class="col-sm-5 col-md-5">
                                    <br />
                                    <br />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-white" OnClick="btnCancelar_Click" />
                                    <asp:Button ID="btnAgendar" runat="server" Text="Agendar" class="btn btn-primary" OnClick="btnAgendar_Click" OnClientClick="return verificaBtnAgendar();" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <!-- Mainly scripts -->
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../js/plugins/metisMenu/jquery.metisMenu.js"></script>    

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="../js/plugins/chosen/chosen.jquery.js"></script>


    <script src="../js/mascaras.js"></script>

    <script type="text/javascript">

        $("#Conteudo_ddlServico").chosen({
            disable_search_threshold: 10,
            allow_single_deselect: true,
            no_results_text: "Nenhum serviço encontrado!",
            placeholder_text_multiple: "Selecione um serviço...",
            width: "95%"
        });

        //var config = {
        //    '.chosen-select': {},
        //    '.chosen-select-deselect': { allow_single_deselect: true },
        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
        //    '.chosen-select-no-results': { no_results_text: 'Nenhum serviço encontrado!' },
        //    '.chosen-select-width': { width: "95%" }
        //}
        //for (var selector in config) {
        //    $(selector).chosen(config[selector]);
        //}


        function capturaHorarioAgenda(hora) {
            debugger;
            document.getElementById("Conteudo_hdfHoraSelecionada").value = hora;
            document.getElementById("Conteudo_lblHorarioEscolhido").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";
            document.getElementById("Conteudo_lblHorarioExcluir").innerHTML = "Horário Escolhido: De " + hora + " às " + (hora + 1) + " Horas.";

            document.getElementById("Conteudo_txtNome").value = "";
            document.getElementById("Conteudo_txtTelefone").value = "";
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

        function selecaoServico() {
            //debugger;
            var slctSvc = document.getElementById("Conteudo_ddlServico");
            var idSvc = '';
            var arrayIdSvc = '';
            for (var i = 0; i < slctSvc.length; i++) {

                if (slctSvc.options[i].selected) {
                    idSvc = slctSvc.options[i].value;
                    if (arrayIdSvc == '') {
                        arrayIdSvc = idSvc;//se for o 1º a ser selecionado, não coloca o ";"
                    }
                    else {
                        arrayIdSvc = arrayIdSvc + ';' + idSvc;
                    }
                }
            }
            document.getElementById("Conteudo_hdfSvcSelecionados").value = arrayIdSvc;
        }

        function verificaBtnAgendar() {
            debugger;

            if (document.getElementById("Conteudo_txtNome").value == '') {
                alert('Erro: Digite seu NOME para realizar o agendamento!!');
                document.getElementById("Conteudo_txtNome").focus();
                return false;
            }

            if (document.getElementById("Conteudo_txtTelefone").value == '') {
                alert('Erro: Digite seu TELEFONE para realizar o agendamento!!');
                document.getElementById("Conteudo_txtTelefone").focus();
                return false;
            }

            var slctSvc = document.getElementById("Conteudo_ddlServico");
            if (slctSvc.length == 0) {
                alert('Erro: Selecione ao menos um serviço para realizar o agendamento!!');
                document.getElementById("Conteudo_ddlServico").focus();
                return false;
            }

            return true;
        }



    </script>

</asp:Content>

