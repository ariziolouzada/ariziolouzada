using CriptografiaSgpm;
using sbessencial_cl;
using System;
using System.Web.UI;

namespace ariziolouzada.sbessencial.profissional
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
                                lblCabecalho.Text = "EDITAR PROFISSIONAL";
                                pnlStatus.Visible = true;
                            }
                            else
                            {
                                //Cadastro
                                pnlStatus.Visible = false;

                                if (Session["cadProfissional"] != null)
                                    if (Session["cadProfissional"].ToString().Equals("OK"))
                                    {
                                        Session["cadProfissional"] = "";
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
                var profissional = Profissional.Pesquisar(id);
                if (profissional != null)
                {
                    txtNome.Value = profissional.Nome;                    
                    txtTelefone.Value = profissional.Telefone;
                    ddlStatus.SelectedValue = profissional.IdStatus.ToString();
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
                    //Session["idEpi"] = id;

                    if (id.Equals("0"))
                    {
                        Session["cadProfissional"] = "OK";
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
            txtNome.Value = string.Empty;
            txtTelefone.Value = string.Empty;
        }


        private bool VerificaCampos()
        {
            try
            {
                // NOME
                if (txtNome.Value == string.Empty)
                {
                    txtNome.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o NOME do Cliente.</p></div>";
                    return false;
                }

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                if (id.Equals("0"))
                {
                    var clt = Profissional.Pesquisar(txtNome.Value);
                    if (clt != null)
                    {
                        txtNome.Focus();
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Já existe um cliente cadastrado com este Nome!!!</p></div>";
                        return false;
                    }
                }
                

                // 
                if (txtTelefone.Value == string.Empty)
                {
                    txtTelefone.Focus();
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">×" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> ATENÇÃO: Informe o pelo menos um telefone para contato.</p></div>";
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

                var profissional = new Profissional()
                {
                    Nome = txtNome.Value.ToUpper(),                    
                    Telefone = txtTelefone.Value
                };

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));
                //Session["idEpi"] = id;

                if (id.Equals("0"))
                {
                    //Cadastro
                    if (!Profissional.Inserir(profissional))
                    {
                        ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                     "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível cadastrar PROFISSIONAL!!!</p></div>";
                        return false;
                    }
                    return true;
                }

                //Edição
                profissional.Id = int.Parse(id);
                profissional.IdStatus = int.Parse(ddlStatus.SelectedValue);
                if (!Profissional.Editar(profissional))
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Não foi possível Editar profissional!!!</p></div>";
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