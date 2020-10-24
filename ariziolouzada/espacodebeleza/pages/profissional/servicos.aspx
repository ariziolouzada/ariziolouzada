<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="servicos.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.profissional.servicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="../../css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet">

    
    <style>
        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #1f5688; /* Blue */
            border-radius: 50%;
            width: 120px;
            height: 120px;
            animation: spin 2s linear infinite;
            margin: auto;
            padding: 10px;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text=""></asp:Label></h4>
                            <asp:HiddenField ID="hdfIdProfCriptografado" runat="server" />
                            <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-xs-2 col-sm-2 col-md-2 control-label">Filtrar pelo nome:</label>
                                <div class="col-xs-2 col-sm-4  col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtPesquisa" maxlength="20">
                                </div>

                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Mostrar:</label>
                                <div class="col-xs-3 col-sm-3  col-md-3">
                                    <asp:DropDownList ID="ddlExibir" runat="server" class="form-control">
                                        <asp:ListItem Value="1">Todos os Serviços</asp:ListItem>
                                        <asp:ListItem Value="2">Só os selecionados</asp:ListItem>
                                        <asp:ListItem Value="3">Não selecionados</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2 col-sm-2 col-md-2">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="btn btn-success btn-rounded" OnClick="btnFiltrar_Click" />
                                </div>

                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label"></label>
                                <div class="col-md-9 col-sm-10 ">
                                    <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>


        <div class="modal inmodal fade" id="myModal6" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>--%>
                        <h4 class="modal-title">Editar Comissão</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="control-label">Comissão %: </label>
                            <input type="text" class="form-control" runat="server" id="txtComissao" maxlength="3" onkeypress="mascara(this, mnumeros);" />
                            <asp:HiddenField ID="hdfIdServico" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <button type="button" class="btn btn-primary" onclick="salvarComissao();">Salvar</button>
                        <%--<asp:Button ID="btnSalvarComissao" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvarComissao_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
        
        <div id="loader" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Aguarde ... </h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div class="loader centered"></div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script src="../../js/jquery.dataTables/jquery.dataTables.min.js"></script>
    <script src="../../js/mascaras.js"></script>
    <script src="../../js/plugins/iCheck/icheck.min.js"></script>

    <script>

        $(document).ready(function () {

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });

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

        function doalert(checkboxElem, idSvc) {
            debugger;
            
            $('#loader').modal('show');

            var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;

            if (checkboxElem.checked) {
                //alert("hi" + idSvc);                

                window.PageMethods.AdicionarServicoProfissional(idSvc, idProfissional, onSucess, onError);
                function onSucess(result) {
                    debugger;
                    window.location.href = 'servicos.aspx?id=' + document.getElementById("<%=hdfIdProfCriptografado.ClientID%>").value;
                }
                function onError(result) {
                    alert('Erro ao Adicionar Servico: Contate o Administrador!!');
                }

            } else {

                window.PageMethods.RetirarServicoProfissional(idSvc, idProfissional, onSucess, onError);
                function onSucess(result) {

                }
                function onError(result) {
                    alert('Erro ao Retirar Servico: Contate o Administrador!!');
                }


            }
        }

        function capturaDadosEditComissao(idSvc) {
            debugger;
            document.getElementById("<%=hdfIdServico.ClientID%>").value = idSvc;

            var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;

            window.PageMethods.CapturaDadosEditComissao(idSvc, idProfissional, onSucess, onError);
            function onSucess(result) {
                if (result != '')
                    document.getElementById("<%=txtComissao.ClientID%>").value = result;
            }
            function onError(result) {
                alert('Erro capturaDadosEditComissao: Contate o Administrador!!');
            }

        }

        function salvarComissao() {
            debugger;
            if (document.getElementById("<%=txtComissao.ClientID%>").value == '') {
                alert('Erro: Digite o valorr em % da comissão!!!');
            }
            else {

                var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;
                var idSvc = document.getElementById("<%=hdfIdServico.ClientID%>").value;
                var comissao = document.getElementById("<%=txtComissao.ClientID%>").value;

                window.PageMethods.SalvarComissao(idSvc, idProfissional, comissao, onSucess, onError);
                function onSucess(result) {
                    if (result != '') {
                        document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
                    } else {
                        window.location.href = 'servicos.aspx?id=' + document.getElementById("<%=hdfIdProfCriptografado.ClientID%>").value;
                    }
                }
                function onError(result) {
                    alert('Erro salvarComissao: Contate o Administrador!!');
                }

            }
        }

    </script>

</asp:Content>

