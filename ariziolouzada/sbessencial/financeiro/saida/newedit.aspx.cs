using System;
using System.Web.UI;
using CriptografiaSgpm;
using sbessencial_cl;

namespace ariziolouzada.sbessencial.financeiro.saida
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
                                lblCabecalho.Text = "EDITAR SAÍDA";
                                //pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro

                                //pnlStatus.Visible = false;
                                if (Session["cadCaixa"] != null)
                                    if (Session["cadCaixa"].ToString().Equals("OK"))
                                    {
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
                var tiposaida = TipoSaida.Pesquisar(id);
                if (tiposaida != null)
                {
                    txtDescricao.Value = tiposaida.Descricao;
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
                var nome = paremetrosDefault[0];
                Response.Redirect(string.Format("default.aspx?nome={0}", nome));
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
                    //Session["idEpi"] = id;

                    if (id.Equals("0"))
                    {
                        LimparCampos();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-success fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">OK" +
                                     "</button><p><i class=\"fa-thumbs-o-up\"></i> Cadastro realizado com sucesso!!!</p></div>";
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
        }


        private bool VerificaCampos()
        {
            try
            {
                // descrição
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
                    var saida = TipoSaida.Pesquisar(txtDescricao.Value);
                    if (saida != null)
                    {
                        txtDescricao.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: DESCRIÇÃO já cadastrada!!!</p></div>";
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


        private bool SalvarDados()
        {
            try
            {
                var ts = new TipoSaida(txtDescricao.Value.ToUpper());

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                //Session["idEpi"] = id;

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!TipoSaida.Inserir(ts))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar !!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                ts.Id = int.Parse(id);
                if (!TipoSaida.Editar(ts))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível Editar !!!</p></div>";
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