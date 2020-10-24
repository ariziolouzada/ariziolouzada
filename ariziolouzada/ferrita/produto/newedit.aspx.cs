using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Web.Services;
using System.Web.UI;

namespace ariziolouzada.ferrita.produto
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

                            pnlStatus.Visible = !id.Equals("0");
                            CarregaDdlFornecedor(true);// (id.Equals("0"));

                            if (!id.Equals("0"))
                            {
                                //Editar 
                                CarregaDados(int.Parse(id));
                                lblTitulo.Text = "EDITAR";
                                //pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro
                                //pnlStatus.Visible = false;

                                //if (Session["cadCliente"] != null)
                                //    if (Session["cadCliente"].ToString().Equals("OK"))
                                //    {
                                //        Session["cadCliente"] = "";
                                //        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                //                     "</button><p><i class=\"fa fa-thumbs-o-up fa-lg\"></i> Cadastro realizado com sucesso!!!</p></div>";
                                //    }

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


        private void CarregaDdlFornecedor(bool comSelecione)
        {
            var lista = Fornecedor.ListaFornecedores(comSelecione);
            ddlFornecedor.Items.Clear();
            ddlFornecedor.DataSource = lista;
            ddlFornecedor.DataValueField = "IdFornecedor";
            ddlFornecedor.DataTextField = "NomeFornecedor";
            ddlFornecedor.DataBind();            
            
            if (comSelecione)
                ddlFornecedor.SelectedIndex = 0;
        }


        private void CarregaDados(int idProd)
        {

            try
            {
                var svcprod = ServicoProduto.Pesquisa(idProd);
                ddlFornecedor.SelectedValue = svcprod.IdFornecedor.ToString();
                //svcprod.IdTipoServProd = 1;
                txtCodigo.Value = svcprod.CodigoProduto;
                txtDescricao.Value = svcprod.DescricaoProduto;
                txtPrecoCusto.Value = svcprod.PrecoCusto.ToString("#,##0.00");
                txtPrecoVenda.Value = svcprod.PrecoVenda.ToString("#,##0.00");
                txtMargemLucro.Value = svcprod.MargemLucro.ToString("#,##0.00");

                //// = =========== AJUSTE ========
                //var lucro = svcprod.PrecoVenda - svcprod.PrecoCusto;
                //txtLucro.Value = svcprod.Lucro > 0 ? svcprod.Lucro.ToString("#,##0.00") : lucro.ToString("#,##0.00");
                //// ======================
                txtLucro.Value = svcprod.Lucro.ToString("#,##0.00");

                ddlEstoqueUnico.SelectedValue = svcprod.TamanhoUnico;
                ddlStatus.SelectedValue = svcprod.IdStatus.ToString();

                if (svcprod.TamanhoUnico.Equals("SIM"))
                {
                    pnlQtdeUnica.Visible = true;
                    pnlQtdes.Visible = false;
                    txtQtdeunica.Value = svcprod.QtdeEstoque.ToString();
                }
                else
                {
                    pnlQtdeUnica.Visible = false ;
                    pnlQtdes.Visible = true;
                    txtQtdTamP.Value = svcprod.QtdeEstqTamanhoP.ToString();
                    txtQtdTamM.Value = svcprod.QtdeEstqTamanhoM.ToString();
                    txtQtdTamG.Value = svcprod.QtdeEstqTamanhoG.ToString();
                    txtQtdTamGG.Value = svcprod.QtdeEstqTamanhoGG.ToString();
                    txtQtdTamEG.Value = svcprod.QtdeEstqTamanhoEG.ToString();
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCamposBtnSalvar())
            {
                if (SalvarProduto())
                {
                    btnCancelar_Click(sender, e);
                }
            }
        }


        private bool SalvarProduto()
        {
            try
            {
                var svcprod = new ServicoProduto();
                svcprod.IdFornecedor = int.Parse(ddlFornecedor.SelectedValue);
                svcprod.IdTipoServProd = 1;
                svcprod.CodigoProduto = txtCodigo.Value;
                svcprod.DescricaoProduto = txtDescricao.Value;
                svcprod.PrecoCusto = decimal.Parse(txtPrecoCusto.Value);
                svcprod.PrecoVenda = decimal.Parse(txtPrecoVenda.Value);
                svcprod.MargemLucro = decimal.Parse(txtMargemLucro.Value);
                svcprod.Lucro = decimal.Parse(txtLucro.Value);
                svcprod.TamanhoUnico = ddlEstoqueUnico.SelectedValue;

                if (svcprod.TamanhoUnico.Equals("SIM"))
                {
                    svcprod.QtdeEstoque = int.Parse(txtQtdeunica.Value);
                }
                else
                {
                    svcprod.QtdeEstqTamanhoP = int.Parse(txtQtdTamP.Value);
                    svcprod.QtdeEstqTamanhoM = int.Parse(txtQtdTamM.Value);
                    svcprod.QtdeEstqTamanhoG = int.Parse(txtQtdTamG.Value);
                    svcprod.QtdeEstqTamanhoGG = int.Parse(txtQtdTamGG.Value);
                    svcprod.QtdeEstqTamanhoEG = int.Parse(txtQtdTamEG.Value);

                    //Somatória de todas as qtde.
                    svcprod.QtdeEstoque = svcprod.QtdeEstqTamanhoP + svcprod.QtdeEstqTamanhoM + svcprod.QtdeEstqTamanhoG + svcprod.QtdeEstqTamanhoGG + svcprod.QtdeEstqTamanhoEG;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0")) //Cadastro
                {
                    return ServicoProduto.Inserir(svcprod);
                }

                //Editar
                svcprod.IdProduto = int.Parse(id);
                svcprod.IdStatus = int.Parse(ddlStatus.SelectedValue);

                return ServicoProduto.Editar(svcprod);

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarProduto-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }


        private bool VerificaCamposBtnSalvar()
        {
            if (txtDescricao.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Descrição é de preenchimento obrigatório!</p></div>";

                txtDescricao.Focus();
                return false;
            }

            if (txtPrecoCusto.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Preço de Custo é de preenchimento obrigatório!</p></div>";

                txtPrecoCusto.Focus();
                return false;
            }

            if (txtMargemLucro.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Margem de Lucro é de preenchimento obrigatório!</p></div>";

                txtMargemLucro.Focus();
                return false;
            }

            if (ddlEstoqueUnico.SelectedValue.Equals("SIM"))
            {
                if (txtQtdeunica.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Margem de Lucro é de preenchimento obrigatório!</p></div>";

                    txtQtdeunica.Focus();
                    return false;
                }
            }
            else
            {
                if (txtQtdTamP.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Qtde Tam P é de preenchimento obrigatório!</p></div>";
                    txtQtdTamP.Focus();
                    return false;
                }
                if (txtQtdTamM.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Qtde Tam M é de preenchimento obrigatório!</p></div>";
                    txtQtdTamM.Focus();
                    return false;
                }
                if (txtQtdTamG.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Qtde Tam G é de preenchimento obrigatório!</p></div>";
                    txtQtdTamGG.Focus();
                    return false;
                }
                if (txtQtdTamG.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Qtde Tam GG é de preenchimento obrigatório!</p></div>";
                    txtQtdTamGG.Focus();
                    return false;
                }
                if (txtQtdTamEG.Value == string.Empty)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo Qtde Tam EG é de preenchimento obrigatório!</p></div>";
                    txtQtdTamEG.Focus();
                    return false;
                }
            }

            return true;
        }


        protected void ddlEstoqueUnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstoqueUnico.SelectedValue.Equals("NÃO"))
            {
                pnlQtdeUnica.Visible = false;
                pnlQtdes.Visible = true;
            }
            else
            {
                pnlQtdeUnica.Visible = true;
                pnlQtdes.Visible = false;
            }
        }


        [WebMethod]
        public static string CalculaPrecoVenda(string pcCusto, string mgLucro)
        {
            try
            {
                //HttpContext.Current.Session["idContatoEscolaSelect"] = id;
                var precoCusto = Convert.ToDecimal(pcCusto);
                var margeLucro = Convert.ToDecimal(mgLucro);
                var lucro = (precoCusto * margeLucro);
                var precoVenda = precoCusto + lucro;
                return string.Format("{0}|{1}", lucro.ToString("#,##0.00"), precoVenda.ToString("#,##0.00"));
                //return precoVenda.ToString("#,##0.00");
                //return string.Format("{0:C}", precoVenda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CalculaPrecoVenda-ERRO:" + ex.Message + "</p></div>";

            }

        }


        [WebMethod]
        public static string CalculaMargemLucro(string pcCusto, string pcVenda)
        {
            try
            {
                //HttpContext.Current.Session["idContatoEscolaSelect"] = id;
                var precoCusto = Convert.ToDecimal(pcCusto);
                var precoVenda = Convert.ToDecimal(pcVenda);

                var lucro = precoVenda - precoCusto;// (precoCusto * margeLucro);
                var margemLucro =  lucro / precoCusto;

                return string.Format("{0}|{1}", lucro.ToString("#,##0.00"), margemLucro.ToString("#,##0.00"));
                //return precoVenda.ToString("#,##0.00");
                //return string.Format("{0:C}", precoVenda);
            }
            catch (Exception ex)
            {
                return "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                       "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CalculaMargemLucro-ERRO:" + ex.Message + "</p></div>";

            }

        }

    }
}