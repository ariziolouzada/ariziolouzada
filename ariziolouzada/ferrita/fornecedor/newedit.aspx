<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.ferrita.fornecedor.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Fornecedor       
                                <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="default.aspx"><i class="fa fa-truck"></i>Fornecedores</a></li>
            <li class="active">NewEdit Fornecedor</li>
        </ol>
    </section>
    
    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="box">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro"></asp:Label>
                </h3>
            </div>
            <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-12">
                                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Nome</label>
                                <input id="txtNome" runat="server" class="form-control" type="text" placeholder="Nome Fornecedor">
                            </div>
                            <div class="col-md-3">
                                <label>CPF</label>
                                <input id="txtCnpj" runat="server" class="form-control" type="text" placeholder="CNPJ do Colaborador">
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">                               

                                <div class="col-md-4">
                                    <label>Telefone</label>
                                    <input id="txtTelefone" runat="server" class="form-control" type="text" placeholder="telefone de contato">
                                </div>

                                <asp:Panel ID="pnlStatus" runat="server">
                                    <div class="col-md-2">
                                        <label>Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem>Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" Width="100px"/>
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"  Width="100px"/>
                                </div>

                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
            
        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
