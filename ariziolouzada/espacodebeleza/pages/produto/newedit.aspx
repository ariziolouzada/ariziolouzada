<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.produto.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text="CADASTRAR PRODUTO"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-xs-4 col-sm-3 col-md-2 control-label">Descrição:</label>
                                <div class="col-xs-8 col-sm-9 col-md-10">
                                    <input type="text" class="form-control" runat="server" placeholder="Descrição simples" id="txtDescProduto" maxlength="100">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-xs-4 col-sm-3 col-md-2 control-label">Descrição Completa:</label>
                                <div class="col-xs-8 col-sm-9 col-md-10">
                                    <textarea rows="4" class="form-control" runat="server" placeholder="Descrição completa/Marca/Volume do produto" id="txtDescCompleta" maxlength="300"></textarea>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Pço. Custo:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="text" class="form-control" placeholder="Preço de custo" runat="server" id="txtPrecoCusto" onkeypress="mascara(this, mdinheiro);">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">Pço. Venda:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" placeholder="Preço de Venda" runat="server" id="txtPrecoVenda" onkeypress="mascara(this, mdinheiro);">
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Qtd Estoque:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtQtdEstoque" placeholder="Qtde em estoque" onkeypress="mascara(this, mnumeros);">
                                </div>
                                <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                    <label class="col-sm-3 col-md-2 control-label">Status:</label>
                                    <div class="col-sm-3 col-md-4">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label"></label>
                                <div class="col-md-9 col-sm-10 ">
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

    <script src="../js/mascaras.js"></script>

</asp:Content>

