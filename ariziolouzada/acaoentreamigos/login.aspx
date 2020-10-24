<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ariziolouzada.acaoentreamigos.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="Arizio Aguilar Oliveira Louzada" />

    <title>Ação entre amigos - Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <%--    <link id="Link1" rel="shortcut icon" href="imagens/female-hair-shape-and-face-silhouetteIcone.png" type="image/x-icon" />
    <link id="Link2" rel="icon" href="imagens/female-hair-shape-and-face-silhouetteIcone.png" type="image/ico" />--%>

    <style>
        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #1f5688; /* Blue */
            border-radius: 50%;
            width: 60px;
            height: 60px;
            animation: spin 2s linear infinite;
            margin: auto;
            padding: 10px;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>

</head>

<body class="gray-bg">
    <form id="form1" runat="server" class="m-t">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="middle-box text-center loginscreen animated fadeInDown">           
            <div>                
                <h2><b>Ação Enter Amigos</b></h2>
                <p>Acesso a Área Administrativa!!</p>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="1">Vendedor</asp:ListItem>
                                <asp:ListItem Value="2">Administrador</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        <%--<div class="form-group">
                            <input type="text" id="txtLogin" class="form-control" placeholder="LOGIN" runat="server" required="" />
                        </div>--%>
                        <div class="form-group">
                            <input type="password" id="txtSenha" class="form-control" placeholder="SENHA" runat="server" required="" />
                        </div>

                        <div class="form-group">
                            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary btn-rounded btn-block" OnClick="btnLogin_Click" OnClientClick=" return validaCampos()" />
                        </div>

                        <div class="form-group">
                            <a href="default.aspx" title="Ir para Página Inicial">Home Page</a>
                        </div>
                        <div class="form-group">
                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            <%--<asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar email" OnClick="btnEnviarEmail_Click"/>--%>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <%--<p class="m-t"><small>Arizio Aguilar Oliveira Louzada &copy; 2020</small> </p>--%>
            </div>

            <%--   <div class="col-sm-4 col-md-4 col-lg-4"></div>--%>
        </div>


        <div id="loader" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Aguarde ... </h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">
                        <div class="loader centered"></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Mainly scripts -->
        <script src="js/jquery-2.1.1.js"></script>
        <script src="js/bootstrap.min.js"></script>

    </form>

    <script type="text/javascript">

        $(document).ready(function () {
            //document.getElementById("txtLogin").setAttribute('autocomplete', 'off');
            //document.getElementById("txtCaptcha").setAttribute('autocomplete', 'off');

            /* 
                       $('#txtLogin').attr('autocomplete', 'off');
                       $('#txtSenha').attr('autocomplete', 'off');
               */
        });

        function validaCampos() {

            debugger;
            if (document.getElementById("<%=ddlTipoUsuario.ClientID%>").value == "0") {
                alert("Selecione o tipo de usuário.");
                document.getElementById("<%=ddlTipoUsuario.ClientID%>").focus();
                return false;
            }
            
<%--            if (document.getElementById("<%=txtLogin.ClientID%>").value == "") {
                alert("Informe o Login.");
                document.getElementById("<%=txtLogin.ClientID%>").focus();
                return false;
            }--%>
        
            if (document.getElementById("<%=txtSenha.ClientID%>").value == "") {
                alert("Informe a Senha.");
                document.getElementById("<%=txtSenha.ClientID%>").focus();
                return false;
            }

            //$('#loader').modal('show');

            return true;
        }

        function fecharModal() {
            $('#loader').modal('hide');
        }

    </script>

</body>
</html>
