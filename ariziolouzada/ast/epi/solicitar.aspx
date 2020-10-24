<%@ Page Title="" Language="C#" MasterPageFile="~/ast/ast.Master" AutoEventWireup="true" CodeBehind="solicitar.aspx.cs" Inherits="ariziolouzada.ast.epi.solicitar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/plugins/iCheck/custom.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <form id="form1" runat="server">

        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-sm-12">
                <h2>Solicitação de EPI</h2>
            </div>
        </div>

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-md-3">
                    <div class="ibox">
                        <div class="ibox-content product-box">

                            <div class="product-imitation">
                                <img src="../imagens/capacete_seguranca.jpg" width="150" />
                            </div>
                            <div class="product-desc">
                                <%--<span class="product-price">$10</span>--%>
                                <small class="text-muted"></small>
                                <a href="#" class="product-name">Capacete de Segurança</a>

                                <%--<div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>--%>
                                <div class="m-t text-righ">
                                    <div class="m-t text-righ">
                                        <div class="i-checks">
                                            <label>
                                                <input type="checkbox" value="" id="ckCapacete" runat="server">
                                                <i></i>Solicitar</label>
                                        </div>

                                        <%--<a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="ibox">
                        <div class="ibox-content product-box">

                            <div class="product-imitation">
                                <img src="../imagens/oculos_seguranca.jpg" width="170" />
                                <br />
                                <br />
                                <br />

                            </div>
                            <div class="product-desc">
                                <%--<span class="product-price">$10
                            </span>--%>
                                <small class="text-muted"></small>
                                <a href="#" class="product-name">Óculos de Seguraça</a>

                                <%--  <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>--%>
                                <div class="m-t text-righ">
                                    <div class="i-checks">
                                        <label>
                                            <input type="checkbox" value="" id="ckOculos" runat="server">
                                            <i></i>Solicitar</label>
                                    </div>

                                    <%--<a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="ibox">
                        <div class="ibox-content product-box ">

                            <div class="product-imitation">
                                <img src="../imagens/protetor_auditivo_tipo_plug.jpg" width="150" />
                            </div>
                            <div class="product-desc">
                                <%--<span class="product-price">$10
                        </span>--%>
                                <small class="text-muted"></small>
                                <a href="#" class="product-name">Protetor Auricular</a>

                                <%--                        <div class="small m-t-xs">
                            Many desktop publishing packages and web page editors now.
                        </div>--%>
                                <div class="m-t text-righ">
                                    <div class="m-t text-righ">
                                        <div class="i-checks">
                                            <label>
                                                <input type="checkbox" value="" id="ckProtetor" runat="server">
                                                <i></i>Solicitar</label>
                                        </div>

                                        <%--<a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="ibox">
                        <div class="ibox-content product-box">

                            <div class="product-imitation">
                                <img src="../imagens/calcado_seguranca.jpg" width="150" />
                            </div>
                            <div class="product-desc">
                                <%--<span class="product-price">$10
                        </span>--%>
                                <small class="text-muted"></small>
                                <a href="#" class="product-name">Calçado de Segurança</a>

                                <%--                        <div class="small m-t-xs">
                            Many desktop publishing packages and web page editors now.
                        </div>--%>
                                <div class="m-t text-righ">
                                    <div class="m-t text-righ">
                                        <div class="i-checks">
                                            <label>
                                                <input type="checkbox" value="" id="ckCalcado" runat="server">
                                                <i></i>Solicitar</label>
                                        </div>

                                        <%--<a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>






                </div>

                <%--        <div class="row">
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="ibox">
                    <div class="ibox-content product-box">

                        <div class="product-imitation">
                            [ INFO ]
                        </div>
                        <div class="product-desc">
                            <span class="product-price">$10
                            </span>
                            <small class="text-muted">Category</small>
                            <a href="#" class="product-name">Product</a>



                            <div class="small m-t-xs">
                                Many desktop publishing packages and web page editors now.
                            </div>
                            <div class="m-t text-righ">

                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
                --%>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-w-m btn-primary" OnClick="btnConfirmar_Click" />
                </div>
                <div class="col-md-9">
                    <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hdlHashSolicitacao" runat="server" />                    
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <!-- iCheck -->
    <script src="../js/plugins/iCheck/icheck.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
        });
    </script>
</asp:Content>
