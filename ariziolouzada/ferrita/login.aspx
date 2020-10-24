<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ariziolouzada.ferrita.login" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ferrita Lingerie</title>

    <link id="Link1" href="img/ferrita.ico" rel="shortcut icon" type="image/x-icon" />
    <link id="Link2" href="img/ferrita.ico" rel="icon" type="image/ico" />

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="plugins/iCheck/square/blue.css" />
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">

        <div class="login-box">
            <div class="login-logo">
                <%-- <a href="#"><b>Ferrita</b> Lingerie</a>--%>
                <img src="img/ferri.principal_compactada.jpg" />
            </div>
            <!-- /.login-logo -->
            <div class="login-box-body">
                <p class="login-box-msg">Entre para iniciar sua sessão.</p>

                <div class="form-group has-feedback">
                    <input type="email" class="form-control" placeholder="Email" id="txtEmail" runat="server" />
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" class="form-control" placeholder="Senha" id="txtSenha" runat="server" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <%-- <div class="checkbox icheck">
                            <label>
                                <input type="checkbox">
                                Remember Me
           
                            </label>
                        </div>--%>
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button ID="btnEntrar" runat="server" Text="Entrar"  OnClick="btnEntrar_Click"  class="btn btn-primary btn-block btn-flat"/>
                        <%--<button type="submit" class="btn btn-primary btn-block btn-flat">Entrar</button>--%>
                    </div>
                    <!-- /.col -->
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                    </div>
                </div>

                <%-- <div class="social-auth-links text-center">
                    <p>- OR -</p>
                    <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i>Sign in using
        Facebook</a>
                    <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i>Sign in using
        Google+</a>
                </div>
                <!-- /.social-auth-links -->

                <a href="#">I forgot my password</a><br>
                <a href="register.html" class="text-center">Register a new membership</a>--%>
            </div>
            <!-- /.login-box-body -->
        </div>
        <!-- /.login-box -->

    </form>

    <!-- jQuery 3 -->
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' /* optional */
            });
        });
</script>

</body>
</html>

