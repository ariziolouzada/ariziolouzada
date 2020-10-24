<%@ Page Title="" Language="C#" MasterPageFile="~/ast/ast.Master" AutoEventWireup="true" CodeBehind="alterarsenha.aspx.cs" Inherits="ariziolouzada.ast.sistema.alterarsenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-12">
                <h2>Alterar Senha</h2>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label">Nova Senha</label>
                    <div class="col-sm-3">
                        <input type="password" name="txtSenha1" id="txtSenha1" class="form-control" autocomplete="off" required>
                    </div>
                    <div class="col-sm-7"></div>
                </div>
                <br />
                <br />
                <div class="form-group">
                    <label class="col-sm-2 control-label">Confirmar Senha</label>
                    <div class="col-sm-3">
                        <input type="password" name="txtSenha2" id="txtSenha2" class="form-control" autocomplete="off" required>
                    </div>
                    <div class="col-sm-7"></div>

                </div>
                <br />
                <br />

                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-md-3">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-w-m btn-primary" OnClick="btnConfirmar_Click" />
                    </div>
                </div>

                <br />
                <br />
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-md-10">
                        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                    </div>
                </div>


            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
