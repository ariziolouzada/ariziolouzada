<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="listacmdacolab.aspx.cs" Inherits="ariziolouzada.ferrita.colaborador.listacmdacolab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Comandas Colaborador
            <small>
                <asp:Label ID="lblNomeColaborador" runat="server" Text="Label"></asp:Label>
            </small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="default.aspx"><i class="fa fa-dashboard"></i>Colaboradores</a></li>
            <li class="active">Comandas</li>
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
                            <div class="col-md-1">
                                <%--<a class="btn btn-primary btn-rounded" href="neweditcomanda.aspx?id=X5A1oqTnjBE="><i class="fa fa-plus-square"></i>&nbsp;Novo</a>--%>
                                <asp:Button ID="btnNovo" runat="server" Text="Novo"  class="btn btn-primary btn-rounded" OnClick="btnNovo_Click" />
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
