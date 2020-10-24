<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.sbessencial.servico._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <h2>Lista dos Serviços</h2>
                </div>
                <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2">
                    <a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <%-- <div class="ibox-title">
                            <h5>Lista de Serviços</h5>
                        </div>--%>

                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-sm-3 col-md-3 control-label">Filtrar pela descrição:</label>
                                <div class="col-sm-4  col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtPesquisa" maxlength="30">
                                </div>
                                <label class="col-sm-1 col-md-1 control-label">Mostrar:</label>
                                <div class="col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Ativos</asp:ListItem>
                                        <asp:ListItem Value="2">Inativos</asp:ListItem>
                                        <asp:ListItem Value="3">Excluídos</asp:ListItem>
                                        <asp:ListItem Value="-1">Todos</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-success btn-rounded" OnClick="btnFiltrar_Click" />
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
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


        <div class="modal inmodal fade" id="myModal6" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                        <h4 class="modal-title">Excluir Serviço</h4>
                    </div>
                    <div class="modal-body">
                        <p>Deseja realmente excluir o serviço selecionado?</p>
                        <div id="msnModal"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Não</button>
                        <button type="button" class="btn btn-primary" onclick="excluirServico();">Sim</button>
                        <asp:HiddenField ID="hdfIdServicoExcluir" runat="server" />
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <%--<script src="../js/jquery.dataTables/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/jquery.dataTables/jquery.dataTables.min.js"></script>

    <script>

        $(document).ready(function () {
            //debugger;
            $('#tabelaServicos').DataTable({
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

        function capturarIdServicoExcluir(idServico) {
            document.getElementById("<%=hdfIdServicoExcluir.ClientID%>").value = idServico;
        }

        function excluirServico() {
            debugger;
            var idServico = document.getElementById("<%=hdfIdServicoExcluir.ClientID%>").value;
            window.PageMethods.ExcluirServico(idServico, onSucess, onError);
            function onSucess(result) {
                if (result == '') {
                    window.location.href = 'default.aspx';
                }
                else {
                    document.getElementById("msnModal").innerHTML = result;
                }
            }
            function onError(result) {
                alert('Erro excluirServico: Contate o Administrador!!');
            }
        }

    </script>

</asp:Content>
