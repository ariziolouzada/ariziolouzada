<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.sbessencial.financeiro.fechamentodiario._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-6 col-sm-10 col-md-10 col-lg-10">
                    <h2>Fechamento Diário do Caixa</h2>
                </div>
                <div class="col-xs-6 col-sm-2 col-md-2 col-lg-2">
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Total Entrada:</label>
                                <div class="col-sm-2  col-md-2">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtTotalEntrada" style="color: #008000; font-weight: bold">
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Total Saída:</label>
                                <div class="col-sm-2  col-md-2">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtTotalSaida" style="color: #FF0000; font-weight: bold">
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Depósitos:</label>
                                <div class="col-sm-2  col-md-2">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtDeposito" style="color: #FF9900; font-weight: bold">
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Saldo:</label>
                                <div class="col-sm-2  col-md-2">
                                    <input type="text" disabled="" class="form-control" runat="server" id="txtSaldo">
                                </div>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label">Data:</label>
                                <div class="col-sm-3  col-md-3">
                                     <input type="date" class="form-control" runat="server" id="txtData">
                                </div>                                

                                <label class="col-sm-1 control-label">Tipo:</label>
                                <div class="col-sm-2 col-md-2">
                                    <asp:DropDownList ID="ddlTipo" runat="server" class="form-control ">
                                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="1">Entrada</asp:ListItem>
                                        <asp:ListItem Value="2">Saída</asp:ListItem>
                                        <asp:ListItem Value="3">Depósito</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Button ID="btnCarregarLista" runat="server" Text="Carregar" class="btn btn-success btn-rounded" OnClick="btnCarregarLista_Click" />
                                </div>

                            </div>
                            <div class="hr-line-dashed"></div>



                            <%--<div class="hr-line-dashed"></div>--%>

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
                        <%--<asp:Button ID="btnSimDelReg" runat="server" Text="Sim"  class="btn btn-primary" OnClick="btnSimDelReg_Click"/>--%>
                        <button type="button" class="btn btn-primary" onclick="excluirId()">Sim</button>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <%--<script src="../../js/jquery.dataTables/jquery-3.2.1.min.js"></script>--%>
    <script src="../../js/jquery.dataTables/jquery.dataTables.min.js"></script>

    <script>

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

        function capturarId(id) {
            debugger;
            window.PageMethods.CapturarId(id, onSucess, onError);

            function onSucess(result) {

                if (result != '')
                    document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
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

                    var arrayPar = result;
                    var param = arrayPar.split(';');
                    window.location.href = 'default.aspx?dia=' + param[0] + '&tipo=' + param[1];
                }

            }

            function onError(result) {
                alert('excluirId-Erro: Contate o Administrador!!');            }
        }


    </script>

</asp:Content>


