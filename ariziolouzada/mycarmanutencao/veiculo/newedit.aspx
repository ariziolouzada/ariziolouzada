<%@ Page Title="" Language="C#" MasterPageFile="~/mycarmanutencao/mycar.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.mycarmanutencao.veiculo.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>My Car Manutenção</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="../../mycarmanutencao/default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="../../mycarmanutencao/veiculo/default.aspx">Lista Veículos</a></li>
                        <li class="breadcrumb-item active">NewEdit Veículo</li>
                    </ol>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">

                    <!-- Default box -->
                    <div class="card card-info">
                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Literal ID="ltlCabecalho" runat="server" Text="Cadastro de Veículo"></asp:Literal>
                            </h3>
                        </div>
                        <div class="card-body">
                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-sm-6">
                                    <!-- text input -->
                                    <div class="form-group">
                                        <label>Marca / Modelo</label>
                                        <input type="text" class="form-control" placeholder="Marca/Modelo" runat="server" id="txtMarcaModelo" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Ano Fabricação/Modelo</label>
                                        <input type="text" class="form-control" placeholder="Ano Fabricação/Modelo" runat="server" id="txtAnoFabMod" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Placas</label>
                                        <input type="text" class="form-control" placeholder="Placas" runat="server" id="txtPlacas" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6">
                                    <!-- text input -->
                                    <div class="form-group">
                                        <label>Chassi</label>
                                        <input type="text" class="form-control" placeholder="Chassi" runat="server" id="txtChassi" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Cor</label>
                                        <input type="text" class="form-control" placeholder="Cor" runat="server" id="txtCor" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:Panel ID="pnlSituacao" runat="server">
                                            <label>Situação</label>
                                            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1">Ativo</asp:ListItem>
                                                <asp:ListItem Value="2">Inativo</asp:ListItem>
                                                <asp:ListItem Value="3">Excluído</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Data da Compra</label>
                                        <input type="date" class="form-control" runat="server" id="txtDataCompra" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Valor de Compra</label>
                                        <input type="text" class="form-control" placeholder="Valor Compra" runat="server" id="txtValorCompra" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Data da Venda</label>
                                        <input type="date" class="form-control" runat="server" id="txtDataVenda" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Valor de Venda</label>
                                        <input type="text" class="form-control" placeholder="Valor Venda" runat="server" id="txtValorVenda" />
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <!-- /.card-body -->
                    <div class="card-footer">
                        <div class="row">
                            <div class="col-2">
                                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-block btn-outline-success" OnClick="btnSalvar_Click" />
                            </div>
                            <div class="col-2">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-block btn-outline-danger" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.card -->

            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_scripts" runat="server">
</asp:Content>
