<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.acaoentreamigos._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="Arizio Aguilar Oliveira Louzada" />

    <title>Ação Entre Amigos</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet"/>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

</head>
<body class="gray-bg">
    <form id="form1" runat="server">

        <div class=" text-center animated fadeInDown">
                <h1><b>Ação Entre Amigos</b></h1>
            <a href="login.aspx" title="Acessar área ADM.">LOGIN</a>
            <h3 class="font-bold">NÚMEROS <span style="color: green">DISPONÍVEIS</span> | <span style="color: red">JÁ VENDIDOS</span> </h3>
            
            <div class="row">
                <div class="form-group">
                    <label class="col-sm-2 control-label text-right">Filtrar:</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlFiltroNumeros" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroNumeros_SelectedIndexChanged">
                            <asp:ListItem Value="2">Selecione</asp:ListItem>
                            <asp:ListItem Value="-1">Todos</asp:ListItem>
                            <asp:ListItem Value="0">Disponíveis</asp:ListItem>
                            <asp:ListItem Value="1">Vendidos</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </div>
                </div>
            </div>

            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>

            <div class="table-responsive">
                <%--<table>
                    <tr>
                        <td>121</td>
                        <td style="color: red; font-weight: bold;">3213</td>
                        <td>3</td>
                    </tr>
                </table>--%>

                <asp:Literal ID="ltlTabelaNumeros" runat="server"></asp:Literal>
            </div>
        </div>



    </form>


    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <script src="js/plugins/dataTables/datatables.min.js"></script>

</body>
</html>
