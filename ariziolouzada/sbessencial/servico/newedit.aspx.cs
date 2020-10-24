using System;
using System.Web.UI;
using sbessencial_cl;
using CriptografiaSgpm;

namespace ariziolouzada.sbessencial.servico
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
                                CarregaDados(int.Parse(id));
                                lblCabecalho.Text = "EDITAR SERVIÇO";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro
                                pnlStatus.Visible = false;

                                if (Session["cadServico"] != null)
                                    if (Session["cadServico"].ToString().Equals("OK"))
                                    {
                                        Session["cadServico"] = "";
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
                var srv = Servico.Pesquisar(id);
                if (srv != null)
                {
                    txtDescricao.Value = srv.Descricao;
                    txtValor.Value = string.Format("{0:C}", srv.Valor);
                    ddlStatus.SelectedValue = srv.IdStatus.ToString();
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
            //Response.Redirect("~/sbessencial/financeiro/caixa");
            if (Session["paremetrosDefault"].ToString() != string.Empty)
            {
                var paremetrosDefault = Session["paremetrosDefault"].ToString();
                Response.Redirect(string.Format("default.aspx?desc={0}", paremetrosDefault));
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
                        Session["cadServico"] = "OK";
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        btnVoltar_Click(sender, e);
                    }
                }
            }
        }


        private void LimparCampos()
        {
            txtDescricao.Value = string.Empty;
            txtValor.Value = string.Empty;
        }


        private bool VerificaCampos()
        {
            try
            {
                ltlMsn.Text = string.Empty;
                // Desrição
                if (txtDescricao.Value == string.Empty)
                {
                    txtDescricao.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe a DESCRIÇÃO.</p></div>";
                    return false;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (id.Equals("0"))
                {
                    var srv = Servico.Pesquisar(txtDescricao.Value);
                    if (srv != null)
                    {
                        txtDescricao.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: DESCRIÇÃO de serviço já cadastrada.</p></div>";
                        return false;
                    }
                }

                // Qtde Estoque
                if (txtValor.Value == string.Empty)
                {
                    txtValor.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o VALOR.</p></div>";
                    return false;
                }

                try
                {
                    var vlr = Convert.ToDecimal(txtValor.Value.Replace("R$", ""));
                }
                catch (FormatException fx)
                {
                    txtValor.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Valor Inválido.</p></div>";
                    return false;
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


        private bool SalvarDados()
        {
            try
            {
                //terminar im´lentação
                var srv = new Servico(txtDescricao.Value, decimal.Parse(txtValor.Value.Replace("R$", "")));

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!Servico.Inserir(srv))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar SERVIÇO!!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                srv.Id = int.Parse(id);
                srv.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!Servico.Editar(srv))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível Editar SERVIÇO!!!</p></div>";
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