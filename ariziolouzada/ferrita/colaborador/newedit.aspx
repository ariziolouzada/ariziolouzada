<%@ Page Title="" Language="C#" MasterPageFile="~/ferrita/ferrita.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.ferrita.colaborador.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Colaborador(a)       
                                <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="default.aspx"><i class="fa fa-user"></i>Colaboradores</a></li>
            <li class="active">NewEdit Colaborador</li>
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
                            <div class="col-md-4">
                                <label>Nome</label>
                                <input id="txtNome" runat="server" class="form-control" type="text" placeholder="Nome Colaborador(a)">
                            </div>
                            <div class="col-md-2">
                                <label>Sexo</label>
                                <asp:DropDownList ID="ddlSexo" runat="server" class="form-control">
                                    <asp:ListItem>Selecione...</asp:ListItem>
                                    <asp:ListItem Value="F">Mulher</asp:ListItem>
                                    <asp:ListItem Value="M">Homem</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label>CPF</label>
                                <input id="txtCpf" runat="server" class="form-control" type="text" placeholder="CPF Colaborador" maxlength="14" onkeypress="mascara(this, mcpf);">
                            </div>

                            <div class="col-md-2">
                                <label>Data Nascto.</label>
                                <input id="txtDatanascto" runat="server" class="form-control" type="date">
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

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label>Telefone</label>
                                    <input id="txtTelefone" runat="server" class="form-control" type="text" placeholder="telefones de contato">
                                </div>
                                <div class="col-md-4">
                                    <label>Email</label>
                                    <input id="txtEmail" runat="server" class="form-control" type="text" placeholder="Email">
                                </div>
                                <div class="col-md-4">
                                    <label>Facebook</label>
                                    <input id="txtFacebokk" runat="server" class="form-control" type="text" placeholder="Facebook">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label>CEP</label>
                                    <input id="txtEndCep" runat="server" class="form-control" type="text" placeholder="CEP" onkeypress="mascara(this, mcep);" onblur="buscaCep()" maxlength="10">
                                </div>
                                <div class="col-md-4">
                                    <label>Logradouro</label>
                                    <input id="txtEndLogradouro" runat="server" class="form-control" type="text" placeholder="Rua/Av./Beco/Escadaria...">
                                </div>
                                <div class="col-md-2">
                                    <label>Nº</label>
                                    <input id="txtEndNumero" runat="server" class="form-control" type="text" placeholder="Nº">
                                </div>
                                <div class="col-md-4">
                                    <label>Complemento</label>
                                    <input id="txtEndComplemento" runat="server" class="form-control" type="text" placeholder="Complemento">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group">

                                <div class="col-md-4">
                                    <label>Bairro</label>
                                    <input id="txtEndBairro" runat="server" class="form-control" type="text" placeholder="Bairro">
                                </div>
                                <div class="col-md-4">
                                    <label>Cidade</label>
                                    <input id="txtEndCidade" runat="server" class="form-control" type="text" placeholder="Cidade">
                                </div>
                                <div class="col-md-2">
                                    <label>UF</label>
                                    <input id="txtEndUf" runat="server" class="form-control" type="text" placeholder="UF">
                                </div>
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

        </div>
        <!-- /.box -->

    </section>
    <!-- /.content -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="../js/mascaras.js"></script>
    <%--<script src="../js/jquery.dataTables/jquery-3.2.1.min.js"></script>--%>

    <!-- Adicionando Javascript -->
    <script type="text/javascript">

        //$(document).ready(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            document.getElementById("<%=txtEndLogradouro.ClientID%>").value = "";
            document.getElementById("<%=txtEndLogradouro.ClientID%>").value = "";
            document.getElementById("<%=txtEndBairro.ClientID%>").value = "";
            document.getElementById("<%=txtEndCidade.ClientID%>").value = "";
            document.getElementById("<%=txtEndUf.ClientID%>").value = "";

            //$("#rua").val("");
            //$("#bairro").val("");
            //$("#cidade").val("");
            //$("#uf").val("");
            //$("#ibge").val("");
        }

        function buscaCep() {

            //Nova variável "cep" somente com dígitos.
            var cep = $(<%=txtEndCep.ClientID%>).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {
                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("<%=txtEndLogradouro.ClientID%>").val("...");
                    $("<%=txtEndBairro.ClientID%>").val("...");
                    $("<%=txtEndCidade.ClientID%>").val("...");
                    $("<%=txtEndUf.ClientID%>").val("...");
                    //$("#ibge").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                        debugger;
                        if (!("erro" in dados)) {
                            //Atualiza os campos com os valores da consulta.
                            document.getElementById("<%=txtEndLogradouro.ClientID%>").value = dados.logradouro;
                            document.getElementById("<%=txtEndLogradouro.ClientID%>").value = dados.logradouro;
                            document.getElementById("<%=txtEndBairro.ClientID%>").value = dados.bairro;
                            document.getElementById("<%=txtEndCidade.ClientID%>").value = dados.localidade;
                            document.getElementById("<%=txtEndUf.ClientID%>").value = dados.uf;

                            document.getElementById("<%=txtEndNumero.ClientID%>").focus();
                            //$("#ibge").val(dados.ibge);
                        } //end if.
                        else {
                            //CEP pesquisado não foi encontrado.
                            limpa_formulário_cep();
                            alert("CEP não encontrado.");
                        }
                    });
                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.

        }

        <%--    //Quando o campo cep perde o foco.
            $("<%=txtEndCep.ClientID%>").blur(function () {

                //Nova variável "cep" somente com dígitos.
                var cep = $(this).val().replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep != "") {
                    debugger;
                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    //Valida o formato do CEP.
                    if (validacep.test(cep)) {

                        //Preenche os campos com "..." enquanto consulta webservice.
                        $("<%=txtEndLogradouro.ClientID%>").val("...");
                        $("<%=txtEndBairro.ClientID%>").val("...");
                        $("<%=txtEndCidade.ClientID%>").val("...");
                        $("<%=txtEndUf.ClientID%>").val("...");
                        //$("#ibge").val("...");

                        //Consulta o webservice viacep.com.br/
                        $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("<%=txtEndLogradouro.ClientID%>").val(dados.logradouro);
                                $("<%=txtEndBairro.ClientID%>").val(dados.bairro);
                                $("<%=txtEndCidade.ClientID%>").val(dados.localidade);
                                $("<%=txtEndUf.ClientID%>").val(dados.uf);
                                //$("#ibge").val(dados.ibge);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                    } //end if.
                    else {
                        //cep é inválido.
                        limpa_formulário_cep();
                        alert("Formato de CEP inválido.");
                    }
                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    limpa_formulário_cep();
                }
        });

--%>

        //});

    </script>

</asp:Content>
