<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/pages/colaborador/espacobelezacolaborador.Master" AutoEventWireup="true" CodeBehind="listacomandas.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.colaborador.listacomandas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- Sweet Alert 
    <link href="../css/plugins/sweetalert/sweetalert.css" rel="stylesheet">-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-9 col-md-9 col-lg-9">
                    <h2>Lista das Comandas</h2>
                    <asp:Literal ID="ltlNomeProfissional" runat="server"></asp:Literal>
                </div>
                <div class="col-xs-4 col-sm-3 col-md-3 col-lg-3">
                    <asp:Literal ID="ltlTotalSomaComanda" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class=" col-md-12 col-lg-12">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                    <div id="msnResetSenha"></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <%--<div class="ibox-title">
                            <h5>Lista de Clientes</h5>
                        </div>--%>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-xs-2 col-sm-1 col-md-1  col-lg-1 control-label">Data:</label>
                                <div class="col-xs-2 col-sm-3  col-md-3 col-lg-3">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10">
                                </div>
                                <div class="col-xs-2 col-sm-2  col-md-2 col-lg-2">
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">                                       
                                        <asp:ListItem Value="0">Todas</asp:ListItem>
                                        <asp:ListItem Value="1">Em Aberto</asp:ListItem>
                                        <asp:ListItem Value="2">Fechadas</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-2">
                                    <%--<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-success btn-rounded btn-outline" OnClick="btnFiltrar_Click" ></asp:Button>--%>
                                    <a class="btn btn-success btn-rounded btn-outline" href="#" onclick="carregarComandas();" title="Carregar lista das comanda."><i class="fa fa-search fa-2x"></i></a>
                                </div>
                                <div class="col-xs-3 col-sm-3 col-md-3 col-lg-2">
                                    <a class="btn btn-primary btn-rounded btn-outline" href="comanda.aspx?id=X5A1oqTnjBE=" title="Criar comanda."><i class="fa fa-plus fa-2x"></i></a>
                                </div>

                                <br />
                                <br />
                                <div class="hr-line-dashed"></div>
                            </div>

                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                    <div id="tabelaComandas"></div>
                                </div>
                            </div>

                            <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal" id="myModal2" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated flipInY">
                    <div class="modal-header">
                        <%--                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <h4 class="modal-title">Serviços da Comanda</h4>
                        <%--<small class="font-bold">Comanda nº </small>--%>
                    </div>
                    <div class="modal-body">
                        <div id="servicosComandaModal"></div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <%--<script src="../js/jquery.dataTables/jquery-3.2.1.min.js"></script>--%>
    <script src="../../js/jquery.dataTables/jquery.dataTables.min.js"></script>

    <script>

        $(document).ready(function () {
            //debugger;
            $('#tabelaClientes').DataTable({
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


        function carregarComandas() {

            var data = document.getElementById("Conteudo_txtData").value;
            var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;

            var select = document.getElementById("Conteudo_ddlTipo");
            var idTipo = select.options[select.selectedIndex].value;

            window.PageMethods.CarregaTabelaComandas(data, idProfissional, idTipo, onSucess, onError);
            function onSucess(result) {
                debugger;
                // if (document.getElementById("<%=ltlTabela.ClientID%>") != null)
                document.getElementById("divTabelaComandas").innerHTML = '';

                document.getElementById("tabelaComandas").innerHTML = result;

                $('#tabelaClientes').DataTable({
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

            }
            function onError(result) {
                alert('Erro carregaComandas: Contate o Administrador!!');
            }
        }

        function carregaServicoCmdaModal(idComanda) {

            window.PageMethods.CarregaServicoCmdaModal(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("servicosComandaModal").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaServicoCmdaModal: Contate o Administrador!!');
            }
        }

        function resetarsenha(idClt) {

            window.PageMethods.ResetarSenha(idClt, onSucess, onError);

            function onSucess(result) {
                debugger;
                if (result == '1') {
                    resetOk();
                } else {
                    resetErro(result);
                    //document.getElementById("msnResetSenha").innerHTML = result;
                }
            }

            function onError(result) {
                alert('resetarsenha-Erro: Contate o Administrador!!');
            }

        }

        
        function editarDadosComanda(idComanda) {
            window.location.href = 'comanda.aspx?id=' + idComanda;
        }

        //function resetOk() {
        //    debugger;
        //    swal({
        //        title: "Reset de Senha!",
        //        text: "Senha resetada com sucesso!",
        //        type: "success"
        //    });

        //}

        //function resetErro(texto) {
        //    debugger;
        //    swal({
        //        title: "Erro no Reset de Senha!",
        //        text: texto,
        //        type: "error"
        //    });

        //}

    </script>


    <!-- Mainly scripts 
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../js/plugins/jeditable/jquery.jeditable.js"></script>-->

    <%--<script src="../js/plugins/dataTables/datatables.min.js"></script>--%>

    <!-- Custom and plugin javascript
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>
     -->

    <!-- Sweet alert 
    <script src="../js/plugins/sweetalert/sweetalert.min.js"></script>-->

</asp:Content>
