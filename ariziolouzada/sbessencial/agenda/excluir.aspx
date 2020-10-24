<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="excluir.aspx.cs" Inherits="ariziolouzada.sbessencial.agenda.excluir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">

     <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h4>
                                <asp:Label ID="lblCabecalho" runat="server" Text="Você confirma a exclusão deste Registro?"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-1 col-md-1 control-label"></label>
                                <div class="col-md-3 col-sm-3 ">                                   
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="NÃO" OnClick="btnCancelar_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="SIM" OnClick="btnSalvar_Click" />
                                </div>
                              
                                <label class="col-sm-8 col-md-8 control-label"></label>
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
