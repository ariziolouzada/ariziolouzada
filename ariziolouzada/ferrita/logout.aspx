<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="ariziolouzada.ferrita.logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Home       
                                <small>Site</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Logout</a></li>
        </ol>
    </section>

    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="box">
            <div class="box-header with-border">
                <h3 class="box-title"></h3>
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
                            <div class="col-md-12">
                                <label></label>
                                <h2>Deseja realmente sair do sistema?</h2>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-primary" ID="btnSim" runat="server" Text="Sim" OnClick="btnSim_Click" Width="100px" />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-danger" ID="btnNao" runat="server" Text="Não" OnClick="btnNao_Click" Width="100px" />
                                </div>

                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>

            <!-- /.box-footer-->
        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
