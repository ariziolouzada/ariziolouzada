<%@ Page Title="" Language="C#" MasterPageFile="~/espacodebeleza/espacobeleza.Master" AutoEventWireup="true" CodeBehind="colaborador.aspx.cs" Inherits="ariziolouzada.espacodebeleza.pages.financeiro.colaborador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="form-group">
                <div class="col-xs-8 col-sm-10 col-md-10 col-lg-10">
                    <asp:Literal ID="ltlCabecalho" runat="server"></asp:Literal>
                    <%--<h2>Agenda</h2>--%>
                </div>
                <div class="col-xs-4 col-sm-2 col-md-2 col-lg-2">
                </div>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <%--<div class="ibox-title">
                            <h5>Lista de Clientes</h5>
                        </div>--%>
                        <div class="ibox-content">

                            <div class="form-group">
                                <label class="col-xs-2 col-sm-2 col-md-2 control-label">Data Inicial:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="date" class="form-control" runat="server" id="txtDatainicial" maxlength="10" />
                                </div>

                                <label class="col-xs-2 col-sm-2 col-md-2 control-label">Data Final:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <input type="date" class="form-control" runat="server" id="txtDataFinal" maxlength="10" />
                                </div>

                                <label class="col-xs-1 col-sm-1 col-md-1 control-label">Profissional:</label>
                                <div class="col-xs-2 col-sm-2  col-md-2">
                                    <asp:DropDownList ID="ddlProfissao" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                                <div class="col-xs-1 col-sm-1 col-md-1">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Carregar" class="btn btn-success btn-rounded" OnClick="btnFiltrar_Click" />  <%--&nbsp;Carregar &nbsp;Criar--%>
                                    <%--<a class="btn btn-success btn-rounded btn-outline" href="#" onclick="carregarAgenda();" title="Carregar Entradas."><i class="fa fa-play fa-2x"></i></a>--%>
                                </div>

                            </div>
                            <br />
                            <div class="hr-line-dashed"></div>

                            <div class="table-responsive">
                                <asp:Literal ID="ltlTabela" runat="server"></asp:Literal>
                                <div id="tabelaAgenda"></div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class=" col-md-12 col-lg-12">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                </div>
            </div>
        </div>


        <div class="modal inmodal" id="myModal6" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content animated flipInY">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>--%>
                        <h4 class="modal-title">Agenda</h4>
                    </div>
                    <div class="modal-body">
                        <div id="dadosAgenda"></div>
                        <asp:HiddenField ID="hdfIdProfissional" runat="server" />
                        <asp:HiddenField ID="hdfIdHoraInicial" runat="server" />
                        <asp:HiddenField ID="hdfIdServico" runat="server" />

                        <%--<div class="form-group">
                            <label class="control-label">Profissional </label>
                            <input type="text" class="form-control" id="txtProfAgenda" disabled="" value="" />
                        </div>
                        <div class="form-group">
                            <label class="control-label">Horário Inicial </label>
                            <input type="text" class="form-control" id="txtHoraInicialAgenda" disabled="" value="" />
                        </div>
                        <div class="form-group">
                            <label class="control-label">Horário Final </label>
                            <select class="form-control" id="ddlHoraFinal">
                                <option value="0">option 1</option>
                                <option value="0">option 2</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Serviço </label>
                            <select class="form-control" id="ddlServico">
                                <option value="0">option 1</option>
                                <option value="0">option 2</option>
                            </select>
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <a href="../cliente/newedit.aspx?id=X5A1oqTnjBE=" class="btn btn-warning"><i class="fa fa-plus-square"></i>&nbsp;Cliente</a>
                        <button type="button" class="btn btn-white" data-dismiss="modal">Fechar</button>
                        <button type="button" class="btn btn-primary" onclick="salvarDadosAgenda();">Salvar</button>
                        <%--<asp:Button ID="btnSalvarComissao" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvarComissao_Click" />--%>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal inmodal fade" id="myModal7" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only"></span></button>--%>
                        <h4 class="modal-title">Agenda</h4>
                    </div>
                    <div class="modal-body">
                        <div id="dadosAgenda2"></div>
                        <br />
                        <br />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-outline" data-dismiss="modal" title="Fechar"><i class="fa fa-times fa-2x"></i></button>
                        <%--<div id="pnlBtnsModal" style="display:block;">   demo3   --%>
                        <button id="btnModalApagar" class="btn btn-outline btn-warning " onclick="apagarDadosAgenda();" title="Apagar" ><i class="fa fa-eraser fa-2x"></i></button>
                        <button id="btnModalFinalizar" type="button" class="btn btn-outline btn-success" onclick="finalizarAgenda();" title="Confirmar"><i class="fa fa-check fa-2x"></i></button>
                        <%--</div>--%>
                        <asp:HiddenField ID="hdfIdAgendaApagar" runat="server" />
                        <%--<button type="button" class="btn btn-danger" onclick="salvarDadosAgenda();">Cancelar</button>
                        <asp:Button ID="btnSalvarComissao" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvarComissao_Click" />--%>
                    </div>
                </div>
            </div>
        </div>


        <div id="loader" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<h4 class="modal-title"> </h4>--%>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body ">

                        <div class="ibox ">
                            <div class="ibox-title">
                                <h2>Aguarde ...</h2>
                            </div>
                            <div class="ibox-content">
                                <div class="spiner-example">
                                    <div class="sk-spinner sk-spinner-wave">
                                        <div class="sk-rect1"></div>
                                        <div class="sk-rect2"></div>
                                        <div class="sk-rect3"></div>
                                        <div class="sk-rect4"></div>
                                        <div class="sk-rect5"></div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--<div class="loader centered"></div>--%>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
