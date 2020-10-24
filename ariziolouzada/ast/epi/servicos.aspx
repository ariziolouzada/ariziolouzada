<%@ Page Title="" Language="C#" MasterPageFile="~/ast/ast.Master" AutoEventWireup="true" CodeBehind="servicos.aspx.cs" Inherits="ariziolouzada.ast.epi.servicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <meta charset="utf-8">
    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="../css/plugins/datapicker/datepicker3.css" rel="stylesheet" />

    <link href="../css/plugins/dataTables/datatables.min.css" rel="stylesheet">

    <link href="../css/animate.css" rel="stylesheet">
    <link href="../css/style.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>


        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-4">
                <h2>Serviços Executados</h2>
            </div>
            <div class="col-sm-7 "></div>
            <div class="col-sm-1 ">
                <a class="btn btn-primary btn-rounded" href="neweditservico.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Lista de serviços executados.</h5>
                        </div>
                        <div class="ibox-content">

                            <div class="table-responsive">

                                <div class="form-group" id="data_1">

                                    <label class="col-sm-1 control-label">Filtrar</label>
                                    <label class="col-sm-2 control-label">Data inicial:</label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" id="txtDataIncial" runat="server">
                                        </div>
                                    </div>

                                    <label class="col-sm-2 control-label">Data Final:</label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <input type="text" class="form-control" id="txtDataFinal" runat="server">
                                        </div>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtar" class="btn btn-info btn-rounded" OnClick="btnFiltrar_Click" />
                                        <%--<button class="btn btn-info btn-rounded" runat="server" id="btnFiltrar"><i class="fa fa-search"></i>&nbsp;Pesquisar</button>--%>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <div class="form-group">


                                    <asp:Literal ID="ltlTabelaServicos" runat="server"></asp:Literal>

                                    <%--<table class="table table-striped table-bordered table-hover dataTables-example">
                                <thead>
                                    <tr>
                                        <th>Descrição</th>
                                        <th>Valor Unitário</th>
                                        <th>Qtde Estoque</th>
                                        <th>Imagem</th>
                                        <th>Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="gradeX">
                                        <td>Trident</td>
                                        <td>Internet Explorer 4.0</td>
                                        <td>Win 95+</td>
                                        <td class="center">4</td>
                                        <td class="center">X</td>
                                    </tr>
                                </tbody>
                            </table>--%>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
            </div>
        </div>



        <!-- Modal new_edit_avisos  ltlTabelaMemorias  -->
        <div id="modalDeletePedSvc" class="modal fade" role="dialog" data-backdrop="static" tabindex="-1" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <!--<button type="button" class="close" data-dismiss="modal">&times;</button>-->
                        <div style="text-align: center;">
                            <h4 class="modal-title" id="myModalLabel"><strong>Excluir Pedido do Serviço</strong></h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div style="text-align: center;">
                            <%--<br />
                            <h3>Modal Body</h3>--%>
                            <div id="lblDeleteMemoria"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnExcluirPedSvc" class="btn btn-success btn-sm" runat="server" Text="Sim" OnClick="btnExcluirPedSvc_Click" />
                        <button id="fecharModal" type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Não</button>
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
    <script src="../js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../js/plugins/jeditable/jquery.jeditable.js"></script>

    <script src="../js/plugins/dataTables/datatables.min.js"></script>

    <!-- Data picker -->
    <script src="../js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {
            $('.dataTables-example').DataTable({
                "order": [],
                "columnDefs": [{
                    "targets": 'no-sort',
                    "orderable": false,
                }],

                dom: '<"html5buttons"B>lTfgitp',
                buttons: [
                    { extend: 'copy' },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'Lista de serviços executados.' },
                    { extend: 'pdf', title: 'Lista de serviços executados.' },

                    {
                        extend: 'print',
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');

                            $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', 'inherit');
                        }
                    }
                ]



            });

            /* Init DataTables */
            var oTable = $('#editable').DataTable();

            /* Apply the jEditable handlers to the table */
            oTable.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable.fnGetPosition(this);
                    oTable.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable.fnGetPosition(this)[2]
                    };
                },

                "width": "90%",
                "height": "100%"
            });


        });


        function fnClickAddRow() {
            $('#editable').dataTable().fnAddData([
                "Custom row",
                "New row",
                "New row",
                "New row",
                "New row"]);

        }


        $('#data_1 .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy',
            //language: 'pt-BR',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior'
        });



        function capturarIdServicoExcluir(id) {

            window.PageMethods.CapturarIdServicoExcluir(id, onSucess, onError);

            function onSucess(result) {
                document.getElementById("lblDeleteMemoria").innerHTML = result;
            }

            function onError(result) {
                alert('Erro: Contate o Administrador!!');
            }
        }



    </script>

</asp:Content>
