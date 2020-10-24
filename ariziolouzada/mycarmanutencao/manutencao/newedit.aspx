<%@ Page Title="" Language="C#" MasterPageFile="~/mycarmanutencao/mycar.Master" AutoEventWireup="true" CodeBehind="newedit.aspx.cs" Inherits="ariziolouzada.mycarmanutencao.manutencao.newedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <!-- Toastr -->
    <link rel="stylesheet" href="../plugins/toastr/toastr.min.css">
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
                        <li class="breadcrumb-item"><a href="../../mycarmanutencao/manutencao/default.aspx">Lista de Manutenções</a></li>
                        <li class="breadcrumb-item active">NewEdit Manutenção</li>
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
                                <asp:Literal ID="ltlCabecalho" runat="server" Text="Cadastro de Manutenção"></asp:Literal>
                            </h3>
                        </div>
                        <div class="card-body">
                            <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                            <div class="row">
                                <div class="col-sm-4">
                                    <!-- text input -->
                                    <div class="form-group">
                                        <label>Data</label>
                                        <input type="date" class="form-control" placeholder="Data" runat="server" id="txtData" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>KM</label>
                                        <input type="number" class="form-control" runat="server" id="txtKm" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Valor</label>
                                        <input type="text" class="form-control" placeholder="Valor" runat="server" id="txtValor" autocomplete="off" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- text input -->
                                    <div class="form-group">
                                        <label>Descrição</label>
                                        <textarea class="form-control" rows="5" placeholder="Descrição da manutenção" runat="server" id="txtDescricao"></textarea>
                                        <%--<input type="text" class="form-control" placeholder="Chassi" runat="server" id="txtChassi">--%>
                                        <asp:HiddenField ID="hdfIdMyCar" runat="server" />
                                        <asp:HiddenField ID="hdfIdManutencao" runat="server" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- /.card-body -->
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-2">
                                    <a href="#" class="btn btn-block btn-success " onclick="verificaCampos()">Salvar</a>
                                    <%--<button type="button" class="btn btn-success toastsDefaultSuccess">Salvar</button>--%>
                                </div>
                                <%-- <div class="col-2">
                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-block btn-outline-success" OnClick="btnSalvar_Click" />
                                </div>--%>
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
    <!-- Toastr -->
    <script src="../plugins/toastr/toastr.min.js"></script>

    <script type="text/javascript">

        function salvarDados() {
            debugger;

            var valor = document.getElementById("<%=txtValor.ClientID%>").value;
            valor = valor.replace("R$","");
            valor = valor.replace(".","");
            valor = valor.replace(",",".");
            valor = valor.trim();
            var dados = {
                Id: document.getElementById("<%=hdfIdManutencao.ClientID%>").value
                , IdMyCar: document.getElementById("<%=hdfIdMyCar.ClientID%>").value
                , Data: document.getElementById("<%=txtData.ClientID%>").value
                , Descricao: document.getElementById("<%=txtDescricao.ClientID%>").value
                , Km: document.getElementById("<%=txtKm.ClientID%>").value
                , Valor: valor
            };

            var dadosJson = JSON.stringify(dados);
            window.PageMethods.SalvarDados(dadosJson, onSucess, onError);
            function onSucess(result) {
                debugger;
                if (result === 'saveOk') {
                    toastr.success('Dados salvos com sucesso!!');
                    limparCampos();
                } else {
                    msnErro(result);
                }
            }
            function onError(result) {
                debugger;
                msnErro('Erro javascript: Procure o Administrador!!');
            }

        }

        function verificaCampos() {
            //debugger;
            var camposOk = true;
            if (document.getElementById("<%=txtData.ClientID%>").value === "") {
                msnErro('Campo DATA obrigatório!!');
                document.getElementById("<%=txtData.ClientID%>").focus();
                camposOk = false;

            } else if (document.getElementById("<%=txtKm.ClientID%>").value === "") {
                msnErro('Campo KM obrigatório!!');
                document.getElementById("<%=txtKm.ClientID%>").focus();
                camposOk = false;
            } else if (document.getElementById("<%=txtValor.ClientID%>").value === "") {
                msnErro('Campo VALOR obrigatório!!');
                document.getElementById("<%=txtValor.ClientID%>").focus();
                camposOk = false;
            } else if (document.getElementById("<%=txtDescricao.ClientID%>").value === "") {
                msnErro('Campo DESCRICÃO obrigatório!!');
                document.getElementById("<%=txtDescricao.ClientID%>").focus();
                camposOk = false;
            }


            if (camposOk) {
                salvarDados();
            }
        }

        function msnErro(texto) {
            toastr.error(texto);
        }

        function limparCampos() {
            document.getElementById("<%=txtData.ClientID%>").value = "";
            document.getElementById("<%=txtKm.ClientID%>").value = "";
            document.getElementById("<%=txtValor.ClientID%>").value = "";
            document.getElementById("<%=txtDescricao.ClientID%>").value = "";
            document.getElementById("<%=txtData.ClientID%>").focus();
        }

        $(function () {
            /*
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000
            });

            $('.swalDefaultSuccess').click(function () {
                Toast.fire({
                    icon: 'success',
                    title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.swalDefaultInfo').click(function () {
                Toast.fire({
                    icon: 'info',
                    title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.swalDefaultError').click(function () {
                Toast.fire({
                    icon: 'error',
                    title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.swalDefaultWarning').click(function () {
                Toast.fire({
                    icon: 'warning',
                    title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.swalDefaultQuestion').click(function () {
                Toast.fire({
                    icon: 'question',
                    title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            */

            $('.toastrDefaultSuccess').click(function () {
                toastr.success('Lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
            });
            $('.toastrDefaultInfo').click(function () {
                toastr.info('Lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
            });
            $('.toastrDefaultError').click(function () {
                toastr.error('Lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
            });
            $('.toastrDefaultWarning').click(function () {
                toastr.warning('Lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
            });

            $('.toastsDefaultDefault').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultTopLeft').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    position: 'topLeft',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultBottomRight').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    position: 'bottomRight',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultBottomLeft').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    position: 'bottomLeft',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultAutohide').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    autohide: true,
                    delay: 750,
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultNotFixed').click(function () {
                $(document).Toasts('create', {
                    title: 'Toast Title',
                    fixed: false,
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultFull').click(function () {
                $(document).Toasts('create', {
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    icon: 'fas fa-envelope fa-lg',
                })
            });
            $('.toastsDefaultFullImage').click(function () {
                $(document).Toasts('create', {
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    image: '../../dist/img/user3-128x128.jpg',
                    imageAlt: 'User Picture',
                })
            });
            $('.toastsDefaultSuccess').click(function () {
                $(document).Toasts('create', {
                    class: 'bg-success',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultInfo').click(function () {
                $(document).Toasts('create', {
                    class: 'bg-info',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultWarning').click(function () {
                $(document).Toasts('create', {
                    class: 'bg-warning',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultDanger').click(function () {
                $(document).Toasts('create', {
                    class: 'bg-danger',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
            $('.toastsDefaultMaroon').click(function () {
                $(document).Toasts('create', {
                    class: 'bg-maroon',
                    title: 'Toast Title',
                    subtitle: 'Subtitle',
                    body: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
                })
            });
        });

    </script>

</asp:Content>
