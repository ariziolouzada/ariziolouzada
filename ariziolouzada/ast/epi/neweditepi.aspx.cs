using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Web.UI;

namespace ariziolouzada.ast.epi
{
    public partial class neweditepi : Page
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
                            Session["idEpi"] = id;

                            if (!id.Equals("0"))
                            {
                                //Editar EPI
                                CarregaDdlTipoMaterial(false);
                                CarregaDadosEpi(int.Parse(id));
                                lblCabecalho.Text = "Editar EPI";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro de EPI
                                pnlStatus.Visible = false;
                                CarregaDdlTipoMaterial(true);
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


        private void CarregaDdlTipoMaterial(bool comSelecione)
        {
            var lista = MaterialTipo.ListaTipoMaterial(comSelecione);
            ddlTipoMaterial.Items.Clear();
            ddlTipoMaterial.DataSource = lista;
            ddlTipoMaterial.DataValueField = "Id";
            ddlTipoMaterial.DataTextField = "Descricao";
            ddlTipoMaterial.DataBind();

            if (comSelecione)
                ddlTipoMaterial.SelectedIndex = 0;
        }


        private void CarregaDadosEpi(int id)
        {
            try
            {
                var epi = Material.PesquisaMaterial(id);
                if (epi != null)
                {
                    txtDescricao.Value = epi.Descricao;
                    txtPrecoCusto.Value = epi.PrecoCusto > 0 ? string.Format("{0:C}", epi.PrecoCusto) : "0,00";
                    txtPrecoCusto.Value = txtPrecoCusto.Value.Replace("R$", "").Trim();
                    txtQtdEstoque.Value = epi.QtdeEstoque.ToString();
                    ddlStatus.SelectedValue = epi.IdStatus.ToString();
                    ddlTipoMaterial.SelectedValue = epi.IdTipo.ToString();
                }

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>CarregaDadosEpi-ERRO:" + ex.Message + "</p></div>";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ast/epi/");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos())
            {
                if (SalvarDadosEpi())
                {
                    Response.Redirect("~/ast/epi/");
                }
            }
        }

        private bool VerificaCampos()
        {
            try
            {

                // Desrição
                if (txtDescricao.Value == string.Empty)
                {
                    txtDescricao.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe Descrição do EPI.</p></div>";
                    return false;
                }

                if (txtDescricao.Value.Length > 70)
                {
                    txtDescricao.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Quantidade de caracteres da descrição do EPI é maior que o permitido." +
                                  "<br />Limite: 100 caracteres. Você ultrapassou " + string.Format("{0} caracteres.", (txtDescricao.Value.Length - 70) * -1) + "</p></div>";
                    return false;
                }

                // Preço de custo
                if (txtPrecoCusto.Value == string.Empty)
                {
                    txtPrecoCusto.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o Preço de custo.</p></div>";
                    return false;
                }

                try
                {
                    var vlr = Convert.ToDecimal(txtPrecoCusto.Value);
                }
                catch (FormatException fx)
                {
                    txtPrecoCusto.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Preço de custo Inválido.</p></div>";
                    return false;
                }

                // Qtde Estoque
                if (txtQtdEstoque.Value == string.Empty)
                {
                    txtQtdEstoque.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a Quantidade em estoque.</p></div>";
                    return false;
                }

                try
                {
                    var vlr = Convert.ToInt32(txtQtdEstoque.Value);
                }
                catch (FormatException fx)
                {
                    txtPrecoCusto.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Quantidade em estoque Inválida.</p></div>";
                    return false;
                }

                if (Session["idEpi"].ToString().Equals("0"))
                {
                    //Só verifica qdo for o cadastro
                    if (ddlTipoMaterial.SelectedIndex == 0)
                    {
                        ddlTipoMaterial.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Selecione o tipo do material.</p></div>";
                        return false;
                    }

                }

                return true;

            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>VerificaCampos-ERRO:" + ex.Message + "</p></div>";
                return false;
            }
        }

        private bool SalvarDadosEpi()
        {

            try
            {
                var mat = new Material
                {
                    PrecoCusto = decimal.Parse(txtPrecoCusto.Value),
                    Descricao = txtDescricao.Value,
                    QtdeEstoque = int.Parse(txtQtdEstoque.Value),
                    IdTipo = int.Parse(ddlTipoMaterial.SelectedValue),
                    IdStatus = 1
                };


                if (Session["idEpi"].ToString().Equals("0"))
                {

                    return Material.Inserir(mat);
                }
                else
                {
                    mat.Id = Convert.ToInt32(Session["idEpi"]);
                    mat.IdStatus = int.Parse(ddlStatus.SelectedValue);
                    return Material.Editar(mat);
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>SalvarDadosEpi-ERRO:" + ex.Message + "</p></div>";
                return false;
            }

        }

    }
}