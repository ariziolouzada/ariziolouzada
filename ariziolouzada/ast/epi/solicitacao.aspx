<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="solicitacao.aspx.cs" Inherits="ariziolouzada.ast.epi.solicitacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitação de EPI</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="../css/animate.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

        <div class="middle-box text-center loginscreen animated fadeInDown">

            <div class="col-sm-12">
                <div class="form-group">
                    <img src="../imagens/LOGO_PETROBRAS2.png" />
                </div>
                <br />              
                <div class="form-group">
                    <h3>Sistema da AST</h3>
                    <p>Solicitação de EPI!!</p>
                </div>
                <br />
                <br />
                <div class="form-group">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
                <br />
                <br />
                <asp:Label ID="lblEnderecoIp" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <p class="m-t"><small>Assessoria em Segurança do Trabalho &copy; 2016</small> </p>
            </div>
        </div>

    </form>
</body>
</html>
