<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="servicos.aspx.cs" Inherits="ariziolouzada.sbessencial.profissional.servicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/jquery.dataTables/jquery.dataTables.min.css" rel="stylesheet" />

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
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

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

        function doalert(checkboxElem, idSvc) {
            debugger;

            var idProfissional = document.getElementById("<%=hdfIdProfissional.ClientID%>").value;

            if (checkboxElem.checked) {
                //alert("hi" + idSvc);                

                window.PageMethods.AdicionarServicoProfissional(idSvc, idProfissional, onSucess, onError);
                function onSucess(result) {

                }
                function onError(result) {
                    alert('Erro ao Adicionar Servico: Contate o Administrador!!');
                }

            } else {
                //alert("bye");

                window.PageMethods.RetirarServicoProfissional(idSvc, idProfissional, onSucess, onError);
                function onSucess(result) {

                }
                function onError(result) {
                    alert('Erro ao Retirar Servico: Contate o Administrador!!');
                }


            }
        }


    </script>

</asp:Content>
