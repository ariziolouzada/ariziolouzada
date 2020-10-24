using ariziolouzada.classes;
using CriptografiaSgpm;
using System;
using System.Web.UI;

namespace ariziolouzada.ast
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (VerificaCampos())
                {
                    if (Page.IsValid && (txtCaptcha.Text == Session["captcha"].ToString()))
                        if (txtCaptcha.Text == Session["captcha"].ToString())
                        {

                            if (LogarSistema())
                            {
                                var userLogado = Usuario.Pesquisar(txtLogin.Text);

                                var idUserLogado = Criptografia.Encrypt(userLogado.Id.ToString());
                                Cookie.Grava("idUserLogado", idUserLogado);

                                Session["logadoAst"] = "1";
                                //ltlMsn.Text = "<div class=\"alert alert-success alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                //                "Login realizado com sucesso!</div>";
                                Response.Redirect("~/ast/default.aspx");
                            }
                            else
                            {
                                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                                                "!! Usuário ou senha Inválidos !!</div>";
                                txtLogin.Focus();
                            }


                        }
                }
            }
            catch (Exception ex)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "ERRO: " + ex.Message + "</div>";
            }

        }

        private bool LogarSistema()
        {
            var senhaCriptografada = Criptografia.Encrypt(txtSenha.Text);
            return Usuario.AutenticacaoUsuario(txtLogin.Text.ToUpper(), senhaCriptografada);
            //return true;
        }

        private bool VerificaCampos()
        {
            if (txtLogin.Text == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Login não preenchido!</div>";
                txtLogin.Focus();
                return false;
            }

            if (txtSenha.Text == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Senha não preenchido!</div>";
                txtSenha.Focus();
                return false;
            }

            if (txtCaptcha.Text == string.Empty)
            {
                ltlMsn.Text = "<div class=\"alert alert-danger alert-dismissable\"><button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">X</button>" +
                "Campo Senha não preenchido!</div>";
                txtCaptcha.Focus();
                return false;
            }

            return true;
        }


    }
}