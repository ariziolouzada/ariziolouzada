<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ariziolouzada.ast.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>AST - Login</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
</head>
<body class="gray-bg">
    <form id="form1" runat="server" class="m-t">

        <div class="middle-box text-center loginscreen animated fadeInDown">
            <div class="col-sm-4"></div>
            <div class="col-sm-4">
                <div>

                    <%-- <h1 class="logo-name">IN+</h1>--%>
                    <img src="../img/logo_ast.jpg" width="200" />
                    <%--<img src="imagens/LOGO_PETROBRAS2.png" />--%>
                </div>
                <h3>Sistema da AST</h3>
                <p>Bem Vindo!!</p>
                <!-- <p>Login.</p>
                <form class="m-t" role="form" action="index.html">-->
                <div class="form-group">
                    <label>Login: </label>
                    <asp:TextBox ID="txtLogin" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Senha: </label>
                    <asp:TextBox ID="txtSenha" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="captcha">Informe os caracteres da imagem abaixo.</label>
                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Image ID="imgCaptcha" runat="server" ImageUrl="sistema/gerar_captcha.aspx" Height="50px" Width="120px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="login.aspx" class="btn btn-warning" data-original-title="" title="Atualizar caracteres da imagem."><i class="fa fa-refresh fa-lg"></i></a>
                </div>

                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" Text="Entre" CssClass="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" />
                </div>
                <div class="form-group">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
                <!--   <a href="#"><small>Forgot password?</small></a>
                <p class="text-muted text-center"><small>Do not have an account?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="register.html">Create an account</a>
            </form>-->
                <p class="m-t"><small>Assessoria em Segurança do Trabalho &copy; 2016</small> </p>
            </div>
            <div class="col-sm-4"></div>
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

    </script>

</body>
</html>
