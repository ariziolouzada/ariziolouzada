<%@ Page Title="" Language="C#" MasterPageFile="~/sbessencial/sbessencial.Master" AutoEventWireup="true" CodeBehind="alterarsenha.aspx.cs" Inherits="ariziolouzada.sbessencial.sistema.alterarsenha" %>

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
                                <asp:Label ID="lblCabecalho" runat="server" Text="Alterar Senha"></asp:Label></h4>
                        </div>
                        <div class="ibox-content">

                            <div class="form-group">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-sm-3 col-md-3 control-label">Senha Atual:</label>
                                <div class="col-sm-9 col-md-9">
                                    <input type="password" class="form-control" runat="server" placeholder="Digite a senha atual" id="txtSenhaAtual">
                                </div>
                            </div>

                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-3 col-md-3 control-label">Nova Senha:</label>
                                <div class="col-sm-9 col-md-9">
                                    <input type="password" class="form-control" placeholder="Digite a Nova Senha" runat="server" id="txtNovaSenha1">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <label class="col-sm-3 col-md-3 control-label">Confirma Nova Senha:</label>
                                <div class="col-sm-9 col-md-9">
                                    <input type="password" class="form-control" placeholder="Confirme a nova senha" runat="server" id="txtNovaSenha2" onchange="verificaSeCamposSenhasSaoIguais()">
                                </div>
                            </div>
                            <div class="hr-line-dashed"></div>

                            <div class="form-group">
                                <label class="col-sm-2 col-md-2 control-label"></label>
                                <div class="col-md-10 col-sm-10 ">
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

    
<script type="text/javascript">

    function verificaSeCamposSenhasSaoIguais() {
        debugger;
        var senha1 = document.getElementById("<%=txtNovaSenha1.ClientID%>").value;
        var senha2 = document.getElementById("<%=txtNovaSenha2.ClientID%>").value;

        //window.PageMethods.VerificaComplexibilidadeSenha(senha, onSucess, onError);

        if (senha1 != senha2) {
            document.getElementById(<%=ltlMsn.ClientID%>).innerHTML = "<div class='alert alert-danger fade in m-b-15'><strong>Error!</strong>  Os campos da nova senha não são iguais!<span class='close' data-dismiss='alert'>X</span></div>";

        }
    }


</script>

</asp:Content>
