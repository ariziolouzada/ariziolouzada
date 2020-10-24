<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.sbessencial.agenda._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/chosen/chosen.css" rel="stylesheet" />
    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <link href="../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-6 col-sm-10 col-md-10 col-lg-10">
                    <h2>Agenda Diária dos Clientes</h2>
                </div>
                <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2">
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">

                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Data:</label>
                                <div class="col-sm-2  col-md-2">
                                    <input type="date" class="form-control" runat="server" id="txtData">
                                </div>

                                <label class="col-sm-2 col-md-2 control-label">PROFISSIONAL:</label>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="chosen-select" Style="width: 220px;" onchange="selecaoProfissional()">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                                </div>

                                <div class="col-sm-2 col-md-2">
                                    <asp:Button ID="btnCarregarLista" runat="server" Text="Carregar" class="btn btn-success btn-rounded" OnClick="btnCarregarLista_Click" />
                                </div>

                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo Item</a>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>


                            <%-- <div class="form-group">--%>
                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                            </div>
                            <%--</div>--%>
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
                        <asp:Button ID="btnSimDelReg" runat="server" Text="Sim" class="btn btn-primary" OnClick="BtnSimDelReg_Click" />
                        <%--<button type="button" class="btn btn-primary" onclick="excluirId()">Sim</button>--%>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">


    <!-- Chosen -->
    <script src="../js/plugins/chosen/chosen.jquery.js"></script>

    <script src="../js/jquery.dataTables/jquery.dataTables.min.js"></script>

    <script>

        function selecaoProfissional() {
            debugger;

            var c = document.getElementById("Conteudo_ddlProfissional");
            var idProfissional = c.options[c.selectedIndex].value;

            //document.getElementById("Conteudo_hdfIdProfissional").value = idProfissional;
            window.PageMethods.SelecaoProfissional(idProfissional, onSucess, onError);

            function onSucess(result) {

                if (result != '')
                    document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
            }

            function onError(result) {
                alert('selecaoProfissional-Erro: Contate o Administrador!!');
            }
        }

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

        $(document).ready(function () {
            //debugger;
            $('#tabelaFluxoCx').DataTable({
                "ordering": false,
                "language": {
                    "lengthMenu": "Mostrando _MENU_ registros por página",
                    "zeroRecords": "Nada encontrado",
                    /* "info": "Mostrando página _PAGE_ de _PAGES_",*/
                    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                    "infoEmpty": "Nenhum registro disponível",
                    "infoFiltered": "(filtrado de _MAX_ registros no total)",
                    "sLoadingRecords": "Carregando...",
                    "sProcessing": "Processando...",
                    "sSearch": "Pesquisar",
                    "oPaginate": {
                        "sNext": "Próximo",
                        "sPrevious": "Anterior",
                        "sFirst": "Primeiro",
                        "sLast": "Último"
                    }
                }
            });
        });

    </script>


    <script>

        function confirmacaoAgenda(id) {
            debugger;
            window.PageMethods.ConfirmacaoAgenda(id, onSucess, onError);

            function onSucess(result) {

                if (result != '')
                    document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
            }

            function onError(result) {
                alert('confirmacaoAgenda-Erro: Contate o Administrador!!');
            }
        }

        function capturarId(id) {
            debugger;
            window.PageMethods.CapturarId(id, onSucess, onError);

            function onSucess(result) {

                if (result != '') {
                    window.location.href = 'default.aspx?dia=' + resul;
                }
                   // document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
            }

            function onError(result) {
                alert('capturarId-Erro: Contate o Administrador!!');
            }
        }

        function excluirId() {

            window.PageMethods.ExcluirId(onSucess, onError);
            debugger;
            function onSucess(result) {
                debugger;
                if (result != '') {
                    window.location.href = 'default.aspx?dia=' + resul;
                    //var arrayPar = result;
                    //var param = arrayPar.split(';');
                    //window.location.href = 'default.aspx?dia=' + param[0];
                }
            }

            function onError(result) {
                alert('excluirId-Erro: Contate o Administrador!!');
            }
        }


    </script>

</asp:Content>


