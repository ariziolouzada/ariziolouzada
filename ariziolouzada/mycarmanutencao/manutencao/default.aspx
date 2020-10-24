<%@ Page Title="" Language="C#" MasterPageFile="~/mycarmanutencao/mycar.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.mycarmanutencao.manutencao._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>My Car Manutenção</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Lista de Manutenções</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <section class="content">

        <div class="container-fluid">
            <div class="row">
                <div class="col-12">

                    <!-- Default box -->
                    <div class="card card-info">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-10">
                                    <h3 class="card-title">Lista de Manutenções</h3>
                                </div>
                                <div class="col-2">
                                    <asp:Literal ID="ltlBtnNovaManutacao" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Marca / Modelo</label>
                                        <asp:DropDownList ID="ddlCarro" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCarro_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                        </div>
                        <!-- /.card-body -->

                    </div>
                    <!-- /.card -->

                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->


    </section>
    <!-- /.content -->


    <div class="modal fade" id="modal-delete" style="display: none;" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content bg-danger">
                <div class="modal-header">
                    <h4 class="modal-title">Apagar Manutenção!
                    </h4>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">X</span>
                    </button>--%>
                </div>
                <div class="modal-body">
                    <p style="font-size: 12pt;">Deseja realmente apagar a manutenção selecionada?</p>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-outline-light" data-dismiss="modal">Fechar</button>
                    <%--<button id="btnConfirmar" type="button" class="btn btn-primary" data-dismiss="modal">Confirmar</button>--%>
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
                    <asp:HiddenField ID="hdfIdDel" runat="server" />
                    ]
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_scripts" runat="server">
    <!-- DataTables -->
    <script src="../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../plugins/datatables-bs4/js/dataTables.bootstrap4.min.js"></script>
    <script src="../plugins/datatables-responsive/js/dataTables.responsive.min.js"></script>
    <script src="../plugins/datatables-responsive/js/responsive.bootstrap4.min.js"></script>
    
    <script>
        $(function () {
            $('#example1').DataTable({
                //"paging": true,
                //"lengthChange": true,
                //"searching": true,
                "ordering": false,
                "info": true,
                "autoWidth": false,
                "responsive": true,
                "language": {
                    "sEmptyTable": "Não foi encontrado nenhum registo",
                    "sLoadingRecords": "A carregar...",
                    "sProcessing": "A processar...",
                    "sLengthMenu": "Mostrar _MENU_ registos",
                    "sZeroRecords": "Não foram encontrados resultados",
                    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registos",
                    "sInfoEmpty": "Mostrando de 0 até 0 de 0 registos",
                    "sInfoFiltered": "(filtrado de _MAX_ registos no total)",
                    "sInfoPostFix": "",
                    "sSearch": "Procurar:",
                    "sUrl": "",
                    "oPaginate": {
                        "sFirst": "Primeiro",
                        "sPrevious": "Anterior",
                        "sNext": "Seguinte",
                        "sLast": "Último"
                    },
                    "oAria": {
                        "sSortAscending": ": Ordenar colunas de forma ascendente",
                        "sSortDescending": ": Ordenar colunas de forma descendente"
                    }
                }
            });
        });
</script>


    <script>

        function guardaIdExcluir(id) {
            debugger;
            document.getElementById("<%=hdfIdDel.ClientID%>").value = id;
        }
    </script>

</asp:Content>
