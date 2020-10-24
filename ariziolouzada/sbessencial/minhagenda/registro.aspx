<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="ariziolouzada.sbessencial.minhagenda.registro" %>

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
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRegistrar">

        <div class="middle-box text-center loginscreen animated fadeInDown">
            <div>
                <div>
                    <h1 class="logo-name">ES+</h1>

                </div>
                <h3>Agenda do Essencial Salão</h3>
                <p>
                    Preencha os campos abaixo para o cadastro.
                <!--Continually expanded and constantly improved Inspinia Admin Them (IN+)-->
                </p>
                <%--<p>Utilize o número do seu celular e a senha para acessar.</p>--%>

                <div class="form-group">
                    <input type="text" class="form-control" placeholder="Nome" required="" runat="server" id="txtNome" />
                </div>
                <div class="form-group">
                    <input type="text" class="form-control" placeholder="Nº do Celular" required="" runat="server" id="txtLogin" onkeypress="mascara(this, mnumeros);" />
                </div>
                <div class="form-group">
                    <input type="text" class="form-control" placeholder="Email" required="" runat="server" id="txtEmail" />
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" placeholder="Senha" required="" runat="server" id="txtSenha" />
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" placeholder="Confirma a Senha" required="" runat="server" id="txtSenhaConfirma" />
                </div>


                <div class="form-group">
                    <label class="col-sm-7 col-md-7 control-label" for="captcha">Informe os caracteres da imagem.</label>
                    <div class="col-sm-5 col-md-5">
                        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <%--<label class="col-sm-4 col-md-4 control-label"></label>--%>
                    <div class="col-sm-12 col-md-12">
                        <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/sbessencial/sistema/gerar_captcha.aspx" Height="40px" Width="120px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="registro.aspx" class="btn btn-warning" data-original-title="" title="Atualizar caracteres da imagem."><i class="fa fa-refresh fa-lg"></i></a>

                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />

                <div class="form-group">
                    <asp:Button ID="btnRegistrar" runat="server" Text="Entrar" CssClass="btn btn-primary block full-width m-b" OnClick="btnRegistrar_Click" OnClientClick="return validaCampos()" />
                </div>
                <br />
                <br />
                <div class="form-group">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
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

            document.getElementById("<%=txtNome.ClientID%>").setAttribute('autocomplete', 'off');
            document.getElementById("<%=txtLogin.ClientID%>").setAttribute('autocomplete', 'off');
            document.getElementById("<%=txtEmail.ClientID%>").setAttribute('autocomplete', 'off');

        });

        function IsEmail(email) {
            var exclude = /[^@-.w]|^[_@.-]|[._-]{2}|[@.]{2}|(@)[^@]*1/;
            var check = /@[w-]+./;
            var checkend = /.[a-zA-Z]{2,3}$/;
            if (((email.search(exclude) != -1) || (email.search(check)) == -1) || (email.search(checkend) == -1)) { return false; }
            else { return true; }
        }

        function validaCampos() {

            if (document.getElementById("<%=txtNome.ClientID%>").value == "") {
                alert("Informe o NOME.");
                document.getElementById("<%=txtNome.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtLogin.ClientID%>").value == "") {
                alert("Informe o Nº do seu Celular.");
                document.getElementById("<%=txtLogin.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Informe o seu EMAIL.");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            debugger;

            if (IsEmail(document.getElementById("<%=txtEmail.ClientID%>").value) == false) {
                alert("EMAIL INVÁLIDO.");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSenha.ClientID%>").value == "") {
                alert("Informe a Senha.");
                document.getElementById("<%=txtSenha.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtSenhaConfirma.ClientID%>").value == "") {
                alert("Confirme a Senha.");
                document.getElementById("<%=txtSenhaConfirma.ClientID%>").focus();
                return false;
            }

            var senha1 = document.getElementById("<%=txtSenha.ClientID%>").value;
            var senha2 = document.getElementById("<%=txtSenhaConfirma.ClientID%>").value;
            if (senha1 != senha2) {
                alert("As Senhas não conferem.");
                document.getElementById("<%=txtSenhaConfirma.ClientID%>").focus();
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
