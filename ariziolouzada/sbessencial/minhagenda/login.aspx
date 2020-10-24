<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ariziolouzada.sbessencial.minhagenda.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login Agenda Salão Essencial</title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">

            <div class="middle-box text-center loginscreen animated fadeInDown">
                <div>
                    <div>
                        <h1 class="logo-name">ES+</h1>

                    </div>
                    <h3>Bem Vindo!!<br />
                        Agenda do Essencial Salão</h3>
                    <p>
                        Entre a veja os horários disponíveis para o profissional que deseja ser atendido.
                <!--Continually expanded and constantly improved Inspinia Admin Them (IN+)-->
                    </p>
                    <p>Utilize o número do seu celular e a senha para acessar.</p>

                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Nº do Celular" required="" runat="server" id="txtLogin" onkeypress="mascara(this, mnumeros);" />
                    </div>
                    <div class="form-group">
                        <input type="password" class="form-control" placeholder="Senha" required="" runat="server" id="txtSenha" />
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" OnClientClick=" return validaCampos()" />

                    <div class="form-group">
                        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                    </div>

                    <p class="text-muted text-center"><small>Não possui uma conta?</small></p>
                    <a class="btn btn-sm btn-white btn-block" href="registro.aspx">Criar uma conta</a>
                    <%--<br />
                <a class="btn btn-sm btn-info btn-block" href="#">Esqueceu sua Senha?</a>--%>
                    <br />
                    <br />
                    <p class="m-t"><small>AAOL Sistemas - Todos Direitos Reservados &copy; 2018</small> </p>
                </div>
            </div>

        </asp:Panel>
    </form>

    <!-- Mainly scripts -->
    <script src="../js/jquery-2.1.1.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <script src="../js/mascaras.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById("txtLogin").setAttribute('autocomplete', 'off');
            //document.getElementById("txtCaptcha").setAttribute('autocomplete', 'off');


        });

        function validaCampos() {
            if (document.getElementById("<%=txtLogin.ClientID%>").value == "") {
                alert("Informe o Login (Nº de Celular).");
                document.getElementById("<%=txtLogin.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSenha.ClientID%>").value == "") {
                alert("Informe a Senha.");
                document.getElementById("<%=txtSenha.ClientID%>").focus();
                return false;
            }

<%--            if (document.getElementById("<%=txtCaptcha.ClientID%>").value == "") {
                alert("Informe as Letras do CAPTCHA.");
                document.getElementById("<%=txtCaptcha.ClientID%>").focus();
                return false;
            }--%>

            return true;
        }


    </script>

</body>
</html>
