<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.ferrita._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Home       
                                <small>Site</small>
        </h1>
        <ol class="breadcrumb">
            <%-- <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Examples</a></li>--%>
            <li class="active">Home</li>
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
                <div style="text-align: center;">
                    <br />
                    <img src="img/ferri.principal.jpg" />

                </div>
            </div>

            <!-- /.box-footer-->
        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
