<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.cliente.newedit" %>

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
                                <asp:Label ID="lblCabecalho" runat="server" Text="CADASTRAR CLIENTE"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-xs-2 col-sm-2 col-md-2 control-label">Nome:</label>
                                <div class="col-xs-6 col-sm-6 col-md-6">
                                    <input type="text" class="form-control" runat="server" placeholder="Nome" id="txtNome">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">CPF:</label>
                                <div class="col-sm-2 col-md-2">
                                    <input type="text" class="form-control" placeholder="CPF" runat="server" id="txtCpf" maxlength="14" onkeypress="mascara(this, mcpf);">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Data Nascto.:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="date" class="form-control" runat="server" id="txtData" maxlength="10">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">Instagram:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" placeholder="Instagram" runat="server" id="txtInstagram">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Email:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="text" class="form-control" placeholder="Email" runat="server" id="txtEmail">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">Facebook:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" placeholder="Facebook" runat="server" id="txtFacebook">
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Telefone Celular 1:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtTelCel1" placeholder="(XX) XXXXX-XXXX" maxlength="15" onkeypress="mascara(this, mtel);">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">Tel. Cel. 2:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtTelCel2" maxlength="15" placeholder="(XX) XXXXX-XXXX" onkeypress="mascara(this, mtel);">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label">Telefone Fixo:</label>
                                <div class="col-sm-3 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtTelFixo" maxlength="15" placeholder="(XX) XXXXX-XXXX" onkeypress="mascara(this, mtel);">
                                </div>
                                <label class="col-sm-2 col-md-2 control-label">Observação:</label>
                                <div class="col-sm-4 col-md-4">
                                    <input type="text" class="form-control" runat="server" id="txtObservacao" maxlength="250">
                                </div>

                            </div>

                            <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                <div class="form-group">
                                    <label class="col-sm-3 col-md-2 control-label">Status:</label>
                                    <div class="col-sm-3 col-md-2">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-2 control-label"></label>
                                <div class="col-md-9 col-sm-10 ">
                                    <asp:Button class="btn btn-white" ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                    <asp:HiddenField ID="hdfIdEmpresaContratante" runat="server" />
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

    <script src="../../js/mascaras.js"></script>
</asp:Content>
