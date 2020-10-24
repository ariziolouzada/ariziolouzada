<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.ferrita.colaborador._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Colaboradores<small></small></h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Colaborador</li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="box">
            <div class="box-header with-border">
                <h3 class="box-title">Lista</h3>
            </div>
            <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-11">
                                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Pesquisar</label>
                                <input id="txtPesquisaValue" runat="server" class="form-control" type="text" placeholder="Pelo Nome">
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button class="btn btn-warning" ID="btnPesquisar" runat="server" Text="Pesquisar" OnClick="btnPesquisar_Click" Width="80px" />
                            </div>
                            <div class="col-md-5">
                            </div>
                            <div class="col-md-1">
                                <label></label>
                                <a class="btn btn-primary btn-rounded" href="newedit.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
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
