using CriptografiaSgpm;
using ferrita_Cl;
using System;
using System.Web.UI;

namespace ariziolouzada.ferrita.colaborador
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

        private void CarregaDados(int id)
        {
            try
            {
                var colab = Colaborador.PesquisaPeloId(id);
                txtNome.Value = colab.Nome;
                txtCpf.Value = colab.Cpf;
                txtTelefone.Value = colab.Telefone;
                txtEmail.Value = colab.Email;
                txtFacebokk.Value = colab.Facebook;
                txtEndLogradouro.Value = colab.EndLogradouro;
                txtEndNumero.Value = colab.EndNumero;
                txtEndComplemento.Value = colab.EndComplemento;
                txtEndBairro.Value = colab.Endbairro;
                txtEndCidade.Value = colab.EndCidade;
                txtEndUf.Value = colab.EndUf;
                txtEndCep.Value = colab.EndCep;
                txtDatanascto.Value = colab.DataNascto != DateTime.MinValue ? colab.DataNascto.ToString("yyyy-MM-dd") : string.Empty;
                ddlSexo.SelectedValue = colab.Sexo;
                ddlStatus.SelectedValue = colab.IdStatus.ToString();
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> CarregaDados-ERRO:" + ex.Message + "</p></div>";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaCamposBtnSalvar())
                {
                    if (SalvarColaborador())
                    {
                        btnCancelar_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> btnSalvar_Click-ERRO:" + ex.Message + "</p></div>";
            }
        }

        private bool SalvarColaborador()
        {
            try
            {
                var colab = new Colaborador();
                colab.Nome = txtNome.Value.ToUpper();
                colab.Cpf = txtCpf.Value;
                colab.Telefone = txtTelefone.Value;
                colab.Email = txtEmail.Value;
                colab.Facebook = txtFacebokk.Value;
                colab.EndLogradouro = txtEndLogradouro.Value.ToUpper();
                colab.EndNumero = txtEndNumero.Value.ToUpper();
                colab.EndComplemento = txtEndComplemento.Value.ToUpper();
                colab.Endbairro = txtEndBairro.Value.ToUpper();
                colab.EndCidade = txtEndCidade.Value.ToUpper();
                colab.EndUf = txtEndUf.Value.ToUpper();
                colab.EndCep = txtEndCep.Value;
                colab.DataNascto = txtDatanascto.Value.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDatanascto.Value);
                colab.Sexo = ddlSexo.SelectedValue;

                var id = Request.QueryString["id"];
                id = Criptografia.Decrypt(id.Replace('_', '+'));

                if (id.Equals("0"))//Salvar Inserir
                {
                    return Colaborador.Inserir(colab);
                }
                //Editar
                colab.IdStatus = int.Parse(ddlStatus.SelectedValue);
                colab.Id = int.Parse(id);
                return Colaborador.Update(colab);
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i> SalvarColaborador-ERRO:" + ex.Message + "</p></div>";
                return false;
            }

        }

        private bool VerificaCamposBtnSalvar()
        {
            var id = Request.QueryString["id"];
            id = Criptografia.Decrypt(id.Replace('_', '+'));

            ltlMsn.Text = "";
            if (txtNome.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo NOME é de preenchimento obrigatório!</p></div>";
                txtNome.Focus();
                return false;
            }

            if (txtCpf.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo CPF é de preenchimento obrigatório!</p></div>";
                txtCpf.Focus();
                return false;
            }

            if (txtTelefone.Value == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                             "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo TELEFONE é de preenchimento obrigatório!</p></div>";
                txtTelefone.Focus();
                return false;
            }

            if (id.Equals("0"))
                if (ddlSexo.SelectedIndex == 0)
                {
                    ltlMsn.Text = "<div class=\"alert alert-block alert-danger fade in\"><button data-dismiss=\"alert\" class=\"close\" type=\"button\">X" +
                                 "</button><p><i class=\"fa fa-times-circle fa-lg\"></i>ERRO: Campo SEXO é de seleção obrigatório!</p></div>";
                    ddlSexo.Focus();
                    return false;
                }

            return true;

        }
    }
}