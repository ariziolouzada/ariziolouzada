<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="neweditcomanda.aspx.cs" Inherits="ariziolouzada.ferrita.colaborador.neweditcomanda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Comanda Colaborador       
                                <small>
                                    <asp:Label ID="lblNomeColaborador" runat="server" Text="Label"></asp:Label>
                                </small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="listacmdacolab.aspx"><i class="fa fa-user"></i>Lista Cmdas</a></li>
            <li class="active">NewEdit Cmda Colaborador</li>
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
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>Nº Comanda</label>
                                    <input id="txtNumeroCmda" runat="server" class="form-control" type="text" disabled="" />
                                </div>
                                <div class="col-md-2">
                                    <label>Data</label>
                                    <input id="txtData" runat="server" class="form-control" type="date" />
                                </div>
                                <div class="col-md-2">
                                    <label>Valor Total</label>
                                    <input id="txtValorTotal" runat="server" class="form-control" type="text" disabled="" />
                                </div>

<%--                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">--%>
                                <div class="col-md-3">
                                    <label>Forma Pgto</label>
                                    <asp:DropDownList ID="ddlFormaPgto" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <br />
                                    <asp:Button class="btn btn-success" ID="btnAdicionarProduto" runat="server" Text="Adicionar Produto" OnClick="btnAdicionarProduto_Click" />
                                </div>
                                <asp:HiddenField ID="hdfIdColaborador" runat="server" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">

                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar Cmda" Width="100px" OnClick="btnSalvar_Click" />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <asp:Button class="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" Width="100px" OnClick="btnCancelar_Click" />
                                </div>
                                
                                <asp:Panel ID="pnlStatus" runat="server">
                                    <div class="col-md-2">
                                        <label>Situação</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                            <asp:ListItem>Selecione...</asp:ListItem>
                                            <asp:ListItem Value="1">Em aberto</asp:ListItem>
                                            <asp:ListItem Value="2">Pendente</asp:ListItem>
                                            <asp:ListItem Value="3">Fechada</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>


                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>

                        <asp:Panel ID="pnlProdutosSelecionados" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlAdicionarProduto" runat="server" Visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md-3">
                                        <label>Descrição</label>
                                        <asp:DropDownList ID="ddlProduto" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <%--                                    <div class="col-md-1">
                                        <label>Código</label>
                                        <input id="txtCodigoProd" runat="server" class="form-control" type="text" />
                                    </div>--%>

                                    <asp:Panel ID="pnlQtdeUnica" runat="server">
                                        <div class="col-md-2">
                                            <label>QTDE</label>
                                            <input id="txtQtdeUnica" runat="server" class="form-control" type="text" placeholder="Qtde">
                                            <asp:HiddenField ID="hdfTamUnico" runat="server" />
                                            <asp:HiddenField ID="hdfQtdeProduto" runat="server" />
                                            <asp:HiddenField ID="hdfValorunitario" runat="server" />
                                            <asp:HiddenField ID="hdfValorTotal" runat="server" />

                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlQtdes" runat="server" Visible="false">
                                        <div class="col-md-1">
                                            <label>Qtd P</label>
                                            <input id="txtQtdTamP" runat="server" class="form-control" type="text" placeholder="Qtde Tam P">
                                        </div>
                                        <div class="col-md-1">
                                            <label>Qtd M</label>
                                            <input id="txtQtdTamM" runat="server" class="form-control" type="text" placeholder="Qtde Tam M">
                                        </div>
                                        <div class="col-md-1">
                                            <label>Qtd G</label>
                                            <input id="txtQtdTamG" runat="server" class="form-control" type="text" placeholder="Qtde Tam G">
                                        </div>
                                        <div class="col-md-1">
                                            <label>Qtd GG</label>
                                            <input id="txtQtdTamGG" runat="server" class="form-control" type="text" placeholder="Qtde Tam GG">
                                        </div>
                                        <div class="col-md-1">
                                            <label>Qtd EG</label>
                                            <input id="txtQtdTamEG" runat="server" class="form-control" type="text" placeholder="Qtde Tam EG">
                                        </div>

                                    </asp:Panel>


                                    <div class="col-md-1">
                                        <label></label>
                                        <asp:Button ID="BtnAddProdSel" runat="server" Text="Salvar" class="btn btn-block btn-success" OnClick="btnAddProdSel_Click" />
                                    </div>

                                    <div class="col-md-1">
                                        <label></label>
                                        <asp:Button ID="btnCancelarAddProdSel" runat="server" Text="Cancelar" class="btn btn-danger" OnClick="btnCancelarAddProdSel_Click"/>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>



                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>
        <!-- /.box -->



        <div class="modal modal-danger fade" id="modal-danger">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Excluir Produto Selecionado.</h4>
                    </div>
                    <div class="modal-body">
                        <p>Você realmente deseja excluir o produto selecionado?</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Não</button>
                        <asp:Button ID="btnExcluirProdSel" runat="server" Text="Sim" class="btn btn-outline" OnClick="btnExcluirProduto_Click" />
                        <%--<button type="button" class="btn btn-outline" id="btnExcluirProduto" runat="server"  OnClick="btnExcluirProduto_Click">Sim</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">


    <script>

        function capturarId(id, acao) {
            debugger;
            window.PageMethods.CapturarId(id, acao, onSucess, onError);

            function onSucess(result) {

                if (result != '')
                    document.getElementById("<%=ltlMsn.ClientID%>").innerHTML = result;
            }

            function onError(result) {
                alert('capturarId-Erro: Contate o Administrador!!');
            }
        }

        function excluirId() {

            window.PageMethods.ExcluirId(onSucess, onError);
            debugger;
            function onSucess(result) {
                debugger;
                if (result != '') {

                    var arrayPar = result;
                    var param = arrayPar.split(';');
                    window.location.href = 'default.aspx?dia=' + param[0];
                }

            }

            function onError(result) {
                alert('excluirId-Erro: Contate o Administrador!!');
            }
        }


    </script>


</asp:Content>
