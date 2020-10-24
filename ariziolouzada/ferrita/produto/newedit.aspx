<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.ferrita.produto.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Produto       
                                <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="default.aspx"><i class="fa fa-list-alt"></i>Produtos</a></li>
            <li class="active">NewEdit Produto</li>
        </ol>
    </section>


    <!-- Main content -->
    <section class="content">

        <!-- Default box -->
        <div class="box">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <asp:Label ID="lblTitulo" runat="server" Text="Cadastro"></asp:Label>
                </h3>
            </div>
            <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-12">
                                <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <label>Código</label>
                                <input id="txtCodigo" runat="server" class="form-control" type="text" placeholder="Código">
                            </div>
                            <div class="col-md-6">
                                <label>Descrição</label>
                                <input id="txtDescricao" runat="server" class="form-control" type="text" placeholder="Descrição">
                            </div>
                            <div class="col-md-4">
                                <label>Fornecedor</label>
                                <asp:DropDownList ID="ddlFornecedor" runat="server" class="form-control">
                                    <%--<asp:ListItem Value="0">Selecione...</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Preço Custo R$</label>
                                    <input id="txtPrecoCusto" runat="server" class="form-control" type="text" placeholder="Preço de Custo" onchange="calculaPrecoVenda()">
                                </div>

                                <div class="col-md-2">
                                    <label>Margem Lucro %</label>
                                    <input id="txtMargemLucro" runat="server" class="form-control" type="text" placeholder="Margem Lucro" onchange="calculaPrecoVenda()">
                                </div>

                                <div class="col-md-2">
                                    <label>Lucro R$</label>
                                    <input id="txtLucro" runat="server" class="form-control" type="text" placeholder="Lucro">
                                </div>

                                <div class="col-md-2">
                                    <label>Preço Venda R$</label>
                                    <input id="txtPrecoVenda" runat="server" class="form-control" type="text" placeholder="Preço de Vanda" onchange="calculaMargemLucro()">
                                </div>

                                <asp:Panel ID="pnlStatus" runat="server">
                                    <div class="col-md-2">
                                        <label>Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem>Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="2">Inativo</asp:ListItem>
                                            <asp:ListItem Value="3">Excluído</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Tamanho único</label>
                                    <asp:DropDownList ID="ddlEstoqueUnico" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstoqueUnico_SelectedIndexChanged">
                                        <asp:ListItem>SIM</asp:ListItem>
                                        <asp:ListItem>NÃO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <asp:Panel ID="pnlQtdeUnica" runat="server">
                                    <div class="col-md-2">
                                        <label>QTDE</label>
                                        <input id="txtQtdeunica" runat="server" class="form-control" type="text" placeholder="Qtde">
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlQtdes" runat="server" Visible="false">
                                    <div class="col-md-2">
                                        <label>Qtde Tam P</label>
                                        <input id="txtQtdTamP" runat="server" class="form-control" type="text" placeholder="Qtde Tam P">
                                    </div>
                                    <div class="col-md-2">
                                        <label>Qtde Tam M</label>
                                        <input id="txtQtdTamM" runat="server" class="form-control" type="text" placeholder="Qtde Tam M">
                                    </div>
                                    <div class="col-md-2">
                                        <label>Qtde Tam G</label>
                                        <input id="txtQtdTamG" runat="server" class="form-control" type="text" placeholder="Qtde Tam G">
                                    </div>
                                    <div class="col-md-2">
                                        <label>Qtde Tam GG</label>
                                        <input id="txtQtdTamGG" runat="server" class="form-control" type="text" placeholder="Qtde Tam GG">
                                    </div>
                                    <div class="col-md-2">
                                        <label>Qtde Tam EG</label>
                                        <input id="txtQtdTamEG" runat="server" class="form-control" type="text" placeholder="Qtde Tam EG">
                                    </div>

                                </asp:Panel>

                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" Width="100px" />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Width="100px" />
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
            <!-- /.box-body -->
            <%--<div class="box-footer">
                Footer       
            </div>--%>
            <!-- /.box-footer-->
        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../js/mascaras.js"></script>

    <script>

        function calculaPrecoVenda() {
            debugger;

            var pcCusto = document.getElementById("ContentPlaceHolder1_txtPrecoCusto").value;
            if (pcCusto != '') {
                pcCusto = document.getElementById("ContentPlaceHolder1_txtPrecoCusto").value;
            } else {
                pcCusto = '0';
            }

            var mgLucro = document.getElementById("ContentPlaceHolder1_txtMargemLucro").value;
            if (mgLucro != '') {
                mgLucro = document.getElementById("ContentPlaceHolder1_txtMargemLucro").value;
            } else {
                mgLucro = '0';
            }

            window.PageMethods.CalculaPrecoVenda(pcCusto, mgLucro, onSucess, onError);

            function onSucess(result) {
                debugger;

                var arrayResult = result.split("|");
                //if (result != '')
                document.getElementById("<%=txtLucro.ClientID%>").value = arrayResult[0];
                document.getElementById("<%=txtPrecoVenda.ClientID%>").value = arrayResult[1];
            }

            function onError(result) {
                alert('calculaPrecoVenda-Erro: Contate o Administrador!!');
            }

        }

        function calculaMargemLucro() {

            var pcCusto = document.getElementById("<%=txtPrecoCusto.ClientID%>").value;
            var pcVenda = document.getElementById("<%=txtPrecoVenda.ClientID%>").value
            
            window.PageMethods.CalculaMargemLucro (pcCusto, pcVenda, onSucess, onError);

            function onSucess(result) {
                debugger;

                var arrayResult = result.split("|");
                //if (result != '')
                document.getElementById("<%=txtLucro.ClientID%>").value = arrayResult[0];
                document.getElementById("<%=txtMargemLucro.ClientID%>").value = arrayResult[1];
            }

            function onError(result) {
                alert('calculaMargeLucro-Erro: Contate o Administrador!!');
            }
        }

    </script>

</asp:Content>
