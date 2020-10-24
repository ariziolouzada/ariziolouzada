<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.ferrita.fornecedor._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="../bower_components/Ionicons/css/ionicons.min.css">
    <!-- DataTables -->
    <link rel="stylesheet" href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Fornecedores<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Fornecedor</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="box">
            <div class="box-header with-border">
                <h3 class="box-title">Lista</h3>
            </div>
            <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-11">
                                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            </div>
                            <div class="col-md-1">
                                <a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
            <!-- /.box-body -->
            <%--<div class="box-footer">
                Footer       
            </div>--%>
            <!-- /.box-footer-->
        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <!-- jQuery 3 -->
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- DataTables -->
    <script src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script>

        $(function () {
           // $('#example1').DataTable()
            $('#tabela').DataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'ordering': true,
                'info': true,
                'autoWidth': false
            })
        })

        //$(document).ready(function () {
        //    debugger;
        //    $('#tabela').DataTable({
        //        "ordering": false,
        //        "language": {
        //            "lengthMenu": "Mostrando _MENU_ registros por página",
        //            "zeroRecords": "Nada encontrado",
        //            /* "info": "Mostrando página _PAGE_ de _PAGES_",*/
        //            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        //            "infoEmpty": "Nenhum registro disponível",
        //            "infoFiltered": "(filtrado de _MAX_ registros no total)",
        //            "sLoadingRecords": "Carregando...",
        //            "sProcessing": "Processando...",
        //            "sSearch": "Pesquisar",
        //            "oPaginate": {
        //                "sNext": "Próximo",
        //                "sPrevious": "Anterior",
        //                "sFirst": "Primeiro",
        //                "sLast": "Último"
        //            }
        //        }
        //    });
        //});

    </script>


</asp:Content>

