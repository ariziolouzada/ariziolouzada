<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.sbessencial.cliente._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="../css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <h2>Lista dos Clientes</h2>
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
                        <%--<div class="ibox-title">
                            <h5>Lista de Clientes</h5>
                        </div>--%>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-xs-4 col-sm-2 col-md-2 control-label">Filtrar pelo nome:</label>
                                <div class="col-xs-4 col-sm-8  col-md-8">
                                    <input type="text" class="form-control" runat="server" id="txtPesquisa" maxlength="20">
                                </div>
                                <div class="col-xs-4 col-sm-2 col-md-2">
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
                    <div id="msnResetSenha"></div>
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

        function resetOk() {
            debugger;
            swal({
                title: "Reset de Senha!",
                text: "Senha resetada com sucesso!",
                type: "success"
            });

        }

        function resetErro(texto) {
            debugger;
            swal({
                title: "Erro no Reset de Senha!",
                text: texto,
                type: "error"         
            });

        }

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

