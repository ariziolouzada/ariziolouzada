<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="ariziolouzada.acaoentreamigos.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="Arizio Aguilar Oliveira Louzada" />

    <title>Ação Entre Amigos</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />

    <%--<link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />--%>

    <link href="css/plugins/select2/select2.min.css" rel="stylesheet" />
    <!-- sweetalert2 -->
    <link href="js/sweetalert/sweetalert.css" rel="stylesheet" />


    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style>
        .ddlNum {
        }
    </style>

</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="animated fadeInDown">
            <div class="text-center">
                <h1><b>Ação Entre Amigos - Administrador</b></h1>
                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                <asp:HiddenField ID="hdfValueBegin" runat="server" />
                <asp:HiddenField ID="hdfValueEnd" runat="server" />
            </div>

            <div class="row">
                <div class="form-group">

                    <div class="col-lg-12">
                        <div class="tabs-container">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#tab-1" aria-expanded="true">Números</a></li>
                                <li class=""><a data-toggle="tab" href="#tab-2" aria-expanded="false">Vendedores</a></li>
                                <li class=""><a data-toggle="tab" href="#tab-3" aria-expanded="false">Relatórios</a></li>
                            </ul>
                            <div class="tab-content">
                                <div id="tab-1" class="tab-pane active">
                                    <div class="panel-body">
                                        <h2><strong>Lista de Números</strong></h2>

                                        <div class="row">
                                            <div class="form-group">
                                                <%-- <div class="col-lg-2">
                                                    <strong>Lista dos Números</strong>
                                                </div>--%>
                                                <label class="col-sm-2 control-label">Lista dos Números</label>
                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                        <asp:ListItem>Selecione</asp:ListItem>
                                                        <asp:ListItem Value="-1">Todos</asp:ListItem>
                                                        <asp:ListItem Value="0">Disponíveis</asp:ListItem>
                                                        <asp:ListItem Value="1">Vendidos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Panel ID="pnlFiltro" runat="server">
                                                    <label class="col-sm-2 control-label text-right">Pesquisar de</label>
                                                    <div class="col-sm-2">
                                                        <input type="number" id="txtNumPesqInicio" class="form-control" runat="server" required="" maxlength="5" />
                                                    </div>
                                                    <label class="col-sm-1 control-label text-center">até</label>
                                                    <div class="col-sm-2">
                                                        <input type="number" id="txtNumPesqFim" class="form-control" runat="server" required="" maxlength="5" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-outline btn-success" OnClick="btnPesquisar_Click" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <br />

                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <div class="table-responsive">
                                                        <asp:Literal ID="ltlTabelaNumeros" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div id="tab-2" class="tab-pane">
                                    <div class="panel-body">
                                        <h2><strong>Lista de Vendedores</strong></h2>
                                        <br />
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <div class="table-responsive">
                                                        <asp:Literal ID="ltlTabelaVendedores" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div id="tab-3" class="tab-pane">
                                    <div class="panel-body">
                                        <h2><strong>Relatórios</strong></h2>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Qtde Números Vendidos:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblNumVendidos" runat="server" Text="0" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Qtde Números Disponíveis:</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="lblNumDisponiveis" runat="server" Text="0" Font-Bold="true" ForeColor="Green"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Exportar</label>

                                                <div class="col-sm-2">
                                                    <asp:DropDownList ID="ddlFiltroNumeros" runat="server" CssClass="form-control" >
                                                        <asp:ListItem Value="-1">Todos</asp:ListItem>
                                                        <asp:ListItem Value="0">Disponíveis</asp:ListItem>
                                                        <asp:ListItem Value="1">Vendidos</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <button class="btn btn-info " type="button" onclick="exportarExcel();"><i class="fa fa-file-excel-o fa-lg"></i>  Excel</button>
                                                </div>
                                                <div class="col-sm-6">
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>

                    <div class="col-sm-4 col-sm-offset-2">
                        <%-- <button type="button" class="btn btn-outline btn-warning" onclick="acaoBtnCancelar();"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                        <button type="button" class="btn btn-outline btn-primary" onclick="acaoBtnConfirmar();"><i class="fa fa-money"></i>&nbsp;Confirmar Venda</button>
                        --%>
                        <a href="login.aspx" class="btn btn-outline btn-danger"><i class="fa fa-sign-out"></i>&nbsp;Sair</a>
                    </div>

                </div>
            </div>

        </div>


        <div class="modal inmodal" id="myModalEditInvest" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content animated bounceInRight">
                    <div class="modal-header">
                        <i class="fa fa-dolar modal-icon"></i>
                        <h4 class="modal-title">Editar Vendedor</h4>

                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label>Nome</label>
                            <input id="txtNomeVendedorEdit" type="text" runat="server" class="form-control" />
                            <asp:HiddenField ID="hdfIdVendedorEdit" runat="server" />
                        </div>

                        <div class="form-group">
                            <label>Situação</label>
                            <asp:DropDownList ID="ddlStatusVendedor" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Inativo</asp:ListItem>
                                <asp:ListItem Value="1">Ativo</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div id="divMsnModalEdit"></div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" onclick="editarVendedor();">Salvar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal inmodal" id="myModalEditNumero" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content animated bounceInRight">
                    <div class="modal-header">
                        <i class="fa fa-dolar modal-icon"></i>
                        <h4 class="modal-title">Editar Número</h4>
                        <%--<small class="font-bold">Lorem Ipsum is simply dummy text of the printing and typesetting industry.</small>--%>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdfNumeroEdit" runat="server" />
                        <div id="divCamposModalEditNumero"></div>

                        <%--<div id="divMsnModalEdit"></div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <button type="button" class="btn btn-primary" onclick="editarNumero();">Salvar</button>
                    </div>
                </div>
            </div>
        </div>


    </form>


    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <%--<script src="js/jquery.dataTables/jquery-3.2.1.js"></script>--%>
    <script src="js/bootstrap.min.js"></script>

    <!-- Select2
    <script src="js/plugins/select2/select2.full.min.js"></script> -->
    <script src="js/sweetalert/sweetalert.min.js"></script>
    <%--<script src="js/plugins/dataTables/datatables.min.js"></script>--%>

    <script>

        function msn(tipo, texto) {
            //debugger;
            if (tipo == 0) { //men de Erro
                swal({
                    title: "Erro!",
                    text: texto,
                    type: "error"
                });
            }
            if (tipo == 1) { //men de Sucesso
                swal({
                    title: "Successo!",
                    text: texto,
                    type: "success"
                });
            }
        }

        function carregaIdVendedorEdit(idVendedor, nomeVendedor, idStatus) {
            debugger;
            document.getElementById("<%=hdfIdVendedorEdit.ClientID%>").value = idVendedor;
            document.getElementById("<%=ddlStatusVendedor.ClientID%>").value = idStatus;
            document.getElementById("<%=txtNomeVendedorEdit.ClientID%>").value = nomeVendedor;
        }

        function editarVendedor() {
            debugger;

            var id = document.getElementById("<%=hdfIdVendedorEdit.ClientID%>").value;
            var nome = document.getElementById("<%=txtNomeVendedorEdit.ClientID%>").value;
            var idStatus = document.getElementById("<%=ddlStatusVendedor.ClientID%>").value;

            window.PageMethods.EditarVendedor(id, nome, idStatus, onSucess, onError);
            function onSucess(result) {
                //debugger;

                if (result === 'EditVendedorOk') {

                    swal({
                        title: "PARABÉNS",
                        text: "Edição realizada com sucesso!",
                        type: "success"
                    },
                        function () {
                            window.location = 'admin.aspx';
                        });

                }
                else {
                    msn(0, result);
                    //return false;
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo verificaNumero - Contate o Administrador!!');
            }
        }

        function carregaNumeroEdit(numero) {
            debugger;
            document.getElementById("<%=hdfNumeroEdit.ClientID%>").value = numero;

            window.PageMethods.CarregaNumeroEdit(numero, onSucess, onError);
            function onSucess(result) {
                document.getElementById("divCamposModalEditNumero").innerHTML = result;
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo carregaNumeroEdit - Contate o Administrador!!');
            }
        }

        function editarNumero() {
            debugger;
            var numero = document.getElementById("<%=hdfNumeroEdit.ClientID%>").value;
            var nomeComprador = document.getElementById("txtCompradorEdit").value;
            var email = document.getElementById("txtEmailEdit").value;
            var tel = document.getElementById("txtTelefoneEdit").value;
            var dataVda = document.getElementById("txtDataVendaEdit").value;
            var idVendedor = document.getElementById("ddlVendedorEdit").value;
            var idStatus = document.getElementById("ddlStatusEdit").value;

            window.PageMethods.EditarNumero(numero, nomeComprador, email, tel, dataVda, idVendedor, idStatus, onSucess, onError);
            function onSucess(result) {
                //debugger;

                if (result === 'EditNumeroOk') {

                    swal({
                        title: "PARABÉNS",
                        text: "Edição realizada com sucesso!",
                        type: "success"
                    },
                        function () {
                            var valueBegin = document.getElementById("<%=hdfValueBegin.ClientID%>").value;
                            var valueEnd = document.getElementById("<%=hdfValueEnd.ClientID%>").value;
                            window.location = 'admin.aspx?valueBegin=' + valueBegin + '&valueEnd=' + valueEnd;
                        });

                }
                else {
                    msn(0, result);
                    //return false;
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo verificaNumero - Contate o Administrador!!');
            }

        }


        function exportarExcel() {
            var idStatus = document.getElementById("<%=ddlFiltroNumeros.ClientID%>").value;
            window.location = 'exportarexcel.aspx?ids=' + idStatus;
        }

        function reenviarEmail(numero) {
            debugger;

            window.PageMethods.ReenviarEmail(numero, onSucess, onError);
            function onSucess(result) {
                if (result === 'reenvioEmailOk') {
                    msn(1, 'Email reenviado com sucesso!!')
                } else {
                    msn(0, result)
                }
            }
            function onError(result) {
                //alert('Erro: Contate o Administrador!!');
                msn(0, 'Erro metodo carregaNumeroEdit - Contate o Administrador!!');
            }
        }

    </script>

</body>
</html>
