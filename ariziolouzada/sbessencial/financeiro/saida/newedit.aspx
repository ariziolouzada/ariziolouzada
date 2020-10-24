<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.sbessencial.financeiro.saida.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%--    <link href="../css/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="../css/plugins/dataTables/datatables.min.css" rel="stylesheet">

    <link href="../css/animate.css" rel="stylesheet">
    <link href="../css/style.css" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text="CADASATRAR SAÍDA"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Descrição:</label>
                                <div class="col-sm-10 col-md-10">
                                    <input type="text" class="form-control" runat="server" placeholder="Descrição" id="txtDescricao">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

<%--                            <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="col-sm-2 col-md-2 control-label">Status:</label>
                                    <div class="col-sm-10 col-md-10">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                            </asp:Panel>--%>

                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label"></label>
                                <div class="col-md-10 col-sm-10 ">
                                    <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
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

  <%--  <!-- Mainly scripts -->
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <!-- Input Mask-->
    <script src="../js/plugins/jasny/jasny-bootstrap.min.js"></script>--%>

    <script src="../js/mascaras.js"></script>


</asp:Content>

