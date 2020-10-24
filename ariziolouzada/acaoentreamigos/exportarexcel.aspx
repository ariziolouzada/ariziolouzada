<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exportarexcel.aspx.cs" Inherits="ariziolouzada.acaoentreamigos.exportarexcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ação Entre Amigos-Tabela de Números</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
        <div>
            <asp:GridView ID="gwTabelaNumeros" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="numero" HeaderText="Numero" DataFormatString="{0:00000}">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="nomeComprador" HeaderText="Comprador" SortExpression="nomeComprador">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="telefoneComprador" HeaderText="Tel. Comprador" SortExpression="nomeComprador">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="emailComprador" HeaderText="Email Comprador" SortExpression="nomeComprador">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="dataVenda" HeaderText="Data da Venda" SortExpression="nomeComprador">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="nomeVendedor" HeaderText="Vendedor" SortExpression="nomeComprador">
                        <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
