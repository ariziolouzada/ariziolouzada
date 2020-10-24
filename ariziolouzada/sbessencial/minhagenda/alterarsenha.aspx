<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/minhagenda/minhagenda.Master" AutoEventWireup="true" CodeBehind="alterarsenha.aspx.cs" Inherits="ariziolouzada.sbessencial.minhagenda.alterarsenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">

    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-12">
                <h2>Agenda Salão Essencial</h2>
            </div>
        </div>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAlterarSenha">
            <div class="wrapper wrapper-content">
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">

                            <div class="ibox float-e-margins">

                                <div class="ibox-title">
                                    <h5>Alterar a Senha  <small>Digite a sua nova senha.</small></h5>
                                </div>

                                <div class="ibox-content">

                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-1 col-md-2 control-label">Nova Senha:</label>
                                        <div class="col-sm-5 col-md-4">
                                            <input id="txtSenha" runat="server" type="password" placeholder="Nova senha" class="form-control">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-1 col-md-2 control-label">Confirme aNova Senha:</label>
                                        <div class="col-sm-5 col-md-4">
                                            <input id="txtSenhaConfirma" runat="server" type="password" placeholder="Confirme a Nova senha" class="form-control">
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-1 col-md-2 control-label"></label>
                                        <div class="col-sm-5 col-md-4">
                                            <br />
                                            <br />
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-white" OnClick="btnCancelar_Click" />
                                            <%-- OnClientClick="return verificaBtnAgendar();"--%>
                                            <asp:Button ID="btnAlterarSenha" runat="server" Text="Salvar" class="btn btn-primary" OnClick="btnAlterarSenha_Click" />
                                        </div>
                                    </div>


                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
