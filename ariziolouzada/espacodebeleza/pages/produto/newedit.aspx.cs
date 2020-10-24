using CriptografiaSgpm;
using espacobeleza_cl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ariziolouzada.espacodebeleza.pages.produto
{
    public partial class newedit : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString.HasKeys())
                    {
                        if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Page.Request.QueryString["id"]))
                        {

                            var id = Request.QueryString["id"];
                            id = Criptografia.Decrypt(id.Replace('_', '+'));

                            if (!id.Equals("0"))
                            {
                                //Editar 
                                CarregaDados(int.Parse(id));
                                lblCabecalho.Text = "EDITAR PRODUTO";
                                pnlStatus.Visible = true;
                                Session["cadProdEspBel"] = "";
                            }
                            else
                            {
                                pnlStatus.Visible = false;

                                if (Session["cadProdEspBel"] != null)
                                    if (Session["cadProdEspBel"].ToString().Equals("OK"))
                                    {
                                        Session["cadProdEspBel"] = "";
                                        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                    }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>Page_Load-ERRO:" + ex.Message + "</p></div>";
            }

        }


        private void CarregaDados(int id)
        {
            try
            {
                var pdto = ProdutoEspBel.Pesquisar(id);
                if (pdto != null)
                {
                    txtDescProduto.Value = pdto.Descricao;
                    txtDescCompleta.Value = pdto.DescricaoCompleta;
                    txtPrecoCusto.Value = string.Format("{0:C}", pdto.PrecoCusto);
                    txtPrecoVenda.Value = string.Format("{0:C}", pdto.PrecoVenda);
                    txtQtdEstoque.Value = pdto.QtdEstoque.ToString();
                    ddlStatus.SelectedValue = pdto.IdStatus.ToString();
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/cliente");
            if (Session["paremetrosDefault"].ToString() != string.Empty)
            {
                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                Response.Redirect(string.Format("default.aspx?nome={0}", paremetrosDefault));
            }
            else
            {
                Response.Redirect("default.aspx");
            }


        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/sbessencial/financeiro/caixa/newedit.aspx?id=X5A1oqTnjBE=");
            Response.Redirect("newedit.aspx?id=X5A1oqTnjBE=");
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (SalvarDados())
                {
                    var id = Request.QueryString["id"];
                    id = Criptografia.Decrypt(id.Replace('_', '+'));

                    if (id.Equals("0"))
                    {
                        Session["cadProdEspBel"] = "OK";
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        btnVoltar_Click(sender, e);
                    }
                }
            }
        }


        //private void LimparCampos()
        //{
        //    txtData.Value = string.Empty;
        //    txtEmail.Value = string.Empty;
        //    txtFacebook.Value = string.Empty;
        //    txtInstagram.Value = string.Empty;
        //    txtNome.Value = string.Empty;
        //    txtObservacao.Value = string.Empty;
        //    txtTelCel1.Value = string.Empty;
        //    txtTelCel2.Value = string.Empty;
        //    txtTelFixo.Value = string.Empty;
        //}


        private bool VerificaCampos()
        {
            try
            {
                // NOME
                if (txtDescProduto.Value == string.Empty)
                {
                    txtDescProduto.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a Descrição do produto.</p></div>";
                    return false;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (id.Equals("0"))
                {
                    var clt = ProdutoEspBel.Pesquisar(txtDescProduto.Value);
                    if (clt != null)
                    {
                        txtDescProduto.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Já existe um produto cadastrado com este Nome!!!</p></div>";
                        return false;
                    }
                }

                // Pço Custo
                if (txtPrecoCusto.Value == string.Empty)
                {
                    txtPrecoCusto.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o Preço de Custo do Produto.</p></div>";
                    return false;
                }

                try
                {
                    var dt = Convert.ToDecimal(txtPrecoCusto.Value.Replace("R$", ""));
                }
                catch (FormatException fx1)
                {
                    txtQtdEstoque.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO:  Preço de Custo INVÁLIDO.</p></div>";
                    return false;
                }


                // Pço Venda
                if (txtPrecoVenda.Value == string.Empty)
                {
                    txtPrecoVenda.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o Preço de Venda do Produto.</p></div>";
                    return false;
                }

                try
                {
                    var dt = Convert.ToDecimal(txtPrecoVenda.Value.Replace("R$", ""));
                }
                catch (FormatException fx2)
                {
                    txtQtdEstoque.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO:  Preço de Venda INVÁLIDO.</p></div>";
                    return false;
                }

                // Qtde Estoque
                if (txtQtdEstoque.Value == string.Empty)
                {
                    txtQtdEstoque.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a Qtde em estoque do Produto.</p></div>";
                    return false;
                }

                try
                {
                    var dt = Convert.ToInt32(txtQtdEstoque.Value);
                }
                catch (FormatException fx)
                {
                    txtQtdEstoque.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO:  Quantidade INVÁLIDA.</p></div>";
                    return false;
                }



                //var id = Request.QueryString["id"];
                //id = Criptografia.Decrypt(id.Replace('_', '+'));
                ////Session["idEpi"] = id;

                //if (!id.Equals("0"))
                //{
                //    //Só verifica qdo for o edição
                //    if (ddlStatus.SelectedIndex == 0)
                //    {
                //        ddlStatus.Focus();
                //        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                //                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o Status.</p></div>";
                //        return false;
                //    }

                //}

                return true;

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>VerificaCampos-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        private bool SalvarDados()
        {
            try
            {
                var pdto = new ProdutoEspBel()
                {
                    Descricao = txtDescProduto.Value.ToUpper(),
                    DescricaoCompleta = txtDescCompleta.Value.ToUpper(),
                    QtdEstoque = int.Parse(txtQtdEstoque.Value),
                    PrecoCusto = decimal.Parse(txtPrecoCusto.Value.Replace("R$", "")),
                    PrecoVenda = decimal.Parse(txtPrecoVenda.Value.Replace("R$", ""))
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!ProdutoEspBel.Inserir(pdto))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar PRODUTO!!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                pdto.Id = int.Parse(id);
                pdto.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!ProdutoEspBel.Editar(pdto))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível Editar produto!!!</p></div>";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>SalvarDados-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }

    }
}