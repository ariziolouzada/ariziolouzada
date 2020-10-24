<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.servico.newedit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/plugins/dataTables/datatables.min.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server" class="form-horizontal">

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text="CADASTRAR SERVIÇO"></asp:Label></h4>
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
                                    <input type="text" class="form-control" runat="server" id="txtDescricao" maxlength="50">
                                </div>
                            </div>

                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label">Valor (R$):</label>
                                <div class="col-sm-10 col-md-10">
                                    <input type="text" class="form-control" placeholder="Valor" runat="server" id="txtValor">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            
                            <asp:Panel ID="pnlStatus" runat="server" Visible="false">
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
                            </asp:Panel>

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
</asp:Content>
