<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.comanda._default" %>

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
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <h2>Lista das Comandas</h2>
                </div>
                <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2">
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
                                <label class="col-xs-2 col-sm-1 col-md-1 control-label">Data:</label>
                                <div class="col-xs-2 col-sm-3  col-md-3">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10">
                                </div>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Todas</asp:ListItem>
                                        <asp:ListItem Value="1">Em Aberto</asp:ListItem>
                                        <asp:ListItem Value="2">Fechadas</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2 col-sm-1 col-md-1">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-success btn-rounded" OnClick="btnFiltrar_Click" />
                                </div>
                                <div class="col-xs-2 col-sm-1 col-md-1">
                                    <a class="btn btn-primary btn-rounded" href="javascript:;" onclick="novaComanda();"><i class="fa fa-plus-square"></i>&nbsp;Novo</a>
                                </div>
                                <label class="col-xs-2 col-sm-1 col-md-1 control-label">TOTAL:</label>
                                <div class="col-xs-2 col-sm-3  col-md-3">
                                    <strong>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="form-control" Text="0" Style="color: #FF0000; font-size: medium"></asp:Label>
                                    </strong>
                                </div>
                                <br />
                                <div class="hr-line-dashed"></div>
                            </div>

                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                </div>
                            </div>

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

        <div class="modal inmodal" id="myModal3" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated flipInY">
                    <div class="modal-header">
                        <%--                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <h4 class="modal-title">Produtos da Comanda</h4>
                        <%--<small class="font-bold">Comanda nº </small>--%>
                    </div>
                    <div class="modal-body">
                        <div id="produtosComandaModal"></div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal inmodal" id="modalFecharComanda" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated flipInY">
                    <div class="modal-header">
                        <%--                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <h4 class="modal-title">Fechar Comanda</h4>
                        <%--<small class="font-bold">Comanda nº </small>--%>
                    </div>
                    <div class="modal-body">
                        <div id="dadosComandaModal"></div>

                        <div class="form-group">
                            <label class="col-xs-3 col-sm-3 col-md-3 control-label">Forma Pgto:</label>
                            <div class="col-xs-9 col-sm-9 col-md-9">
                                <asp:DropDownList ID="ddlFormaPgtoModal" runat="server" CssClass="form-control" onchange="capturaIdFormaPgto();">
                                    <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="1">Dinheiro</asp:ListItem>
                                    <asp:ListItem Value="2">Cartão Débito</asp:ListItem>
                                    <asp:ListItem Value="3">Cartão Crédito 30 dias</asp:ListItem>
                                    <asp:ListItem Value="4">Cartão Crédito Parcelado</asp:ListItem>
                                    <asp:ListItem Value="5">Cheque</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <asp:HiddenField ID="hdfIdComandaFechar" runat="server" />
                            <asp:HiddenField ID="hdfIdFormaPgtoComandaFechar" runat="server" />
                        </div>
                        <br />
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <button type="button" class="btn btn-primary" onclick="fecharComanda();">Confirmar</button>
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

        function novaComanda() {
            debugger;
            var data = document.getElementById("<%=txtData.ClientID%>").value;
            window.location.href = 'newedit.aspx?id=X5A1oqTnjBE=&data=' + data;
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

        function carregaProdutoCmdaModal(idComanda) {

            window.PageMethods.CarregaProdutoCmdaModal(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("produtosComandaModal").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaprodutoCmdaModal: Contate o Administrador!!');
            }
        }

        function carregaDadosCmdaModal(idComanda) {
            debugger;

            document.getElementById("<%=hdfIdComandaFechar.ClientID%>").value = idComanda;

            var select = document.getElementById("<%=ddlFormaPgtoModal.ClientID%>");
            select.selectedIndex = 0;
            <%--var idFormaPgto = select.options[select.selectedIndex].value;
            document.getElementById("<%=hdfIdFormaPgtoComandaFechar.ClientID%>").value = idFormaPgto;--%>

            window.PageMethods.CarregaDadosCmdaModal(idComanda, onSucess, onError);
            function onSucess(result) {
                debugger;
                document.getElementById("dadosComandaModal").innerHTML = result;
            }
            function onError(result) {
                alert('Erro carregaServicoCmdaModal: Contate o Administrador!!');
            }
        }

        function fecharComanda() {
            debugger;

            var idFormaPgto = document.getElementById("<%=hdfIdFormaPgtoComandaFechar.ClientID%>").value;
            if (idFormaPgto == '') {
                alert('Selecione a Forma de Pagamento da comanda.');
                document.getElementById("<%=ddlFormaPgtoModal.ClientID%>").focus();
            }
            else {
                var idComanda = document.getElementById("<%=hdfIdComandaFechar.ClientID%>").value;
                window.PageMethods.FecharComanda(idComanda, idFormaPgto, onSucess, onError);
                function onSucess(result) {
                    //debugger;
                    if (result == '')
                        window.location.href = 'default.aspx?data=' + document.getElementById("<%=txtData.ClientID%>").value;
                    else
                        document.getElementById("dadosComandaModal").innerHTML = result;
                }
                function onError(result) {
                    alert('Erro carregaServicoCmdaModal: Contate o Administrador!!');
                }
            }
        }

        function capturaIdFormaPgto() {
            var select = document.getElementById("<%=ddlFormaPgtoModal.ClientID%>");
            var idFormaPgto = select.options[select.selectedIndex].value;

            document.getElementById("<%=hdfIdFormaPgtoComandaFechar.ClientID%>").value = idFormaPgto;
        }

        function editarDadosComanda(idComanda) {
            window.location.href = 'newedit.aspx?id=' + idComanda;
        }

        //function resetarsenha(idClt) {

        //    window.PageMethods.ResetarSenha(idClt, onSucess, onError);

        //    function onSucess(result) {
        //        debugger;
        //        if (result == '1') {
        //            resetOk();
        //        } else {
        //            resetErro(result);
        //            //document.getElementById("msnResetSenha").innerHTML = result;
        //        }
        //    }

        //    function onError(result) {
        //        alert('resetarsenha-Erro: Contate o Administrador!!');
        //    }

        //}

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

    <!-- Sweet alert -->
    <script src="../js/plugins/sweetalert/sweetalert.min.js"></script>

</asp:Content>


