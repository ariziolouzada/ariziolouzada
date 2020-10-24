<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ariziolouzada.sbessencial.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Salão Essencial ADMIN - Login</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

</head>

<body class="gray-bg">
    <form id="form1" runat="server" class="m-t">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="middle-box text-center loginscreen animated fadeInDown">
            <div class="col-sm-4"></div>
            <div class="col-sm-4 form-horizontal">
                <div>

                    <%-- <h1 class="logo-name">IN+</h1>--%>
                    <img src="../img/logo_salao_essencial.jpg" />
                    <%--<img src="imagens/LOGO_PETROBRAS2.png" />--%>
                </div>
                <h3>Administração</h3>
                <p>Bem Vindo!!</p>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="form-group">
                            <label class="col-sm-4 col-md-4 control-label">Login: </label>
                            <div class="col-sm-8 col-md-8">
                                <asp:TextBox ID="txtLogin" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-4 col-md-4 control-label">Senha: </label>
                            <div class="col-sm-8 col-md-8">
                                <asp:TextBox ID="txtSenha" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <asp:Panel ID="pnlCaptcha" runat="server" Visible="false">

                            <div class="form-group">
                                <label class="col-sm-8 col-md-8 control-label" for="captcha">Informe os caracteres da imagem.</label>
                                <div class="col-sm-4 col-md-4">
                                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 col-md-4 control-label"></label>
                                <div class="col-sm-8 col-md-8">
                                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/sbessencial/sistema/gerar_captcha.aspx" Height="40px" Width="120px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="login.aspx" class="btn btn-warning" data-original-title="" title="Atualizar caracteres da imagem."><i class="fa fa-refresh fa-lg"></i></a>

                                </div>
                            </div>

                        </asp:Panel>

                        <div class="form-group">
                            <label class="col-sm-4 col-md-4 control-label"></label>
                            <div class="col-sm-8 col-md-8">
                                <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary btn-rounded btn-block" OnClick="btnLogin_Click" OnClientClick=" return validaCampos()" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12 col-md-12">
                                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            </div>
                        </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>

                <p class="m-t"><small>Arizio Aguilar Oliveira Louzada &copy; 2018</small> </p>
            </div>
            <div class="col-sm-4 col-md-4"></div>
        </div>

        <!-- Mainly scripts -->
        <script src="../js/jquery-2.1.1.js"></script>
        <script src="../js/bootstrap.min.js"></script>

    </form>

    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById("txtLogin").setAttribute('autocomplete', 'off');
            document.getElementById("txtCaptcha").setAttribute('autocomplete', 'off');

            /* 
                       $('#txtLogin').attr('autocomplete', 'off');
                       $('#txtSenha').attr('autocomplete', 'off');
               */
        });

        function validaCampos() {
            if (document.getElementById("<%=txtLogin.ClientID%>").value == "") {
                alert("Informe o Login.");
                document.getElementById("<%=txtLogin.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSenha.ClientID%>").value == "") {
                alert("Informe a Senhan.");
                document.getElementById("<%=txtSenha.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtCaptcha.ClientID%>").value == "") {
                alert("Informe as Letras do CAPTCHA.");
                document.getElementById("<%=txtCaptcha.ClientID%>").focus();
                return false;
            }

            return true;
        }


    </script>

</body>
</html>
